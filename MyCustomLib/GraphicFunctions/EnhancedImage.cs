using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public GraphicsPath ImagePath { get; private set; }
            public ShadowProperties ShadowProperties { get; private set; }
            public BorderProperties BorderProperties { get; private set; }

            public EnhancedImage(IEnhancedPlainImage enhancedPlainImage)
            {
                  PlainImage = enhancedPlainImage.PlainImage;
                  ImagePath = enhancedPlainImage.ImagePath;

                  InitializeAsync();
            }

            public EnhancedImage(IEnhancedPlainImageWithBorder enhancedPlainImageWithBorder)
            {
                  PlainImage = enhancedPlainImageWithBorder.PlainImage;
                  ImagePath = enhancedPlainImageWithBorder.ImagePath;
                  BorderProperties = enhancedPlainImageWithBorder.BorderProperties;

                  _drawBorder = true;

                  InitializeAsync();
            }

            public EnhancedImage(IEnhancedImageWithShadow enhancedImageWithShadow)
            {
                  PlainImage = enhancedImageWithShadow.PlainImage;
                  ImagePath = enhancedImageWithShadow.ImagePath;
                  ShadowProperties = enhancedImageWithShadow.ShadowProperties;

                  _drawShadow = true;

                  InitializeAsync();
            }

            public EnhancedImage(IEnhancedImageWithShadowAndBorder enhancedImageWithShadowAndBorder)
            {
                  PlainImage = enhancedImageWithShadowAndBorder.PlainImage;
                  ImagePath = enhancedImageWithShadowAndBorder.ImagePath;
                  ShadowProperties = enhancedImageWithShadowAndBorder.ShadowProperties;
                  BorderProperties = enhancedImageWithShadowAndBorder.BorderProperties;

                  _drawBorder = true;
                  _drawShadow = true;

                  InitializeAsync();
            }

            protected async void InitializeAsync() => await Task.Run(Initialize); 

            protected void Initialize()
            {
                  if (_drawBorder)
                        ImageWithBorder = CustomGraphics.AddBorder(PlainImage, ImagePath, BorderProperties);

                  if (_drawShadow)
                        ShadowedImage = CustomGraphics.AddShadow(PlainImage, ShadowProperties);

                  if (_drawBorder && _drawShadow)
                        ShadowedImageWithBorder = CustomGraphics.AddBorder(ShadowedImage, ImagePath, BorderProperties);

            }

            
      }
}
