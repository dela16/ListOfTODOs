using MongoDB.Driver;
using ToDoList.Properties.DAL.Models;

namespace ToDoList.Properties.DAL.Storages
{
	public class ItemStorage : AbstractStorage
	{
		public List<ToDoItem> GetAllItems()
		{
			var collection = GetItemDatabase().GetCollection<ToDoItem>("ToDoItem");
			var allItems = collection.Find(_=> true).ToList();
			return allItems;
		}

		public bool CreateItem(ToDoItem item)
		{
			var collection = GetItemDatabase().GetCollection<ToDoItem>("ToDoItem");
			collection.InsertOne(item); 
			return true;
		}

	}
}
