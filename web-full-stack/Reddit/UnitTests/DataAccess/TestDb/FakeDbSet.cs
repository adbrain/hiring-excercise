using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adbrain.Reddit.UnitTests.DataAccess.TestDb
{
    public class FakeDbSet<T> : IDbSet<T>, IDbAsyncEnumerable<T> where T : class
    {
        readonly ObservableCollection<T> _data;
        readonly IQueryable _queryable;

        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _queryable = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _queryable.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _queryable.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new AsyncQueryProviderWrapper<T>(_queryable.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new AsyncEnumeratorWrapper<T>(_data.GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }
    } 
}
