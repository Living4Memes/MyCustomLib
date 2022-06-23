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
                  this.customButton1 = new MyCustomLib.Controls.CustomButton();
                  this.SuspendLayout();
                  // 
                  // customButton1
                  // 
                  this.customButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(70)))));
                  this.customButton1.BorderColor = System.Drawing.Color.Orange;
                  this.customButton1.BorderWidth = 1F;
                  this.customButton1.DrawBorder = false;
                  this.customButton1.Hoverable = false;
                  this.customButton1.Location = new System.Drawing.Point(195, 168);
                  this.customButton1.MouseEnterColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(137)))), ((int)(((byte)(45)))));
                  this.customButton1.Name = "customButton1";
                  this.customButton1.Radius = 20D;
                  this.customButton1.Size = new System.Drawing.Size(298, 74);
                  this.customButton1.Style = MyCustomLib.Controls.CustomContainerStyle.Rounded;
                  this.customButton1.TabIndex = 1;
                  this.customButton1.Text = "customButton1";
                  this.customButton1.Click += new System.EventHandler(this.customButton1_Click);
                  // 
                  // DebugForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                  this.ClientSize = new System.Drawing.Size(800, 450);
                  this.Controls.Add(this.customButton1);
                  this.Name = "DebugForm";
                  this.Opacity = 1D;
                  this.Text = "Form1";
                  this.Controls.SetChildIndex(this.customButton1, 0);
                  this.ResumeLayout(false);

            }

            #endregion

            private MyCustomLib.Controls.CustomButton customButton1;
      }
}

