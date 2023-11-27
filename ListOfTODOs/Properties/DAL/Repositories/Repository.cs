using ListOfTODOs.Properties.DAL.Contexts;
using ListOfTODOs.Properties.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
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
			await _dbContext.Items.AddAsync(item); //Sätt i variabel senare så du kan kolla så den inte är null osv. 
			await _dbContext.SaveChangesAsync();
			return true; 
		}

		public async Task<List<ToDoItem>> GetAllItems()
		{
			return await _dbContext.Items.ToListAsync(); 
		}
	}
}
