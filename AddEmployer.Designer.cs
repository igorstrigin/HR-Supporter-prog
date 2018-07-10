namespace SalaryGUI
{
    partial class AddEmployer
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
            this.cbSelectEmployer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btAddEmployer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbSelectEmployer
            // 
            this.cbSelectEmployer.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSelectEmployer.FormattingEnabled = true;
            this.cbSelectEmployer.Location = new System.Drawing.Point(176, 64);
            this.cbSelectEmployer.Name = "cbSelectEmployer";
            this.cbSelectEmployer.Size = new System.Drawing.Size(241, 27);
            this.cbSelectEmployer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберете начальника:";
            // 
            // btAddEmployer
            // 
            this.btAddEmployer.Location = new System.Drawing.Point(281, 172);
            this.btAddEmployer.Name = "btAddEmployer";
            this.btAddEmployer.Size = new System.Drawing.Size(136, 23);
            this.btAddEmployer.TabIndex = 2;
            this.btAddEmployer.Text = "Добавить начальника";
            this.btAddEmployer.UseVisualStyleBackColor = true;
            this.btAddEmployer.Click += new System.EventHandler(this.btAddEmployer_Click);
            // 
            // AddEmployer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 215);
            this.Controls.Add(this.btAddEmployer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSelectEmployer);
            this.Name = "AddEmployer";
            this.Text = "AddEmployer";
            this.Load += new System.EventHandler(this.AddEmployer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSelectEmployer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btAddEmployer;
    }
}