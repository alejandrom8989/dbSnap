namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Domain.DomainInterfaces;
    using Domain.RepositoryInterfaces;

    /// <summary>
    /// Provides snapshots.
    /// </summary>
    public class SnapshotService
    {
        /// <summary>
        /// Repository used for data access.
        /// </summary>
        protected ISnapshotRepository repository;

        /// <summary>
        /// Instantiates a repository that uses the given <see cref="ISnapshotRepository"/>
        /// </summary>
        /// <param name="snapshotRepository">The <see cref="ISnapshotRepository"/>. Cannot be null.</param>
        public SnapshotService(ISnapshotRepository snapshotRepository)
        {
            if (snapshotRepository == null)
            {
                throw new ArgumentNullException("snapshotRepository");
            }

            repository = snapshotRepository;
        }

        /// <summary>
        /// Returns all <see cref="ISnapshot"/>s in the given repository.
        /// </summary>
        public IList<ISnapshot> GetSnapshots()
        {
            return repository.GetSnapshots();
        }

        /// <summary>
        /// Deletes the given <see cref="ISnapshot"/>.
        /// </summary>
        /// <param name="snapshot"></param>
        public void DeleteSnapshot(ISnapshot snapshot)
        {
            repository.Delete(snapshot.Name);
        }
    }
}
