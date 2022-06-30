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
      /// <summary>
      /// Изображение с эффектами
      /// </summary>
      public class EnhancedImage
      {
            private bool _drawBorder = false;
            private bool _drawShadow = false;

            /// <summary>
            /// Изображение без эффектов
            /// </summary>
            public Image PlainImage { get; private set; }
            /// <summary>
            /// Изображение с рамкой
            /// </summary>
            public Image ImageWithBorder { get; private set; }
            /// <summary>
            ///  Изображение с тенью
            /// </summary>
            public Image ShadowedImage { get; private set; }
            /// <summary>
            /// Изображение с тенью и рамкой
            /// </summary>
            public Image ShadowedImageWithBorder { get; private set; }
            /// <summary>
            /// Настройки контейнера, в котором находится изображение
            /// </summary>
            public CustomContainerProperties ContainerProperties { get; private set; }
            /// <summary>
            /// Настройки тени
            /// </summary>
            public ShadowProperties ShadowProperties { get; private set; }
            /// <summary>
            /// Настройки рамки
            /// </summary>
            public BorderProperties BorderProperties { get; private set; }

            /// <summary>
            /// Включение и выключение рамки
            /// </summary>
            public bool DrawBorder { get => _drawBorder; set { _drawBorder = value; Initialize(); } }
            /// <summary>
            /// Включение и выключение тени
            /// </summary>
            public bool DrawShadow { get => _drawShadow; set { _drawShadow = value; Initialize(); } }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="EnhancedImage"/> через интерфейс
            /// </summary>
            /// <param name="enhancedPlainImage">Интерфейс, на основе которого инициализируется экземпляр класса</param>
            public EnhancedImage(IEnhancedPlainImage enhancedPlainImage)
            {
                  PlainImage = enhancedPlainImage.PlainImage;
                  ContainerProperties = enhancedPlainImage.ContainerProperties;

                  Initialize();
            }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="EnhancedImage"/> через интерфейс
            /// </summary>
            /// <param name="enhancedPlainImageWithBorder">Интерфейс, на основе которого инициализируется экземпляр класса</param>
            public EnhancedImage(IEnhancedPlainImageWithBorder enhancedPlainImageWithBorder)
            {
                  PlainImage = enhancedPlainImageWithBorder.PlainImage;
                  ContainerProperties = enhancedPlainImageWithBorder.ContainerProperties;
                  BorderProperties = enhancedPlainImageWithBorder.BorderProperties;

                  _drawBorder = true;

                  Initialize();
            }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="EnhancedImage"/> через интерфейс
            /// </summary>
            /// <param name="enhancedImageWithShadow">Интерфейс, на основе которого инициализируется экземпляр класса</param>
            public EnhancedImage(IEnhancedImageWithShadow enhancedImageWithShadow)
            {
                  PlainImage = enhancedImageWithShadow.PlainImage;
                  ContainerProperties = enhancedImageWithShadow.ContainerProperties;
                  ShadowProperties = enhancedImageWithShadow.ShadowProperties;

                  _drawShadow = true;

                  Initialize();
            }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="EnhancedImage"/> через интерфейс
            /// </summary>
            /// <param name="enhancedImageWithShadowAndBorder">Интерфейс, на основе которого инициализируется экземпляр класса</param>
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

            /// <summary>
            /// Асинхронная инициализация <see cref="EnhancedImage"/>
            /// </summary>
            protected async void InitializeAsync() => await Task.Run(Initialize); 

            /// <summary>
            /// Инициализация <see cref="EnhancedImage"/>
            /// </summary>
            protected void Initialize()
            {
                  if (_drawBorder)
                        ImageWithBorder = AddBorder(PlainImage);

                  if (_drawShadow)
                        ShadowedImage = CustomGraphics.AddShadow(PlainImage, ShadowProperties);

                  if (_drawBorder && _drawShadow)
                        ShadowedImageWithBorder = AddBorder(PlainImage);

            }

            /// <summary>
            /// Устанавливает толщину рамки <see cref="ImageWithBorder"/> и <see cref="ShadowedImageWithBorder"/>
            /// </summary>
            /// <param name="borderWidth">Толщина рамки</param>
            public void SetBorderWidth(float borderWidth)
            {
                  BorderProperties = new BorderProperties()
                  {
                        BorderWidth = borderWidth,
                        BorderColor = BorderProperties.BorderColor
                  };

                  Initialize();
            }

            /// <summary>
            /// Устанавливает цвет рамки <see cref="ShadowedImage"/> и <see cref="ShadowedImageWithBorder"/>
            /// </summary>
            /// <param name="borderColor">Толщина рамки</param>
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
