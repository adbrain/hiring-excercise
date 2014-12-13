using Adbrain.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.DataAccess.DbContexts
{
    public class SqlDbContext : DbContext, ISqlDbContext
    {
        public SqlDbContext() : base("SqlDbContext")
        {
            Database.SetInitializer<SqlDbContext>(new CreateDatabaseIfNotExists<SqlDbContext>());
            //Database.SetInitializer<SqlDbContext>(new DropCreateDatabaseIfModelChanges<SqlDbContext>());
            //Database.SetInitializer<SqlDbContext>(new DropCreateDatabaseAlways<SqlDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonNode>()
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<PersonNode>()
                .HasOptional(p => p.LeftChild)
                .WithMany()
                .HasForeignKey(p => p.LeftChildId);
            modelBuilder.Entity<PersonNode>()
                .HasOptional(p => p.RightChild)
                .WithMany()
                .HasForeignKey(p => p.RightChildId);
        }

        public int Save()
        {
            try
            {
                return this.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw dbEx;
            }
        }
    }
}
