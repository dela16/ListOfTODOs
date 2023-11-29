using ToDoList.Properties.DAL.Models;

namespace ListOfTODOs.Properties.DAL.Repositories.Interfaces
{
	public interface IRepository
	{
		public Task<bool> CreateItem(ToDoItem item);

		public Task<ToDoItem> UpdateItemById(ToDoItem item, int id);

		public Task<List<ToDoItem>> GetAllItems(); 
		public Task<ToDoItem> GetItemById(int id);

		public Task<bool> DeleteItemById(int id);
	}
}
