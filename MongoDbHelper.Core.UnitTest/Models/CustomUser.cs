using System;

namespace MongoDbHelper.Core.UnitTest.Models
{
    public class CustomUser
    {
        public string UserType { get; set; } = string.Empty;

        public User UserInfo { get; set; } = new User();
    }
}