using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbHelper.Core.UnitTest
{
    public class OrderTemplate : OrderTemplateBase
    {
        public List<Guid> ElementGuid = new List<Guid>();
    }

    public enum ElememtCategory
    {
        Input,
        User,
        Select
    }

    public class OrderElement
    {
        public Guid Id = new Guid();
        public string Name { get; set; } = string.Empty;
        public ElememtCategory Category { get; set; } = ElememtCategory.Input;
        public IEnumerable<T> Source = null;

        public string Desc = string.Empty;
    }

    public class WalleUser
    {

    }

    public class Store
    { 
        public void Create(string name , ElememtCategory category , IEnumerable<string> options = null)
        {
            if (category == ElememtCategory.User)
            {
                OrderElement<WalleUser> usersElement = new OrderElement<WalleUser>();
                usersElement.Name = name;
                //....
            }

            if (category == ElememtCategory.Select)
            {
                OrderElement<string> selectElement = new OrderElement<string>();
                selectElement.Source = options;
                //....
            }
        }   
    }
}
