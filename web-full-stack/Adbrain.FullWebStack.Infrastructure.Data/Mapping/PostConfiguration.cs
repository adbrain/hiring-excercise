using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Adbrain.FullWebStack.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adbrain.FullWebStack.Infrastructure.Data.Mapping
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Domain).IsRequired();
            Property(t => t.Title).IsRequired();
            Property(t => t.CreatedUtc).IsRequired();
            Property(t => t.Author).IsRequired();
        }
    }
}
