using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using AlckieBot.Helpers;

namespace AlckieBot.Data
{
    public static class Giphy
    {
        private const string GIPHY_API = "http://api.giphy.com/v1/";
        private const string GIPHY_MEDIA_URL_FORMAT = "http://media2.giphy.com/media/{0}/giphy.gif";

        public static string GetGiphyUrl(string searchTerms)
        {
            var searchUrl = GIPHY_API + "gifs/search?q=" + WebUtility.UrlEncode(searchTerms) + "&api_key=" + ConfigurationManager.AppSettings["GIPHY_TOKEN"];
            var mediaUrl = "";

            var request = WebRequest.Create(searchUrl);
            request.ContentType = "text/json";
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = JsonConvert.DeserializeObject<dynamic>(streamReader.ReadToEnd());
                if (result != null && result.data != null && result.data.Count > 0)
                {
                    var randomPicIndex = RandomHelper.GetRandomNumber(result.data.Count) - 1;
                    if (result.data[randomPicIndex] != null)
                    {
                        mediaUrl = String.Format(GIPHY_MEDIA_URL_FORMAT, result.data[randomPicIndex].id);
                    }
                }
            }

            return mediaUrl;
        }
    }
}
