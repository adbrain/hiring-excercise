using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using POCO;

namespace DAL
{
    public class RedditPostConfiguration : EntityTypeConfiguration<RedditPost>
    {
        public RedditPostConfiguration()
        {
            // Sql Server's index's key cannot exceed a total size of 900 bytes.
            Property(t => t.id).HasMaxLength(200);

            HasKey(t => new { t.RequestId, t.id });
            Property(e => e.RequestId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(e => e.RequestId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
