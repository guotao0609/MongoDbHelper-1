using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbHelper.Core.UnitTest
{
    [TestClass]
    public class MongoClientTest
    {
        private MongoClient client = null;

        private string UserName = "TCFlyWalle";
        private string Password = "FG6Zkq9Q3T5XSiTwBBTB";
        private string Hosts = "10.100.173.185:15000,10.100.173.186:15000,10.100.173.187:15000";
        private string DbName = "TCFlyWalle";
        private string ReplicaSet = "fly07";

        [TestInitialize]
        public void Connect()
        {
            var connectStr = $"mongodb://{UserName}:{Password}@{Hosts}/{DbName}/?slaveOk=true&replicaSet={ReplicaSet}";
            client = new MongoClient(connectStr);
            Assert.IsNotNull(client);
        }

        [TestMethod]
        public void CanConnect()
        {
            Assert.IsNotNull(client);
        }


        [TestMethod]
        public void CanGetDataBase()
        {
            var db = client.GetDatabase(DbName);
            Assert.IsNotNull(db);
        }
    }
}
