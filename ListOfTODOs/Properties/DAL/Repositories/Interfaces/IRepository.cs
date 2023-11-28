﻿using ToDoList.Properties.DAL.Models;

namespace ListOfTODOs.Properties.DAL.Repositories.Interfaces
{
	public interface IRepository
	{
		Task<bool> CreateItem(ToDoItem item);

		public Task<List<ToDoItem>> GetAllItems();

		public Task<bool> DeleteItemById(int id);
	}
}
