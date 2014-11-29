using Adbrain.FullWebStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.FullWebStack.Infrastructure.Data
{
    public interface IDbContext
    {
        DbSet<Post> Posts { get; set; }
        void SetAsAdded(Post post);
        void SetAsModified(Post post);
        int SaveChanges();
        void Dispose();
    }
}
