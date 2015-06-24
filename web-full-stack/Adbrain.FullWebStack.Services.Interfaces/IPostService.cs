using Adbrain.FullWebStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.FullWebStack.Service.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<IGrouping<string, Post>>> GetGroupedByAuthor(string domain);
    }
}
