﻿using Microsoft.EntityFrameworkCore;
using ToDoList.Properties.DAL.Models;

namespace ListOfTODOs.Properties.DAL.Contexts
{
	public class ToDoListDbContext : DbContext
	{
		public DbSet<ToDoItem> Items { get; set; }

		public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
		{

		}
	}
}
