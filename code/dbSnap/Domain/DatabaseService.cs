// -----------------------------------------------------------------------
// <copyright file="DatabaseService.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Domain.DomainInterfaces;
    using Domain.RepositoryInterfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseService
    {
        private readonly IDatabaseRepository repository;
        
        public DatabaseService(IDatabaseRepository databaseRepository)
        {
            if (databaseRepository == null)
            {
                throw new ArgumentNullException("databaseRepository");
            }

            this.repository = databaseRepository;
        }

        public IList<IDatabase> GetDatabases()
        {
            return repository.GetDatabases();
        }
    }
}
