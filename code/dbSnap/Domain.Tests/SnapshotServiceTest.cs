using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    using Domain.DomainInterfaces;
    using Domain.RepositoryInterfaces;

    using Moq;

    [TestClass]
    public class SnapshotServiceTest
    {
        [TestMethod]
        public void ShouldGetAllSnapshots()
        {
            var snapshots = GetTestSnapshots();
            var snapshotRepository = new Mock<ISnapshotRepository>();
            snapshotRepository.Setup(rep => rep.GetSnapshots()).Returns(snapshots);
            
            var service = new SnapshotService(snapshotRepository.Object);
            var returnedSnapshots = service.GetSnapshots();

            Assert.AreEqual(2, returnedSnapshots.Count, "There should be 2 snapshots.");
            Assert.AreEqual(1, returnedSnapshots.Count(shot => shot.Name == "first"), "There should be a snapshot called first");
            Assert.AreEqual(1, returnedSnapshots.Count(shot => shot.Name == "second"), "There should be a snapshot called second");
        }

        [TestMethod]
        public void ShouldDeleteSnapshots()
        {
            var snapshots = GetTestSnapshots();
            var initialSnapshotsCount = snapshots.Count;
            var snapshotRepository = new Mock<ISnapshotRepository>();
            snapshotRepository.Setup(rep => rep.GetSnapshots()).Returns(snapshots);
            snapshotRepository.Setup(rep => rep.Delete(It.IsAny<string>()))
                .Callback((string name) => snapshots.Remove(snapshots.First(shot => shot.Name == name)));

            var service = new SnapshotService(snapshotRepository.Object);
            var nameToDelete = "second";
            service.DeleteSnapshot(snapshots.First(shot => shot.Name == nameToDelete));

            Assert.AreEqual(initialSnapshotsCount - 1, snapshots.Count, "There should be one less snapshot after deletion.");
            Assert.AreEqual(0, service.GetSnapshots().Count(shot => shot.Name == nameToDelete), 
                string.Format("The snapshot named {0} should have been deleted", nameToDelete));
        }

        /// <summary>
        /// Returns a list of two <see cref="ISnapshot"/> named "first" and "second"
        /// </summary>
        /// <returns>The list of <see cref="ISnapshot"/>.</returns>
        private static IList<ISnapshot> GetTestSnapshots()
        {
            var firstShot = new Mock<ISnapshot>();
            firstShot.Setup(shot => shot.Name).Returns("first");
            var secondShot = new Mock<ISnapshot>();
            secondShot.Setup(shot => shot.Name).Returns("second");
            return new List<ISnapshot> { firstShot.Object, secondShot.Object };
        }
    }
}
