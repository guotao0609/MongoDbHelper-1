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
        private MongoDbClient()
        {
            try
            {
                MongoClientSettings set = new MongoClientSettings();
                List<MongoServerAddress> address = new List<MongoServerAddress>();
                List<MongoCredential> lstCredential = new List<MongoCredential>();
                //加载配置
                string serverIP = MongodbConfig.Instance.ServerIP;
                string CredentialsStr = MongodbConfig.Instance.Credentials;
                string connMode = MongodbConfig.Instance.ConnMode;
                if (string.IsNullOrEmpty(serverIP))
                {
                    address.Add(new MongoServerAddress("127.0.0.1", 27017));
                }
                else
                {
                    string[] servers = serverIP.Split('|');
                    if (servers.Length > 1)
                    {
                        //集群模式
                        foreach (var item in servers)
                        {
                            string[] unit = item.Split(':');
                            if (unit.Length == 2)
                            {
                                address.Add(new MongoServerAddress(unit[0], int.Parse(unit[1])));
                            }
                        }
                        set.Servers = address;
                        set.ReadPreference = ReadPreference.SecondaryPreferred;
                        if (connMode == "Shard")
                        {
                            set.ConnectionMode = ConnectionMode.ShardRouter;
                        }
                        else
                        {
                            set.ConnectionMode = ConnectionMode.ReplicaSet;
                        }
                    }
                    else
                    {
                        string[] unit = servers[0].Split(':');
                        set.Server = new MongoServerAddress(unit[0], int.Parse(unit[1]));
                        set.ConnectionMode = ConnectionMode.Direct;
                    }
                }
                if (!string.IsNullOrEmpty(CredentialsStr))
                {
                    string[] credentials = CredentialsStr.Split('|');
                    foreach (var item in credentials)
                    {
                        string[] unit = item.Split('-');
                        if (unit.Length >= 3)
                        {
                            //支持认证模式定义
                            if (unit.Length == 4)
                            {
                                string authMode = unit[3];//CR,SHA
                                if (authMode == "CR")
                                {
                                    lstCredential.Add(MongoCredential.CreateMongoCRCredential(unit[0], unit[1], unit[2]));
                                }
                            }
                            else
                            {
                                //默认CR模式认证
                                lstCredential.Add(MongoCredential.CreateMongoCRCredential(unit[0], unit[1], unit[2]));
                            }
                        }
                    }
                    set.Credentials = lstCredential;
                }
                set.MaxConnectionPoolSize = 1000;
                client = new MongoClient(set);
            }
            catch (Exception ex)
            {
                SkyNetLogger.LogError(new SkyNetMarker(), "连接MongoDb发生异常", ex);
            }
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
            return client.GetDatabase("WalleDevOps");
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