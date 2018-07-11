namespace SalaryGUI
{
    partial class CalcSalary
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
            this.dtSalary = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lFIO = new System.Windows.Forms.Label();
            this.lGroup = new System.Windows.Forms.Label();
            this.lStartSalary = new System.Windows.Forms.Label();
            this.lStartDateTime = new System.Windows.Forms.Label();
            this.lSalary = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtSalary
            // 
            this.dtSalary.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtSalary.Location = new System.Drawing.Point(295, 50);
            this.dtSalary.Name = "dtSalary";
            this.dtSalary.Size = new System.Drawing.Size(200, 26);
            this.dtSalary.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(33, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберете предположительную дату:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(33, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "ФИО:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(33, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Начальная зарплата:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(33, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Дата вступления на работу:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(33, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Рабочая группа:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(33, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "Текущая зарплата:";
            // 
            // lFIO
            // 
            this.lFIO.AutoSize = true;
            this.lFIO.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lFIO.Location = new System.Drawing.Point(87, 98);
            this.lFIO.Name = "lFIO";
            this.lFIO.Size = new System.Drawing.Size(48, 19);
            this.lFIO.TabIndex = 7;
            this.lFIO.Text = "ФИО:";
            // 
            // lGroup
            // 
            this.lGroup.AutoSize = true;
            this.lGroup.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGroup.Location = new System.Drawing.Point(155, 137);
            this.lGroup.Name = "lGroup";
            this.lGroup.Size = new System.Drawing.Size(48, 19);
            this.lGroup.TabIndex = 8;
            this.lGroup.Text = "ФИО:";
            // 
            // lStartSalary
            // 
            this.lStartSalary.AutoSize = true;
            this.lStartSalary.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStartSalary.Location = new System.Drawing.Point(184, 175);
            this.lStartSalary.Name = "lStartSalary";
            this.lStartSalary.Size = new System.Drawing.Size(48, 19);
            this.lStartSalary.TabIndex = 9;
            this.lStartSalary.Text = "ФИО:";
            // 
            // lStartDateTime
            // 
            this.lStartDateTime.AutoSize = true;
            this.lStartDateTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStartDateTime.Location = new System.Drawing.Point(234, 213);
            this.lStartDateTime.Name = "lStartDateTime";
            this.lStartDateTime.Size = new System.Drawing.Size(48, 19);
            this.lStartDateTime.TabIndex = 10;
            this.lStartDateTime.Text = "ФИО:";
            // 
            // lSalary
            // 
            this.lSalary.AutoSize = true;
            this.lSalary.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lSalary.Location = new System.Drawing.Point(168, 256);
            this.lSalary.Name = "lSalary";
            this.lSalary.Size = new System.Drawing.Size(48, 19);
            this.lSalary.TabIndex = 11;
            this.lSalary.Text = "ФИО:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Расчитать зарплату";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CalcSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 333);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lSalary);
            this.Controls.Add(this.lStartDateTime);
            this.Controls.Add(this.lStartSalary);
            this.Controls.Add(this.lGroup);
            this.Controls.Add(this.lFIO);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtSalary);
            this.Name = "CalcSalary";
            this.Text = "CalcSalary";
            this.Load += new System.EventHandler(this.CalcSalary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtSalary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lFIO;
        private System.Windows.Forms.Label lGroup;
        private System.Windows.Forms.Label lStartSalary;
        private System.Windows.Forms.Label lStartDateTime;
        private System.Windows.Forms.Label lSalary;
        private System.Windows.Forms.Button button1;
    }
}