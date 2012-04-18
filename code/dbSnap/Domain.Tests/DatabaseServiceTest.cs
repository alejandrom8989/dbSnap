using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Moq;

namespace Domain.Tests
{
    using Domain.DomainInterfaces;
    using Domain.RepositoryInterfaces;

    [TestFixture]
    public class DatabaseServiceTest
    {
        [Test]
        public void ShouldGetDatabases()
        {
            var repositoryMock = new Mock<IDatabaseRepository>();
            repositoryMock.Setup(r => r.GetDatabases()).Returns(GetDatabasesList());
            
            var service = new DatabaseService(repositoryMock.Object);
            var result = service.GetDatabases();

            Assert.AreEqual(4, result.Count, "There should be 4 databases.");
            Assert.AreEqual("db0", result[0].Name, "The first db should be 'db0'");
            Assert.AreEqual("db1", result[1].Name, "The second db should be 'db1'");
            Assert.AreEqual("db2", result[2].Name, "The third db should be 'db2'");
            Assert.AreEqual("db3", result[3].Name, "The fourth db should be 'db3'");
        }

        private static IList<IDatabase> GetDatabasesList()
        {
            return new List<IDatabase>
                {
                    GetDatabaseMock("db0").Object,
                    GetDatabaseMock("db1").Object,
                    GetDatabaseMock("db2").Object,
                    GetDatabaseMock("db3").Object,
                };

        }

        private static Mock<IDatabase> GetDatabaseMock(string name)
        {
            var mock = new Mock<IDatabase>();
            mock.Setup(db => db.Name).Returns(name);
            return mock;
        }
    }
}
