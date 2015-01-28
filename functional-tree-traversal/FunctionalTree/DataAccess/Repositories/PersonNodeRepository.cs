using Adbrain.DataAccess.DbContexts;
using Adbrain.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.DataAccess.Repositories
{
    public class PersonNodeRepository : IPersonNodeRepository
    {
        private readonly ISqlDbContext _dbContext;
        private readonly DbSet<PersonNode> _dbSet;

        public PersonNodeRepository(ISqlDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<PersonNode>();
        }

        public async Task<PersonNode> GetHead()
        {
            var head = await _dbSet.OrderBy(p => p.Id).FirstOrDefaultAsync();

            return head;
        }

        public async Task<bool> IsEmpty()
        {
            var head = await GetHead();
            
            return head == null;
        }

        public void Add(PersonNode person)
        {
            _dbSet.Add(person);
        }
    }
}
