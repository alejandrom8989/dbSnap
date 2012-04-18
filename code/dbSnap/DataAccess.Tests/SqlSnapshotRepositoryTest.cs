using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace DataAccess.Tests
{
    [TestFixture]
    public class SqlSnapshotRepositoryTest
    {
        [Test]
        public void ShouldRetrieveAllSnapshots()
        {
        }
    }
}

Continue here: add connection string to the app config and write a test for getting the snapshots, see it fail, and make it pass