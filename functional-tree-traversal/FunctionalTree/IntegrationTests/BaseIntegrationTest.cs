using Adbrain.DataAccess.DbContexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Adbrain.IntegrationTests
{
    public class BaseIntegrationTest
    {
        private TransactionScope _transactionScope;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            using (var dbContext = new SqlDbContext())
            {
                dbContext.Database.CreateIfNotExists();
            }
        }

        [SetUp]
        public void SetUp()
        {
            // I wrap each test in a transaction so that they don't leave
            // any state in the database that can influence other tests.
            _transactionScope = new TransactionScope();
        }

        [TearDown]
        public void TearDown()
        {
            _transactionScope.Dispose();
            _transactionScope = null;
        }
    }
}
