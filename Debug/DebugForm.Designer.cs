namespace Debug
{
      partial class DebugForm
      {
            /// <summary>
            /// Обязательная переменная конструктора.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Освободить все используемые ресурсы.
            /// </summary>
            /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
            protected override void Dispose(bool disposing)
            {
                  if (disposing && (components != null))
                  {
                        components.Dispose();
                  }
                  base.Dispose(disposing);
            }

            #region Код, автоматически созданный конструктором форм Windows

            /// <summary>
            /// Требуемый метод для поддержки конструктора — не изменяйте 
            /// содержимое этого метода с помощью редактора кода.
            /// </summary>
            private void InitializeComponent()
            {
                  this.listBox1 = new System.Windows.Forms.ListBox();
                  this.richTextBox1 = new System.Windows.Forms.RichTextBox();
                  this.SuspendLayout();
                  // 
                  // listBox1
                  // 
                  this.listBox1.FormattingEnabled = true;
                  this.listBox1.ItemHeight = 16;
                  this.listBox1.Location = new System.Drawing.Point(623, 81);
                  this.listBox1.Name = "listBox1";
                  this.listBox1.Size = new System.Drawing.Size(135, 180);
                  this.listBox1.TabIndex = 1;
                  // 
                  // richTextBox1
                  // 
                  this.richTextBox1.Location = new System.Drawing.Point(12, 81);
                  this.richTextBox1.Name = "richTextBox1";
                  this.richTextBox1.Size = new System.Drawing.Size(601, 180);
                  this.richTextBox1.TabIndex = 2;
                  this.richTextBox1.Text = "";
                  // 
                  // DebugForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                  this.ClientSize = new System.Drawing.Size(800, 450);
                  this.Controls.Add(this.richTextBox1);
                  this.Controls.Add(this.listBox1);
                  this.Name = "DebugForm";
                  this.Opacity = 1D;
                  this.Text = "Form1";
                  this.Controls.SetChildIndex(this.listBox1, 0);
                  this.Controls.SetChildIndex(this.richTextBox1, 0);
                  this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.ListBox listBox1;
            private System.Windows.Forms.RichTextBox richTextBox1;
      }
}

