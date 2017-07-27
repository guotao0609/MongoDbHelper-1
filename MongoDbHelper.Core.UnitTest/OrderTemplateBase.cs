namespace MongoDbHelper.Core.UnitTest
{
    public class OrderTemplateBase
    {
        public object Id { get; set; } 

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;


        public string creator { get; set; } = string.Empty;
        
    }
}