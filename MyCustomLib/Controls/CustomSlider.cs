namespace MyCustomLib.Controls
{
      #region === Контролы ===

      public class CustomSlider : CustomControl
      {
            //public delegate void Dlg();
            //event Dlg ImageIndexChanged;
            //event Dlg ImageChanged;

            //private PictureBox EffectArrows;
            //private PictureBox EffectGradient;

            ////public List<ShikiSliderImage> Images { get; private set; } = new List<ShikiSliderImage>();
            //public int ImageIndex { get; set; } = -1;
            ////public ShikiSliderImage SliderImage { get; set; } = new ShikiSliderImage();

            //public CustomSlider()
            //{
            //      //EffectArrows = new PictureBox();
            //      //EffectGradient = new PictureBox();

            //      //SliderImage.Dock = DockStyle.Fill;
            //      //this.Controls.Add(SliderImage);
            //      //AdjustEffects();
            //      //LoadEvents();
            //      //Size = new Size(180, 270);
            //}

            //protected override void OnSizeChanged(EventArgs e)
            //{
            //      base.OnSizeChanged(e);

            //      if (EffectArrows != null && EffectGradient != null)
            //            DrawEffects();
            //}

            //private void AdjustEffects()
            //{
            //      EffectGradient.BackColor = Color.Transparent;
            //      EffectGradient.Size = Size;
            //      EffectGradient.Dock = DockStyle.Fill;

            //      EffectArrows.BackColor = Color.Transparent;
            //      EffectArrows.Size = Size;
            //      EffectArrows.Dock = DockStyle.Fill;

            //      DrawEffects();

            //      SliderImage.Controls.Add(EffectGradient);
            //      SliderImage.Controls.Add(EffectArrows);
            //}

            //private void DrawEffects()
            //{
            //      Bitmap arrowsBM = new Bitmap(Width, Height);
            //      Bitmap gradientBM = new Bitmap(Width, Height);

            //      DrawArrows(arrowsBM, new Point(0, Height / 2));
            //      DrawArrows(gradientBM, new Point(0, Height / 2));

            //      DrawVerticalLineGradient(gradientBM);

            //      EffectArrows.Image = arrowsBM;
            //      EffectGradient.Image = gradientBM;
            //}

            //private void DrawArrows(Bitmap bitmap, Point where)
            //{
            //      Graphics graph = Graphics.FromImage(bitmap);

            //      int width = 15, height = 30, indent = 15;
            //      string path = Path.GetTempFileName();

            //      File.WriteAllText(path, MyLib.Properties.Resources.angle_left);
            //      SvgDocument document = SvgDocument.Open(path);
            //      document.Width = width;
            //      document.Height = height;
            //      Bitmap left = document.Draw();
            //      Point AlignPointL = new Point(where.X + indent, where.Y - left.Height / 2);
            //      graph.DrawImage(left, AlignPointL);

            //      document = new SvgDocument();

            //      File.WriteAllText(path, MyLib.Properties.Resources.angle_right);
            //      document = SvgDocument.Open(path);
            //      document.Width = width;
            //      document.Height = height;
            //      Bitmap right = document.Draw();
            //      Point AlignPointR = new Point(where.X + bitmap.Width - indent * 2, where.Y - left.Height / 2);
            //      graph.DrawImage(right, AlignPointR);
            //      File.Delete(path);
            //}

            //private void LoadEvents()
            //{
            //      EffectGradient.MouseEnter += (s, e) =>
            //      {
            //            EffectGradient.Visible = false;
            //      };
            //      EffectArrows.MouseLeave += (s, e) =>
            //      {
            //            EffectGradient.Visible = true;
            //      };
            //}

            //private void ChangeImageIndex(int index)
            //{
            //      ImageIndex = index;
            //      SliderImage = Images[ImageIndex];
            //      ImageIndexChanged?.Invoke();
            //}

            ////private void ChangeImage(ShikiSliderImage Image)
            ////{
            ////      SliderImage = Image;
            ////      ImageChanged?.Invoke();
            ////}

            ////public void AddImage(Image image)
            ////{
            ////      ShikiSliderImage newImage = new ShikiSliderImage();
            ////      newImage.Image = image;
            ////      Images.Add(newImage);
            ////      ImageIndex++;
            ////}
      }

      #endregion
}
