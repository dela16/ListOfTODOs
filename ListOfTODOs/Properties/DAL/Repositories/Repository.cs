using ListOfTODOs.Properties.DAL.Contexts;
using ListOfTODOs.Properties.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ToDoList.Properties.DAL.Models;

namespace ListOfTODOs.Properties.DAL.Repositories
{
	public class Repository : IRepository
	{
		private readonly ToDoListDbContext _dbContext;

		public Repository(ToDoListDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> CreateItem(ToDoItem item)
		{
			var result = await _dbContext.Items.AddAsync(item);

			if (result is null)
				Results.BadRequest("You have to add an item.");
			//TODO Vill vi att programmet ska krascha and throw exception? 

			await _dbContext.SaveChangesAsync();
			return true; 
		}

		public async Task<ToDoItem> UpdateItemById(ToDoItem updatedItem, int id)
		{
			var existingItem = await _dbContext.Items.FindAsync(id);

			if (existingItem.Id != id)
				Results.BadRequest("Sorry, no item with that id.");
			//TODO Vill vi att programmet ska krascha and throw exception? 

			existingItem.Activity = updatedItem.Activity;

			_dbContext.Items.Update(existingItem);
			await _dbContext.SaveChangesAsync();
			return existingItem; 
		}

		public async Task<List<ToDoItem>> GetAllItems()
		{
			if (_dbContext.Items.Count() < 0)
				Results.BadRequest("No items in list.");

			return await _dbContext.Items.ToListAsync(); 
		}

		public async Task<bool> DeleteItemById(int id)
		{
			var itemToBeDeleted = await _dbContext.Items.FindAsync(id);

			if (itemToBeDeleted is null)
			{
				Results.BadRequest("You have to chose an id");
			}
			else if (itemToBeDeleted.Id != id)
			{
				Results.BadRequest("No item has that id.");
			}

			_dbContext.Remove(itemToBeDeleted);
			await _dbContext.SaveChangesAsync();
			return true; 
		}
	}
}
