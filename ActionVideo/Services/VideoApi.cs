using ActionVideo.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ActionVideo.Services
{
    public class VideoApi
    {
        private readonly HttpClient httpClient = DependencyService.Get<HttpClient>();

        async Task<string> GetContent(string url)
        {
            var res = await httpClient.GetStringAsync(url);
            if (string.IsNullOrEmpty(res))
            {
                return string.Empty;
            }
            if (res.StartsWith("{") && res.EndsWith("}"))
            {
                return res;
            }
            var match = Regex.Match(res, ">(\\{.+?\\})<");
            if (!match.Success)
            {
                return res;
            }
            return match.Groups[1].Value;
        }
        public async Task<(IList<Category>, IList<VideoItem>)> GetHomeVideos()
        {
            var content = await GetContent("https://avninga.com/");
            if (string.IsNullOrEmpty(content))
            {
                return (new Category[0], new VideoItem[0]);
            }
            var root = JObject.Parse(content);
            var categories = new List<Category>();
            var list = root["props"]["pageProps"]["homePageData"]["categories"].Children().SelectMany(items =>
            {
                var type = items["vod_type"];
                var category = new Category() { TypeId = type.Value<int>("id"), TypeName = type.Value<string>("name") };
                categories.Add(category);
                return items["vods"].Children().Select(it => new VideoItem()
                {
                    Pic = it.Value<string>("vod_pic"),
                    Name = it.Value<string>("vod_name"),
                    DateTime = it.Value<string>("vod_time_add").Substring(0, 10),
                    PlayUrl = it.Value<string>("vod_play_url")
                });
            });
            return (categories, list.ToArray());
        }

        public async Task<PageResult<VideoItem>> GetVideoPages(int type, int page)
        {
            var content = await GetContent($"https://avninga.com/vodtype/{type}/page/{page}");
            if (string.IsNullOrEmpty(content))
            {
                return new PageResult<VideoItem>() { Items = new VideoItem[0] };
            }
            var root = JObject.Parse(content);
            var pageToken = root.SelectToken("$.props.pageProps");
            var items = pageToken["data"].Children().Select(it => new VideoItem()
            {
                Pic = it.Value<string>("vod_pic"),
                Name = it.Value<string>("vod_name"),
                DateTime = it.Value<string>("vod_time_add").Substring(0, 10),
                PlayUrl = it.Value<string>("vod_play_url")
            }).ToArray();
            return new PageResult<VideoItem>() { Items = items, Total = pageToken["count"].Value<int>() };
        }

        public async Task<IList<VideoItem>> SearchVideos(string word, int page)
        {
            var content = await GetContent($"https://avninga.com/api/csearch?q={word}&page={page}&type=video");
            if (string.IsNullOrEmpty(content))
            {
                return new VideoItem[0];
            }
            var root = JObject.Parse(content);
            return root["vods"].Children().Select(it => new VideoItem()
            {
                Pic = it.Value<string>("vod_pic"),
                Name = it.Value<string>("vod_name"),
                DateTime = it.Value<string>("vod_time_add").Substring(0, 10),
                PlayUrl = it.Value<string>("vod_play_url")
            }).ToArray();
        }
    }
}
