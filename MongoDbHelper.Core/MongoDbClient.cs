using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using TCBase.Component;
using MongoDbHelper.Core.Common.Components;

namespace MongoDbHelper.Core
{
    public class MongoDbClient
    {
        private static MongoDbClient dbclient = null;
        public MongoClient client = null;

        private string UserName = "TCFlyWalle";
        private string Password = "FG6Zkq9Q3T5XSiTwBBTB";
        private string Hosts = "10.100.173.185:15000,10.100.173.186:15000,10.100.173.187:15000";
        private string DbName = "TCFlyWalle";
        private string ReplicaSet = "fly07";

        private MongoDbClient()
        {
            var connectStr = $"mongodb://{UserName}:{Password}@{Hosts}/{DbName}/?slaveOk=true&replicaSet={ReplicaSet}";
            client = new MongoClient(connectStr);
        }

        public static MongoDbClient Instance
        {
            get
            {
                if (dbclient != null && dbclient.client != null)
                {
                    return dbclient;
                }
                else
                {
                    dbclient = new MongoDbClient();
                    return dbclient;
                }
            }
        }

        public IMongoDatabase GetDefaultDataBase()
        {
            return client.GetDatabase(DbName);
        }

        public IMongoCollection<T> GetDefaultCollection<T>()
        {
            var database = GetDefaultDataBase();
            var type = typeof(T);
            var collectionName = type.Name;
            var collection = database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}