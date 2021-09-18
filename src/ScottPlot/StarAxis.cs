﻿using ScottPlot.Drawing;
using System;
using System.Drawing;
using System.Linq;

namespace ScottPlot
{
    public struct StarAxisTick
    {
        public double Location { get; set; }
        public double[] Labels { get; set; }
    }

    public class StarAxis
    {
        /// <summary>
        /// The ticks for each spoke.
        /// </summary>
        public StarAxisTick[] Ticks { get; set; }

        /// <summary>
        /// The number of spokes to draw.
        /// </summary>
        public int NumberOfSpokes { get; set; }

        /// <summary>
        /// Labels for each category.
        /// Length must be equal to the number of columns (categories) in the original data.
        /// </summary>
        public string[] CategoryLabels;

        /// <summary>
        /// Icons for each category.
        /// Length must be equal to the number of columns (categories) in the original data.
        /// </summary>
        public Image[] CategoryImages;

        /// <summary>
        /// Controls rendering style of the concentric circles (ticks) of the web
        /// </summary>
        public RadarAxis AxisType { get; set; }

        /// <summary>
        /// Indicates the type of axis chart to render
        /// </summary>
        public AxisChart AxisChartType { get; set; }

        /// <summary>
        /// Color of the axis lines and concentric circles representing ticks
        /// </summary>
        public Color WebColor { get; set; } = Color.Gray;

        /// <summary>
        /// If true, each value will be written in text on the plot.
        /// </summary>
        public bool ShowAxisValues { get; set; } = true;

        /// <summary>
        /// If true, category labels will be written in text on the plot (provided they exist)
        /// </summary>
        public bool ShowCategoryLabels { get; set; } = true;

        /// <summary>
        /// Determines whether each spoke should be labeled, or just the first
        /// </summary>
        public bool LabelEachSpoke { get; set; } = false;

        public Graphics Graphics { get; set; }

        /// <summary>
        /// Font used for labeling values on the plot
        /// </summary>
        public Drawing.Font Font = new();

        public int XAxisIndex { get; set; } = 0;
        public int YAxisIndex { get; set; } = 0;

        public void Render(PlotDimensions dims, Bitmap bmp, bool lowQuality = false)
        {
            double sweepAngle = 2 * Math.PI / NumberOfSpokes;
            double minScale = new double[] { dims.PxPerUnitX, dims.PxPerUnitX }.Min();
            PointF origin = new PointF(dims.GetPixelX(0), dims.GetPixelY(0));

            using Pen pen = GDI.Pen(WebColor);
            using StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center };
            using StringFormat sf2 = new StringFormat();
            using System.Drawing.Font font = GDI.Font(Font);
            using Brush fontBrush = GDI.Brush(Font.Color);

            for (int i = 0; i < Ticks.Length; i++)
            {
                double tickDistancePx = Ticks[i].Location * minScale;

                if (AxisType == RadarAxis.Circle)
                {
                    Graphics.DrawEllipse(pen, (int)(origin.X - tickDistancePx), (int)(origin.Y - tickDistancePx), (int)(tickDistancePx * 2), (int)(tickDistancePx * 2));
                }
                else if (AxisType == RadarAxis.Polygon)
                {
                    PointF[] points = new PointF[NumberOfSpokes];
                    for (int j = 0; j < NumberOfSpokes; j++)
                    {
                        float x = (float)(tickDistancePx * Math.Cos(sweepAngle * j - Math.PI / 2) + origin.X);
                        float y = (float)(tickDistancePx * Math.Sin(sweepAngle * j - Math.PI / 2) + origin.Y);
                        points[j] = new PointF(x, y);
                    }
                    Graphics.DrawPolygon(pen, points);
                }
            }

            for (int i = 0; i < NumberOfSpokes; i++)
            {
                PointF destination = new PointF((float)(1.1 * Math.Cos(sweepAngle * i - Math.PI / 2) * minScale + origin.X), (float)(1.1 * Math.Sin(sweepAngle * i - Math.PI / 2) * minScale + origin.Y));
                Graphics.DrawLine(pen, origin, destination);

                for (int j = 0; j < Ticks.Length; j++)
                {
                    double tickDistancePx = Ticks[j].Location * minScale;

                    if (ShowAxisValues)
                    {
                        if (LabelEachSpoke)
                        {
                            float x = (float)(tickDistancePx * Math.Cos(sweepAngle * i - Math.PI / 2) + origin.X);
                            float y = (float)(tickDistancePx * Math.Sin(sweepAngle * i - Math.PI / 2) + origin.Y);

                            sf2.Alignment = x < origin.X ? StringAlignment.Far : StringAlignment.Near;
                            sf2.LineAlignment = y < origin.Y ? StringAlignment.Far : StringAlignment.Near;

                            double val = Ticks[j].Labels[i];
                            Graphics.DrawString($"{val:f1}", font, fontBrush, x, y, sf2);
                        }
                        else if (i == 0)
                        {
                            double val = Ticks[j].Labels[0];
                            Graphics.DrawString($"{val:f1}", font, fontBrush, origin.X, (float)(-tickDistancePx + origin.Y), sf2);
                        }
                    }
                }
            }

            if (CategoryImages is not null)
            {
                for (int i = 0; i < NumberOfSpokes; i++)
                {
                    double sweepOffset = AxisChartType == AxisChart.CoxComb ? sweepAngle / 2 : 0;
                    double cosinus = Math.Cos(sweepAngle * i + sweepOffset - Math.PI / 2);
                    double sinus = Math.Sin(sweepAngle * i + sweepOffset - Math.PI / 2);
                    int imageWidth = CategoryImages[i].Width;
                    int imageHeight = CategoryImages[i].Height;

                    PointF imageDestination = new(
                        (float)(1.45 * cosinus * minScale + origin.X - imageWidth / 2 * cosinus),
                        (float)(1.45 * sinus * minScale + origin.Y - imageHeight / 2 * sinus));

                    Graphics.DrawImage(CategoryImages[i], new RectangleF(imageDestination.X - CategoryImages[i].Width / 2, imageDestination.Y - CategoryImages[i].Height / 2, CategoryImages[i].Width, CategoryImages[i].Height));
                }
            }
            else if (CategoryLabels is not null && ShowCategoryLabels)
            {
                for (int i = 0; i < NumberOfSpokes; i++)
                {
                    PointF textDestination = new PointF(
                        (float)(1.3 * Math.Cos(sweepAngle * i - Math.PI / 2) * minScale + origin.X),
                        (float)(1.3 * Math.Sin(sweepAngle * i - Math.PI / 2) * minScale + origin.Y));

                    if (Math.Abs(textDestination.X - origin.X) < 0.1)
                        sf.Alignment = StringAlignment.Center;
                    else
                        sf.Alignment = dims.GetCoordinateX(textDestination.X) < 0 ? StringAlignment.Far : StringAlignment.Near;
                    Graphics.DrawString(CategoryLabels[i], font, fontBrush, textDestination, sf);
                }
            }
        }
    }
}
