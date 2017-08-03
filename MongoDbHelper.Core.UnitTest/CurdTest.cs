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
using MongoDB.Bson;

namespace MongoDbHelper.Core.UnitTest
{
    [TestClass]
    public class CurdTest
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
            MongoDbClient.Instance.Insert(context);
        }


        [TestMethod]
        public void TestInsert10000_SingleBySingle()
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

        /// <summary>
        /// 批量插入
        /// </summary>
        [TestMethod]
        public void TestInsert10000_Batch()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var list = new List<WriteModel<OrderContext>>();
            for (int i = 0; i <= 10000; i++)
            {
                OrderContext context = new OrderContext();
                context.Name = (i+1).ToString();
                context.Users.Add(new CustomUser
                {
                    UserType = "B",
                    UserInfo = new User
                    {
                        UserName = "XiaoMing",
                        Department = "JiPiao",
                        Gender = "Boy"
                    }
                });
                context.Users.Add(new CustomUser
                {
                    UserType = "C",
                    UserInfo = new User
                    {
                        UserName = "XiaoMing",
                        Department = "JiPiao",
                        Gender = "Girl"
                    }
                });
                list.Add(new InsertOneModel<OrderContext>(context));
            }
            var col = MongoDbClient.Instance.GetDefaultCollection<OrderContext>();
            var result = col.BulkWrite(list);
            Console.WriteLine(result.ToJson());
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
        }

        [TestMethod]
        public void TestFind_Page()
        {
            int pageSize = 100;
            int pageIndex = 10;
            var col = MongoDbClient.Instance.GetDefaultCollection<OrderContext>();
            var query = col.AsQueryable<OrderContext>()
            .OrderBy(item => item.Id)
            .Where(p => p.Name.Length < 3)
            .Skip(pageSize * (pageIndex - 1))
            .Take(pageSize);
            var result = query.ToList();
        }


        [TestMethod]
        public void TestFind_AnyWhere()
        {
            string name = "";
            int pageSize = 100;
            int pageIndex = 10;
            var col = MongoDbClient.Instance.GetDefaultCollection<OrderContext>();
            var query = col.AsQueryable<OrderContext>().OrderBy(item => item.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p=> p.Name == name);
            }
            query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize); ;
            var result = query.ToList();
        }

        [TestMethod]
        public void TestUpdate()
        {

            var update = Builders<OrderContext>.Update.Set(p => p.Users[1].UserType, "E");
            var result = MongoDbClient.Instance.UpdateMany<OrderContext>(p => p.Name == "test", update);
        }

        [TestMethod]
        public void TestDelete()
        {
            var result = MongoDbClient.Instance.DeleteMany<OrderContext>(p => p.Name == "xufenghui" || p.Name == "wangxing");
        }
    }
}
