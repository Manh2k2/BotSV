namespace WindowsFormsApp1
{
    partial class Form1
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
            this.lsvMess = new System.Windows.Forms.ListView();
            this.textMess = new System.Windows.Forms.TextBox();
            this.tbnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvMess
            // 
            this.lsvMess.HideSelection = false;
            this.lsvMess.Location = new System.Drawing.Point(19, 12);
            this.lsvMess.Name = "lsvMess";
            this.lsvMess.Size = new System.Drawing.Size(759, 376);
            this.lsvMess.TabIndex = 0;
            this.lsvMess.UseCompatibleStateImageBehavior = false;
            this.lsvMess.View = System.Windows.Forms.View.List;
            // 
            // textMess
            // 
            this.textMess.Location = new System.Drawing.Point(19, 394);
            this.textMess.Multiline = true;
            this.textMess.Name = "textMess";
            this.textMess.Size = new System.Drawing.Size(656, 42);
            this.textMess.TabIndex = 1;
            // 
            // tbnSend
            // 
            this.tbnSend.Location = new System.Drawing.Point(681, 394);
            this.tbnSend.Name = "tbnSend";
            this.tbnSend.Size = new System.Drawing.Size(96, 41);
            this.tbnSend.TabIndex = 2;
            this.tbnSend.Text = "Send";
            this.tbnSend.UseVisualStyleBackColor = true;
            this.tbnSend.Click += new System.EventHandler(this.tbnSend_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.tbnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbnSend);
            this.Controls.Add(this.textMess);
            this.Controls.Add(this.lsvMess);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvMess;
        private System.Windows.Forms.TextBox textMess;
        private System.Windows.Forms.Button tbnSend;
    }
}

