using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BestStories.API.Controllers
{

    [ApiController]
    public class BestStoriesController
    {

        [Route("BestStories/GetTopStoriesIds")]
        [HttpGet]
        public async Task<string> GetTopStoriesIds()
        {
            string retVal = null;
            try
            {
                WebRequest requestTopIds = HttpWebRequest.Create("https://hacker-news.firebaseio.com/v0/beststories.json");
                WebResponse responseTopIds = await requestTopIds.GetResponseAsync();
                StreamReader readerTopIds = new StreamReader(responseTopIds.GetResponseStream());

                string json = readerTopIds.ReadToEnd();
                int[] topIds = System.Text.Json.JsonSerializer.Deserialize<int[]>(json);

                string jsonStories = "[";
                for (var x = 0; x < 19; x++)
                {
                    WebRequest requestStory = HttpWebRequest.Create("https://hacker-news.firebaseio.com/v0/item/" + topIds[x] + ".json");
                    WebResponse responseStory = await requestStory.GetResponseAsync();
                    StreamReader readerStory = new StreamReader(responseStory.GetResponseStream());

                    var jsonResponse = readerStory.ReadToEnd(); ;

                    JObject jo = JObject.Parse(jsonResponse);
                    jo.Property("kids").Remove();
                    jo.Property("type").Remove();
                    jo.Property("id").Remove();

                    JProperty by = jo.Property("by");
                    by.Value.Rename("postedBy");
                    JProperty descendants = jo.Property("descendants");
                    descendants.Value.Rename("commentCount");

                    JProperty url = jo.Property("url");
                    if (url != null) {
                        url.Value.Rename("uri");
                    }

                    JProperty time = jo.Property("time");

                    if (time != null)
                    {
                        string stringTime = jo.Property("time").Value.ToString();
                        //string javascriptJsonTime = JsonConvert.SerializeObject(stringTime, new JavaScriptDateTimeConverter());

                        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(stringTime));

                        //var date = DateTime.ParseExact(dateTimeOffset.UtcDateTime.ToString(), "yyyy-MM-ddThh:mm:ss zzz",
                        //                                CultureInfo.InvariantCulture);

                        jo.Property("time").Value = dateTimeOffset;
                    }

                    jsonStories += jo.ToString();

                }
                jsonStories += "]";

                retVal = jsonStories;
                return retVal;
            }
            catch (System.Exception ex)
            {
                var errMsg = "Response build failed:" + ex;
                retVal = errMsg;
                return retVal;
            }
        }
    }

    #region Aux Methods
    public static class NewtonsoftExtensions
    {
        public static void Rename(this JToken token, string newName)
        {
            var parent = token.Parent;
            if (parent == null)
                throw new InvalidOperationException("The parent is missing.");
            var newToken = new JProperty(newName, token);
            parent.Replace(newToken);
        }
    }
    #endregion Aux Methods
}




