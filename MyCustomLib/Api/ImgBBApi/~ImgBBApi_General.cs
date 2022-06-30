namespace MyCustomLib.Api.ImgBBApi
{
      /// <summary>
      /// Содержит информацию об ответе imgbb.com
      /// </summary>
      public struct ImgBBResponse
      {
            /// <summary>
            /// Ответ imgbb.com в JSON формате
            /// </summary>
            public string RawImgBBResponse { get; set; }
            /// <summary>
            /// Основная информация об изображении
            /// </summary>
            public ImgBBResponse_GeneralData Data { get; set; }
            /// <summary>
            /// Флаг, отображающий был ли запрос выполнен успешно
            /// </summary>
            public bool Success { get; set; }
            /// <summary>
            /// Статус запроса
            /// </summary>
            public int Status { get; set; }
      }

      /// <summary>
      /// Содержит основную информацию об изображении
      /// </summary>
      public struct ImgBBResponse_GeneralData
      {
            /// <summary>
            /// ID изображения
            /// </summary>
            public string ID { get; set; }
            /// <summary>
            /// Название изображения
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// Ссылка на изображение на сайте imgbb.com
            /// </summary>
            public string URL_Viewer { get; set; }
            /// <summary>
            /// Прямая ссылка на изображение
            /// </summary>
            public string URL { get; set; }
            /// <summary>
            /// Ссылка на уменьшенное изображение
            /// </summary>
            public string Display_URL { get; set; }
            /// <summary>
            /// Ширина изображения
            /// </summary>
            public string Width { get; set; }
            /// <summary>
            /// Высота изображения
            /// </summary>
            public string Height { get; set; }
            /// <summary>
            /// Размер изображения в МБ
            /// </summary>
            public string Size { get; set; }
            /// <summary>
            /// Время загрузки
            /// </summary>
            public string Time { get; set; }
            /// <summary>
            /// Время автоматического удаления
            /// </summary>
            public string Expiration { get; set; }
            public ImgBBResponse_ImageInfo Image { get; set; }
            public ImgBBResponse_ImageInfo Thumb { get; set; }
            public ImgBBResponse_ImageInfo Medium { get; set; }
            /// <summary>
            /// Ссылка для удаления
            /// </summary>
            public string Delete_URL { get; set; }
      }

      /// <summary>
      /// Содержит параметры изображения
      /// </summary>
      public struct ImgBBResponse_ImageInfo
      {
            /// <summary>
            /// Название файла изображения
            /// </summary>
            public string FileName { get; set; }
            /// <summary>
            /// Название изображения
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// MIME формат изображения
            /// </summary>
            public string Mime { get; set; }
            /// <summary>
            /// Расширение файла
            /// </summary>
            public string Extension { get; set; }
            /// <summary>
            /// Прямая ссылка на изображение
            /// </summary>
            public string URL { get; set; }
      }      

      internal static class Extensions
      {
            public static ImgBBResponse ParseImgBBResponse(this string str)
            {
                  return ResponseParser.Parse(str);
            }
      }
}
