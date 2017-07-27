using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDbHelper;
using MongoDbHelper.Core;
using MongoDbHelper.Core.UnitTest.Models;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Diagnostics;

namespace MongoDbHelper.Core.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInsert()
        {

            OrderContext context = new OrderContext();
            context.Name = "xufenghui";
            context.Users.Add(new CustomUser
            {
                UserType = "A",
                UserInfo = new User
                {
                    UserName = "XiaoMing",
                    Department = "JiPiao",
                    Gender = "Boy"
                }
            });
            context.Users.Add(new CustomUser
            {
                UserType = "A",
                UserInfo = new User
                {
                    UserName = "XiaoMing",
                    Department = "JiPiao",
                    Gender = "Girl"
                }
            });
            OrderContext context2 = new OrderContext();
            context2.Name = "wangxing";
            context2.Users.Add(new CustomUser
            {
                UserType = "B",
                UserInfo = new User
                {
                    UserName = "XiaoMing",
                    Department = "JiPiao",
                    Gender = "Boy"
                }
            });
            context2.Users.Add(new CustomUser
            {
                UserType = "C",
                UserInfo = new User
                {
                    UserName = "XiaoMing",
                    Department = "JiPiao",
                    Gender = "Girl"
                }
            });

            MongoDbClient.Instance.Insert(context);
            MongoDbClient.Instance.Insert(context2);

            MongoDbClient.Instance.Insert<User>(new User { Department = "gnjp", Gender = "boy", UserName = "xufenghui" });
        }


        [TestMethod]
        public void TestInsert100000()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i <= 10000; i++)
            {
                TestInsert();
            }
            watch.Start();
            var eli = watch.Elapsed.TotalMilliseconds;
        }

        [TestMethod]
        public void TestFind()
        {
            //使用表达式树
            Expression<Func<OrderContext, bool>> filter = (p) => p.Users.Any(user => user.UserType == "C");
            var result1 = MongoDbClient.Instance.Find<OrderContext>(filter);
            //直接使用表达式
            var result2 = MongoDbClient.Instance.Find<OrderContext>(p => p.Users.Any(user => user.UserType == "B"));

            /// var query = from xxx select *;

        }


        [TestMethod]
        public void TestUpdate()
        {

            var update = Builders<OrderContext>.Update.Set(p => p.Users[1].UserType, "E");
            var result = MongoDbClient.Instance.UpdateMany<OrderContext>(p => p.Name == "test", update);

            //var update2 = Builders<OrderContext>.Update.Set(p => p.Name, "test").Set(p => p.Users[-1].UserType,"E") ;
            //MongoDbClient.Instance.UpdateMany<OrderContext>(p => p.Users.Any(user => user.UserType == "C"), update2);
        }

        [TestMethod]
        public void TestDelete()
        {
            var result = MongoDbClient.Instance.DeleteMany<OrderContext>(p => p.Name == "xufenghui" || p.Name == "wangxing");
        }
    }
}
