// -----------------------------------------------------------------------
// <copyright file="IDatabaseRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Domain.RepositoryInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Domain.DomainInterfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IDatabaseRepository
    {
        IList<IDatabase> GetDatabases();
    }
}
