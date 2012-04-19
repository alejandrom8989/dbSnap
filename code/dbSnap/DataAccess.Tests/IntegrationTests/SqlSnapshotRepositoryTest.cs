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
        public static void ClassInitialize(TestContext testContext)
        {
            CreateTestDatabases();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DropTestDatabases();
        }

        [TestMethod]
        public void ShouldRetrieveAllSnapshots()
        {
                var repository = new SqlSnapshotRepository(ConnectionString);
                var shots = repository.GetSnapshots();

                Assert.AreEqual(0, shots.Count);
         
            try
            {
                CreateSnapshot(TestDbName + '0', "0snap0");
                CreateSnapshot(TestDbName + '0', "0snap1");
                CreateSnapshot(TestDbName + '2', "2snap0");

                shots = repository.GetSnapshots();

                Assert.AreEqual(3, shots.Count, "There should be 3 snapshots");
                Assert.AreEqual(1, shots.Count(shot => shot.Name == "0snap0"), "There should be a snapshot called 0snap0");
                Assert.AreEqual(1, shots.Count(shot => shot.Name == "0snap1"), "There should be a snapshot called 0snap1");
                Assert.AreEqual(1, shots.Count(shot => shot.Name == "2snap0"), "There should be a snapshot called 2snap0");
            }
            finally
            {
                DeleteSnapshot("0snap0");
                DeleteSnapshot("0snap1");
                DeleteSnapshot("2snap0");
            }
        }

        private static void CreateSnapshot(string sourceDatabaseName, string snapshotName)
        {
            UseConnection(
                connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("create database {0} on ( name  = {1}, filename = 'C:\\{2}') as snapshot of {3}", snapshotName, GetDatabaseDataName(sourceDatabaseName), snapshotName + ".ss", sourceDatabaseName);
                    command.ExecuteNonQuery();
                }
                , ex => Assert.Fail(string.Format("Exception while dropping snapshot {0}: {1}", snapshotName, ex.Message)));
        }

        private static void DeleteSnapshot(string snapshotName)
        {
            UseConnection(
                connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("drop database {0}", snapshotName);
                    command.ExecuteNonQuery();
                }
                , ex => Assert.Fail(string.Format("Exception while dropping snapshot {0}: {1}", snapshotName, ex.Message)));
        }

        private static void CreateTestDatabases()
        {
            UseConnection(
                connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("create database {0}0;create database {0}1;create database {0}2;", TestDbName);
                    command.ExecuteNonQuery();
                }
                , ex => Assert.Fail(string.Format("Exception while creating test databases: {0}", ex.Message)));
        }

        private static string GetDatabaseDataName(string databaseName)
        {
            string result = null;
            UseConnection(
                connection =>
                {
                    var commmand = connection.CreateCommand();
                    commmand.CommandText = string.Format("select files.name from master.sys.master_files files join sys.databases databases on files.database_id = databases.database_id where databases.name = '{0}' and type_desc = 'rows'", databaseName);
                    result = commmand.ExecuteScalar().ToString();
                }
                , ex => Assert.Fail(string.Format("Exception while obtaining the database data name for {0}: {1}", databaseName, ex.Message)));
            return result;
        }

        private static void DropTestDatabases()
        {
            UseConnection(
                connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("drop database {0}0;drop database {0}1;drop database {0}2;", TestDbName);
                }
                , ex => Assert.Fail(string.Format("Exception while dropping test databases: {0}", ex.Message)));
        }

        private static void UseConnection(Action<SqlConnection> usage, Action<Exception> onError)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (connection)
                    {
                        connection.Open();
                        usage.Invoke(connection);
                    }
                }
                catch (Exception e)
                {
                    onError.Invoke(e);
                }
            }
        }
    }
}
