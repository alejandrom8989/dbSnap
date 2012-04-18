using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests.IntegrationTests
{
    [TestClass]
    public class SqlSnapshotRepositoryTest
    {
        private const string TestDbName = "SqlSnapshotRepositoryTestDatabse";

        private static string ConnectionString
        {
            get { return ConfigurationManager.AppSettings["connectionString"]; }
        }

        [ClassInitialize]
        public void ClassInitialize()
        {
            CreateTestDatabases();
        }

        [ClassCleanup]
        public void ClassCleanup()
        {
            DropTestDatabases();
        }

        [TestMethod]
        public void ShouldRetrieveAllSnapshots()
        {
            var repository = new SqlSnapshotRepository(ConnectionString);
            var shots = repository.GetSnapshots();

            Assert.AreEqual(0, shots.Count);

            CreateSnapshot(TestDbName + '0', "0snap0");
            CreateSnapshot(TestDbName + '0', "0snap1");
            CreateSnapshot(TestDbName + '2', "2snap0");

            shots = repository.GetSnapshots();

            Assert.AreEqual(3, shots.Count);
            Assert.AreEqual(1, shots.Count(shot => shot.Name == "0snap0"));
            Assert.AreEqual(1, shots.Count(shot => shot.Name == "0snap1"));
            Assert.AreEqual(1, shots.Count(shot => shot.Name == "2snap0"));
        }

        private void CreateSnapshot(string sourceDatabaseName, string snapshotName)
        {
            // TODO code this now
        }

        private void DeleteSnapshot(string snapshotName)
        {
            UseConnection(
                connection => {
                        var command = connection.CreateCommand();
                        command.CommandText = string.Format("drop database {0}", snapshotName);
                }
                , ex => Assert.Fail(string.Format("Exception while dropping snapshot {0}: {1}", snapshotName, ex.Message)));
        }

        private void CreateTestDatabases()
        {
            UseConnection(
                connection => {
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("create database {0}0", TestDbName);
                    command.CommandText = string.Format("create database {0}1", TestDbName);
                    command.CommandText = string.Format("create database {0}2", TestDbName);
                }
                , ex => Assert.Fail(string.Format("Exception while creating test databases: {0}", ex.Message)));
        }

        private void DropTestDatabases()
        {
            UseConnection(
                connection => {
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("drop database {0}0", TestDbName);
                    command.CommandText = string.Format("drop database {0}1", TestDbName);
                    command.CommandText = string.Format("drop database {0}2", TestDbName);        
                }
                , ex => Assert.Fail(string.Format("Exception while dropping test databases: {0}", ex.Message)));
        }

        private void UseConnection(Action<SqlConnection> usage, Action<Exception> onError)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    usage.Invoke(connection);
                }
                catch (Exception e)
                {
                    onError.Invoke(e);
                }
            }
        }
    }
}
