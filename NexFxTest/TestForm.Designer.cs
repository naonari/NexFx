namespace NexFxTest
{
    partial class TestForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.exTextBox1 = new NexFx.Controls.ExTextBox();
            this.exTextBox2 = new NexFx.Controls.ExTextBox();
            this.exButton1 = new NexFx.Controls.ExButton();
            this.SuspendLayout();
            //
            // exTextBox1
            //
            this.exTextBox1.CaptionControl = null;
            this.exTextBox1.FocusedBackColor = System.Drawing.SystemColors.Window;
            this.exTextBox1.FocusedForeColor = System.Drawing.SystemColors.WindowText;
            this.exTextBox1.Location = new System.Drawing.Point(12, 12);
            this.exTextBox1.Name = "exTextBox1";
            this.exTextBox1.Size = new System.Drawing.Size(100, 19);
            this.exTextBox1.TabIndex = 0;
            //
            // exTextBox2
            //
            this.exTextBox2.CaptionControl = null;
            this.exTextBox2.FocusedBackColor = System.Drawing.SystemColors.Window;
            this.exTextBox2.FocusedForeColor = System.Drawing.SystemColors.WindowText;
            this.exTextBox2.Location = new System.Drawing.Point(12, 37);
            this.exTextBox2.Name = "exTextBox2";
            this.exTextBox2.Size = new System.Drawing.Size(100, 19);
            this.exTextBox2.TabIndex = 1;
            //
            // exButton1
            //
            this.exButton1.CaptionControl = null;
            this.exButton1.FocusedBackColor = System.Drawing.SystemColors.Control;
            this.exButton1.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.exButton1.Location = new System.Drawing.Point(12, 62);
            this.exButton1.Name = "exButton1";
            this.exButton1.Size = new System.Drawing.Size(75, 23);
            this.exButton1.TabIndex = 2;
            this.exButton1.Text = "exButton1";
            this.exButton1.UseVisualStyleBackColor = true;
            this.exButton1.Click += new System.EventHandler(this.exButton1_Click);
            //
            // TestForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.exButton1);
            this.Controls.Add(this.exTextBox2);
            this.Controls.Add(this.exTextBox1);
            this.EnableEscClose = true;
            this.KeyPreview = true;
            this.Name = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NexFx.Controls.ExTextBox exTextBox1;
        private NexFx.Controls.ExTextBox exTextBox2;
        private NexFx.Controls.ExButton exButton1;
    }
}
