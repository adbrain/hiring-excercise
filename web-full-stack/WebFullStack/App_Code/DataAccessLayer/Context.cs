using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Adbrain.WebFullStack.Models;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        public Context()
            : base(GetConnectionString())
        {
            Database.SetInitializer<Context>(
                new MigrateDatabaseToLatestVersion<Context, ContextMigrationsConfiguration>(true));
        }

        public DbSet<Post> Posts { get; set; }

        public static string GetDataSource()
        {
            return ".\\SQLEXPRESS2";
        }

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder x = new SqlConnectionStringBuilder();
            x.DataSource = GetDataSource();
            x.AttachDBFilename = "|DataDirectory|\\db.mdf";
            x.IntegratedSecurity = true;
            x.MultipleActiveResultSets = true;

            return x.ConnectionString;
        }

        public static IEnumerable<T> GetAll<T>(params Expression<Func<T, IDbObject>>[] includes) where T : class, IDbObject
        {
            using (var ctx = new Context())
            {
                IQueryable<T> query = ctx.Set<T>();
                foreach (Expression<Func<T, IDbObject>> include in includes)
                    query = query.Include(include);
                return query;
            }
        }

        public static T Load<T, TP>(T obj, Expression<Func<T, ICollection<TP>>> collection)
            where T : class
            where TP : class
        {
            using (var ctx = new Context())
            {
                ctx.Set<T>().Attach(obj);
                ctx.Entry(obj).Collection(collection).Load();
            }
            return obj;
        }

        public static T FromId<T>(int id, params Expression<Func<T, IDbObject>>[] includes) where T : class , IDbObject
        {
            using (var ctx = new Context())
            {
                IQueryable<T> query = ctx.Set<T>();
                foreach (Expression<Func<T, IDbObject>> include in includes)
                    query = query.Include(include);
                query = query.Where(x => x.Id == id);
                return query.FirstOrDefault();
            }
        }

        public static void Save<T>(T obj) where T : class
        {
            using (var ctx = new Context())
            {
                ctx.Set<T>().Attach(obj);
                ctx.Entry(obj).State = EntityState.Added;
                ctx.SaveChanges();
            }
        }

        public static void Update<T>(T obj) where T : class
        {
            using (var ctx = new Context())
            {
                ctx.Set<T>().Attach(obj);
                ctx.Entry(obj).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void Delete<T>(T obj) where T : class
        {
            using (var ctx = new Context())
            {
                ctx.Entry(obj).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public static void SaveOrUpdate<T>(T obj) where T : class, IDbObject
        {
            if (obj.Id == 0)
                Save(obj);
            else
                Update(obj);
        }
    }

    public class ContextMigrationsConfiguration : DbMigrationsConfiguration<Context>
    {
        public ContextMigrationsConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }
    }
}