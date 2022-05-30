using System.Drawing;
using System.Windows.Forms;
using System;
using MyCustomLib.GraphicFunctions;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace MyCustomLib.Controls
{
      public enum PictureBoxShadowStyle
      {
            None,
            ShadowOnHover,
            ShadowWithoutHover,
            Always
      }

      // Кастомный PictureBox ==================================================
      public class CustomPictureBox : CustomControl
      {
            public event EmptyEventHandler ImageChanged;
            public event EmptyEventHandler HoverStyleChanged;
            public event EmptyEventHandler ShadowPropertiesChanged;

            #region === Свойства и поля ===
            protected EnhancedImage _enhancedImage;
            protected PictureBox _mainPictureBox = new PictureBox();
            protected PictureBoxShadowStyle _shadowStyle = PictureBoxShadowStyle.None;
            protected ShadowProperties _imageShadowProperties = new ShadowProperties()
            {
                  ShadowStyle = ShadowStyle.Full,
                  InitialColor = Color.Black,
                  FiniteColor = Color.Transparent,
                  Range = 0.15,
                  GradientBlend = new Blend(2)
                  {
                        Factors = new float[] { 0.0f, 1.0f },
                        Positions = new float[] { 0.0f, 1.0f }
                  }
            };

            [Category("Custom settings"), Description("Sets image of the PictureBox.")]
            public Image Image { get => _enhancedImage.PlainImage; set { SetImage(value); ImageChanged?.Invoke(); } }
            [Category("Custom settings"), Description("Properties of a shadow drawn on an image.")]
            public ShadowProperties ImageShadowProperties { get => _imageShadowProperties; set { _imageShadowProperties = value; ShadowPropertiesChanged?.Invoke(); } }
            [Category("Custom settings"), Description("Sets hover style of the picturebox.")]
            public PictureBoxShadowStyle HoverShadowStyle { get => _shadowStyle; set { _shadowStyle = value; HoverStyleChanged?.Invoke(); } }
            #endregion

            public CustomPictureBox() : base()
            {
                  Size = new Size(300, 200);
                  _drawBorder = false;
                  BackgroundImage = Properties.Resources.PosterBG;
                  BackgroundImageLayout = ImageLayout.Stretch;

                  ImageChanged += OnImageChanged;
                  HoverStyleChanged += OnHoverStyleChanged;

                  Initialize();
            }

            #region === Обработка событий ===

            protected override void OnPaint(PaintEventArgs e)
            {
                  DrawBody(e.Graphics, ClientRectangle);
            }

            protected override void OnLayout(LayoutEventArgs levent)
            {
                  base.OnLayout(levent);

                  UpdateImage();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                  _mainPictureBox.Size = Size;
                  base.OnSizeChanged(e);
            }

            protected virtual void OnImageChanged() => Invalidate();
            protected virtual void OnHoverStyleChanged()
            {
                  UpdateImage();
            }
            #endregion

            protected override void DrawBody(Graphics g, Rectangle client)
            {
                  Rectangle borderRectangle = client; borderRectangle.Inflate(-1, -1);
                  GraphicsPath body = GetGPath(client);
                  GraphicsPath border = GetGPath(borderRectangle);

                  Region = new Region(body);

                  g.SmoothingMode = SmoothingMode.HighQuality;

                  if (Image == null)
                        DrawBackgroundImageAsync();
                  else
                        g.Clear(BackColor);

                  if (DrawBorder && _mainPictureBox != null)
                  {
                        AddBorder(g, border);
                  }
            }

            private void SetImage(Image image)
            {
                  if (image == null)
                        return;

                  EnhancedImageProperties imageProperties = new EnhancedImageProperties()
                  {
                        PlainImage = image,
                        ImagePath = GetGPath(ClientRectangle)
                  };

                  imageProperties.ShadowProperties = _imageShadowProperties;

                  if (_drawBorder)
                        imageProperties.BorderProperties = new BorderProperties()
                        {
                              BorderColor = this.BorderColor,
                              BorderWidth = this.BorderWidth
                        };

                  if (_drawBorder)
                        _enhancedImage = new EnhancedImage(imageProperties as IEnhancedImageWithShadowAndBorder);
                  else
                        _enhancedImage = new EnhancedImage(imageProperties as IEnhancedImageWithShadow);

                  _mainPictureBox.Image = _enhancedImage.PlainImage;
            }

            private Image GetCorrectImage()
            {
                  if (HoverShadowStyle == PictureBoxShadowStyle.ShadowWithoutHover || HoverShadowStyle == PictureBoxShadowStyle.Always)
                  {
                        if (_drawBorder)
                              return _enhancedImage.ShadowedImageWithBorder;
                        else
                              return _enhancedImage.ShadowedImage;
                  }
                  else if (_drawBorder)
                        return _enhancedImage.ImageWithBorder;
                  else
                        return _enhancedImage.PlainImage;
            }

            private void UpdateImage()
            {
                  _mainPictureBox.Image = GetCorrectImage();

                  Invalidate();
            }

            private void Initialize()
            {
                  _mainPictureBox.Dock = DockStyle.Fill;
                  _mainPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                  _mainPictureBox.BackColor = Color.Transparent;
                  _mainPictureBox.Region = Region;

                  _mainPictureBox.Parent = this;
                  Controls.Add(_mainPictureBox);

                  LoadEvents();
            }

            private void LoadEvents()
            {
                  _mainPictureBox.MouseLeave += (s, e) =>
                  {
                        if(HoverShadowStyle != PictureBoxShadowStyle.Always && HoverShadowStyle != PictureBoxShadowStyle.None)
                              switch(HoverShadowStyle)
                              {
                                    case PictureBoxShadowStyle.ShadowWithoutHover: { _mainPictureBox.Image = _enhancedImage.ShadowedImage; break; }
                                    case PictureBoxShadowStyle.ShadowOnHover: { _mainPictureBox.Image = _enhancedImage.PlainImage; break; }
                              }
                  };
                  _mainPictureBox.MouseEnter += (s, e) =>
                  {
                        if(HoverShadowStyle != PictureBoxShadowStyle.Always && HoverShadowStyle != PictureBoxShadowStyle.None)
                              switch (HoverShadowStyle)
                              {
                                    case PictureBoxShadowStyle.ShadowWithoutHover: { _mainPictureBox.Image = _enhancedImage.PlainImage; break; }
                                    case PictureBoxShadowStyle.ShadowOnHover: { _mainPictureBox.Image = _enhancedImage.ShadowedImage; break; }
                              }
                  };
            }
      }
}
