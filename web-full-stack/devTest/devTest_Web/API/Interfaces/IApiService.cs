using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using devTest_Web.API.Model;

namespace devTest_Web.API.Interfaces
{
    interface IApiService
    {
        int SaveApiData();
        List<ApiItemModel> GetApiData(int requestId);
    }
}