using MongoDB.Driver;

namespace ToDoList.Properties.DAL.Storages
{
	public class AbstractStorage
	{
		private readonly IMongoDatabase _database;
		public AbstractStorage()
		{
			var settings = MongoClientSettings.FromConnectionString("mongodb+srv://deniceML:Apa123@todolist.zt8verq.mongodb.net/ToDoCluster?retryWrites=true&w=majority");
			var client = new MongoClient(settings);
			_database = client.GetDatabase("ToDoDb");
		}

		public IMongoDatabase GetItemDatabase()
		{
			return _database;
		}
	}
}
