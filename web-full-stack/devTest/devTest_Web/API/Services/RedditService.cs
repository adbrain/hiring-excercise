using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using devTest_Web.API.Interfaces;
using devTest_Web.API.Model;
using devTest_Web.DataModels;
using Newtonsoft.Json;

namespace devTest_Web.API.Services
{
    public class RedditService : ServiceBase, IApiService
    {
        public int SaveApiData()
        {
            var data = this.LoadApiData();

            dynamic result = JsonConvert.DeserializeObject(data);

            var requestId = -1;
            using (var db = new Entities())
            {
                db.ApiStorages.Add(new ApiStorage() { ApiData = data });
                db.SaveChanges();
               
                requestId = db.ApiStorages.ToList().OrderBy(a => a.Id).Last().Id;
            }

            return requestId;
        }

        public List<ApiItemModel> GetApiData(int requestId)
        {
            var jsonData = "";

            using (var db = new Entities())
            {
                jsonData = db.ApiStorages.Where(a => a.Id == requestId).Single().ApiData;
            }

            dynamic result = JsonConvert.DeserializeObject(jsonData);

            return this.GetChildrenList(result.data.children);
        }
    }
}