using Adbrain.WebApi.Models.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.WebApi.Models.Services
{
    public interface IPersonService
    {
        void Insert(string name, int age);

        Person Find(string name, int age);
    }
}
