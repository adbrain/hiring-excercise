using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adbrain.WebFullStack.DataAccessLayer
{
    public interface IRepository<T>
    {
        Task<int> SaveAll(IEnumerable<T> posts);
        IList<T> FromBatchNumber(string batchNumber);
    }
}