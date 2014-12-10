using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Adbrain.WebFullStack.Models;

namespace Adbrain.WebFullStack
{
    public static class RedditHandler
    {
        public static async Task<Post[]> GetLatestPosts(string subreddit, int count)
        {
            if (string.IsNullOrEmpty(subreddit))
                throw new ArgumentException("subreddit is not valid");
            if (count <= 0)
                throw new ArgumentOutOfRangeException("count", "count is not valid");

            List<Post> result = new List<Post>();

            using (WebClient client = new WebClient())
            {
                string jsonUrl = string.Format("{0}?limit={1}", GetSubredditJsonUrl(subreddit), count);
                string rawDataText = await client.DownloadStringTaskAsync(jsonUrl);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> deserializeObject =
                    serializer.Deserialize<Dictionary<string, object>>(rawDataText);

                Dictionary<string, object> data = deserializeObject["data"] as Dictionary<string, object>;
                ArrayList children = data["children"] as ArrayList;
                for (int i = 0; i < count; i++)
                {
                    Dictionary<string, object> child = children[i] as Dictionary<string, object>;
                    Dictionary<string, object> childData = child["data"] as Dictionary<string, object>;
                    
                    Post post = ParsChild(childData);

                    result.Add(post);
                }
            }

            return result.ToArray();
        }

        public static Post ParsChild(Dictionary<string, object> child)
        {
            return new Post
                        {
                            DateTime = Services.ConvertToDateTime(Convert.ToInt64(child["created_utc"])),
                            Title = child["title"].ToString().Trim(),
                            Author = child["author"].ToString().Trim(),
                            Url = child["url"].ToString().Trim(),
                            Domain = child["domain"].ToString().Trim(),
                            PermanentLink = child["permalink"].ToString().Trim(),
                            Subreddit = child["subreddit"].ToString().Trim(),
                            RedditId = child["id"].ToString().Trim(),
                        };
        }

        public static string GetSubredditJsonUrl(string subreddit)
        {
            return String.Format("http://www.reddit.com/r/{0}.json", subreddit);
        }
    }
}