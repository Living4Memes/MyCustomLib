using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyCustomLib.Controls;

namespace MyCustomLib.GraphicFunctions
{
      public class EnhancedImage
      {
            private bool _drawBorder = false;
            private bool _drawShadow = false;

            public Image PlainImage { get; private set; }
            public Image ImageWithBorder { get; private set; }
            public Image ShadowedImage { get; private set; }
            public Image ShadowedImageWithBorder { get; private set; }
            public CustomContainerProperties ContainerProperties { get; private set; }
            public ShadowProperties ShadowProperties { get; private set; }
            public BorderProperties BorderProperties { get; private set; }

            public bool DrawBorder { get => _drawBorder; set { _drawBorder = value; Initialize(); } }
            public bool DrawShadow { get => _drawShadow; set { _drawShadow = value; Initialize(); } }

            public EnhancedImage(IEnhancedPlainImage enhancedPlainImage)
            {
                  PlainImage = enhancedPlainImage.PlainImage;
                  ContainerProperties = enhancedPlainImage.ContainerProperties;

                  Initialize();
            }

            public EnhancedImage(IEnhancedPlainImageWithBorder enhancedPlainImageWithBorder)
            {
                  PlainImage = enhancedPlainImageWithBorder.PlainImage;
                  ContainerProperties = enhancedPlainImageWithBorder.ContainerProperties;
                  BorderProperties = enhancedPlainImageWithBorder.BorderProperties;

                  _drawBorder = true;

                  Initialize();
            }

            public EnhancedImage(IEnhancedImageWithShadow enhancedImageWithShadow)
            {
                  PlainImage = enhancedImageWithShadow.PlainImage;
                  ContainerProperties = enhancedImageWithShadow.ContainerProperties;
                  ShadowProperties = enhancedImageWithShadow.ShadowProperties;

                  _drawShadow = true;

                  Initialize();
            }

            public EnhancedImage(IEnhancedImageWithShadowAndBorder enhancedImageWithShadowAndBorder)
            {
                  PlainImage = enhancedImageWithShadowAndBorder.PlainImage;
                  ContainerProperties = enhancedImageWithShadowAndBorder.ContainerProperties;
                  ShadowProperties = enhancedImageWithShadowAndBorder.ShadowProperties;
                  BorderProperties = enhancedImageWithShadowAndBorder.BorderProperties;

                  _drawBorder = true;
                  _drawShadow = true;

                  Initialize();
            }

            protected async void InitializeAsync() => await Task.Run(Initialize); 

            protected void Initialize()
            {
                  if (_drawBorder)
                        ImageWithBorder = AddBorder(PlainImage);

                  if (_drawShadow)
                        ShadowedImage = CustomGraphics.AddShadow(PlainImage, ShadowProperties);

                  if (_drawBorder && _drawShadow)
                        ShadowedImageWithBorder = AddBorder(PlainImage);

            }

            public void SetBorderWidth(float borderWidth)
            {
                  BorderProperties = new BorderProperties()
                  {
                        BorderWidth = borderWidth,
                        BorderColor = BorderProperties.BorderColor
                  };

                  Initialize();
            }

            public void SetBorderColor(Color borderColor)
            {
                  BorderProperties = new BorderProperties()
                  {
                        BorderWidth = BorderProperties.BorderWidth,
                        BorderColor = borderColor
                  };

                  Initialize();
            }

            private Image AddBorder(Image image)
            {
                  switch (ContainerProperties.ContainerStyle)
                  {
                        case CustomContainerStyle.Square: return CustomGraphics.AddBorder(image, CustomGraphics.GetRectanglePath(image.GetRectangle()), BorderProperties);
                        case CustomContainerStyle.Rounded: return CustomGraphics.AddBorder(image, CustomGraphics.GetRoundedRectanglePath(image.GetRectangle()), BorderProperties);
                        case CustomContainerStyle.Pill: return CustomGraphics.AddBorder(image, CustomGraphics.GetPillRectanglePath(image.GetRectangle()), BorderProperties);
                        default: throw new ArgumentException("Unknown CustomContainerStyle. Function: EnhancedImage.AddBorder", nameof(ContainerProperties.ContainerStyle));
                  }
            }
      }
}
