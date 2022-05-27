using Newtonsoft.Json;
using System;

namespace MyCustomLib.Api.ImgBBApi
{
      static class ResponseParser
      {
            public static ImgBBResponse Parse(string rawResponse)
            {
                  try
                  {
                        dynamic info = JsonConvert.DeserializeObject<ImgBBResponse>(rawResponse);

                        ImgBBResponse response = (ImgBBResponse)info;
                        response.RawImgBBResponse = rawResponse;
                        return response;
                  }
                  catch (Exception ex)
                  {
                        throw ex;
                  }
            }
      }
}
