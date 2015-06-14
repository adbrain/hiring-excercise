using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using devTest_Web.API.Model;
using devTest_Web.Utils;

namespace devTest_Web.API.Services
{
    public abstract class ServiceBase
    {
        public string ServiceEndpointUrl = ConfigurationManager.AppSettings["ServiceEndpointUrl"];
        public string RestPath = ConfigurationManager.AppSettings["RestPath"];

        public string LoadApiData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ServiceEndpointUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(this.RestPath).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }

            return string.Empty;
        }

        public List<ApiItemModel> GetChildrenList(dynamic children)
        {
            var resList = new List<ApiItemModel>();
            foreach (var item in children)
            {
                var dataItem = item.data;
                var itemModel = new ApiItemModel()
                {
                    author = dataItem.author,
                    domain = dataItem.domain,
                    id = dataItem.id,
                    createdDate = DateTimeUtil.FromUnixTime((long)dataItem.created).ToString("o"),
                    permalink = dataItem.permalink,
                    title = dataItem.title
                };

                resList.Add(itemModel);
            }

            return resList;
        }
    }
}