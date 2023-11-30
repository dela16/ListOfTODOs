using ListOfTODOs.Properties.DAL.Contexts;
using ListOfTODOs.Properties.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
			if (item.Activity.IsNullOrEmpty())
			{
				Results.BadRequest("Activity cannot be empty.");
				return false; 
			}

			await _dbContext.Items.AddAsync(item);

			var result = await _dbContext.SaveChangesAsync();

			if(result > 0) return true;

			return false;
		}

		public async Task<bool> UpdateItemById(ToDoItem updatedItem, int id)
		{
			var existingItem = await _dbContext.Items.FindAsync(id);

			if (existingItem is null || existingItem.Id != id || existingItem.Id < 0)
				return false;
				
			existingItem.Activity = updatedItem.Activity;

			_dbContext.Items.Update(existingItem);
			var result = await _dbContext.SaveChangesAsync();

			if(result > 0) return true;

			return false;
		}

		public async Task<List<ToDoItem>?> GetAllItems()
		{
			return await _dbContext.Items.ToListAsync();
		}

		public async Task<ToDoItem?> GetItemById(int id)
		{
			return await _dbContext.Items.FindAsync(id);
		}

		public async Task<bool> DeleteItemById(int id)
		{
			var itemToBeDeleted = await _dbContext.Items.FindAsync(id);

			if (itemToBeDeleted is null || itemToBeDeleted.Id != id|| itemToBeDeleted.Id < 0)
				return false;	
			

			_dbContext.Remove(itemToBeDeleted);

			var result = await _dbContext.SaveChangesAsync();

			if(result > 0) return true;

			return false; 
		}
	}
}
