using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Core;
using System.Linq.Expressions;

namespace MongoDbHelper.Core
{
    public static class MongoDbHelper
    {
        public static void Insert<T>(this MongoDbClient dbclient, T obj)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            collection.InsertOne(obj);
        }

        public static IEnumerable<T> Find<T>(this MongoDbClient dbclient, Expression<Func<T, bool>> filter)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.Find<T>(filter);
            return result?.ToList();
        }
        public static bool UpdateOne<T>(this MongoDbClient dbclient, Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.UpdateOne<T>(filter, update);
            return result.IsAcknowledged;
        }
        public static bool UpdateMany<T>(this MongoDbClient dbclient, Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.UpdateMany<T>(filter, update);
            return result.IsAcknowledged;
        }
        public static bool DeleteOne<T>(this MongoDbClient dbclient, Expression<Func<T, bool>> filter)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.DeleteOne<T>(filter);
            return true;
        }

        public static bool DeleteMany<T>(this MongoDbClient dbclient, Expression<Func<T, bool>> filter)
        {
            var collection = dbclient.GetDefaultCollection<T>();
            var result = collection.DeleteMany<T>(filter) ;
            return true;
        }

        public class Person
        {
            public string Name;
            public string Gender;
        }
    }
}
