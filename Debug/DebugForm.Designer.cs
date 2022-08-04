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
                  this.customButton1 = new MyCustomLib.Controls.CustomButton();
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
                  // customButton1
                  // 
                  this.customButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
                  this.customButton1.BorderColor = System.Drawing.Color.Orange;
                  this.customButton1.BorderWidth = 5F;
                  this.customButton1.DrawBorder = true;
                  this.customButton1.Hoverable = false;
                  this.customButton1.Location = new System.Drawing.Point(453, 308);
                  this.customButton1.MouseEnterColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(137)))), ((int)(((byte)(45)))));
                  this.customButton1.Name = "customButton1";
                  this.customButton1.Radius = 20D;
                  this.customButton1.Size = new System.Drawing.Size(200, 50);
                  this.customButton1.Style = MyCustomLib.Controls.CustomContainerStyle.Rounded;
                  this.customButton1.TabIndex = 3;
                  this.customButton1.Text = "Do stuff";
                  this.customButton1.Click += new System.EventHandler(this.customButton1_Click);
                  // 
                  // DebugForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                  this.ClientSize = new System.Drawing.Size(800, 450);
                  this.Controls.Add(this.customButton1);
                  this.Controls.Add(this.richTextBox1);
                  this.Controls.Add(this.listBox1);
                  this.Name = "DebugForm";
                  this.Opacity = 1D;
                  this.Text = "Form1";
                  this.Controls.SetChildIndex(this.listBox1, 0);
                  this.Controls.SetChildIndex(this.richTextBox1, 0);
                  this.Controls.SetChildIndex(this.customButton1, 0);
                  this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.ListBox listBox1;
            private System.Windows.Forms.RichTextBox richTextBox1;
            private MyCustomLib.Controls.CustomButton customButton1;
      }
}

