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
                  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
                  this.customPictureBox1 = new MyCustomLib.Controls.CustomPictureBox();
                  this.SuspendLayout();
                  // 
                  // customPictureBox1
                  // 
                  this.customPictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customPictureBox1.BackgroundImage")));
                  this.customPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                  this.customPictureBox1.BorderColor = System.Drawing.Color.Orange;
                  this.customPictureBox1.BorderWidth = 1F;
                  this.customPictureBox1.DrawBorder = false;
                  this.customPictureBox1.HoverShadowStyle = MyCustomLib.Controls.PictureBoxShadowStyle.None;
                  this.customPictureBox1.Image = global::Debug.Properties.Resources.DFX;
                  this.customPictureBox1.Location = new System.Drawing.Point(292, 137);
                  this.customPictureBox1.Name = "customPictureBox1";
                  this.customPictureBox1.Radius = 20D;
                  this.customPictureBox1.Size = new System.Drawing.Size(429, 200);
                  this.customPictureBox1.Style = MyCustomLib.Controls.CustomContainerStyle.Rounded;
                  this.customPictureBox1.TabIndex = 1;
                  this.customPictureBox1.Text = "customPictureBox1";
                  // 
                  // DebugForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                  this.ClientSize = new System.Drawing.Size(800, 450);
                  this.Controls.Add(this.customPictureBox1);
                  this.Name = "DebugForm";
                  this.Opacity = 1D;
                  this.Text = "Form1";
                  this.Controls.SetChildIndex(this.customPictureBox1, 0);
                  this.ResumeLayout(false);

            }

            #endregion

            private MyCustomLib.Controls.CustomPictureBox customPictureBox1;
      }
}

