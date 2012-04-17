namespace Domain.RepositoryInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Domain.DomainInterfaces;

    /// <summary>
    /// Snapshots data accessor.
    /// </summary>
    public interface ISnapshotRepository
    {
        IList<ISnapshot> GetSnapshots();

        void Delete(string snapshotName);

        void Delete(IList<string> snapshotNames);
    }
}
