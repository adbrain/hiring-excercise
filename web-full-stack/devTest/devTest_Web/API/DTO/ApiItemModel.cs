using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace devTest_Web.API.Model
{
    public class ApiItemModel
    {
        [XmlIgnore]
        [JsonIgnore]
        public string author { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public string domain { get; set; }
        public string id { get; set; }
        public string createdDate { get; set; }
        public string title { get; set; }
        public string permalink { get; set; }
    }
}
