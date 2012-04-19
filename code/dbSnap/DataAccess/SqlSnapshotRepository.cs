namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using Domain.DomainInterfaces;
    using Domain.RepositoryInterfaces;

    public class SqlSnapshotRepository : ISnapshotRepository
    {
        private string connectionString;

        public SqlSnapshotRepository(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            this.connectionString = connectionString;
        }

        public IList<ISnapshot> GetSnapshots()
        {
            // TODO write this. the test for this method is already written. It uses a lot of sql stuff, so perhaps have a look...
            throw new NotImplementedException();
        }

        public void Delete(string snapshotName)
        {
            throw new NotImplementedException();
        }

        public void Delete(IList<string> snapshotNames)
        {
            throw new NotImplementedException();
        }
    }
}
