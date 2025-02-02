﻿namespace WinForms.Examples
{
    partial class MultipleYAxes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.btnAddAxis = new System.Windows.Forms.Button();
            this.btnRemoveAxis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlot1.Location = new System.Drawing.Point(12, 41);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(776, 397);
            this.formsPlot1.TabIndex = 0;
            // 
            // button1
            // 
            this.btnAddAxis.Location = new System.Drawing.Point(12, 12);
            this.btnAddAxis.Name = "button1";
            this.btnAddAxis.Size = new System.Drawing.Size(75, 23);
            this.btnAddAxis.TabIndex = 1;
            this.btnAddAxis.Text = "Add";
            this.btnAddAxis.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.btnRemoveAxis.Location = new System.Drawing.Point(93, 12);
            this.btnRemoveAxis.Name = "button2";
            this.btnRemoveAxis.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAxis.TabIndex = 2;
            this.btnRemoveAxis.Text = "Remove";
            this.btnRemoveAxis.UseVisualStyleBackColor = true;
            // 
            // MultipleYAxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRemoveAxis);
            this.Controls.Add(this.btnAddAxis);
            this.Controls.Add(this.formsPlot1);
            this.Name = "MultipleYAxes";
            this.Text = "MultiAxis";
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Button btnAddAxis;
        private System.Windows.Forms.Button btnRemoveAxis;
    }
}