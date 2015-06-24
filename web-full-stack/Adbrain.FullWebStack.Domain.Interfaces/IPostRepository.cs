using Adbrain.FullWebStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.FullWebStack.Domain.Interfaces
{
    public interface IPostRepository : IDisposable
    {
        IEnumerable<IGrouping<string, Post>> GetGroupedByAuthor(string domain);
        void InsertAll(IEnumerable<Post> post);
        void DeleteAll();
        int SaveChanges();
    }
}
