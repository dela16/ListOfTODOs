using FluentAssertions;
using ListOfTODOs.Properties.DAL.Contexts;
using ListOfTODOs.Properties.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using ToDoList.Properties.DAL.Models;

namespace ListOfTODOs.Tests
{
	public class ListOfTODOsTests
	{
		//Denna metod görs för att bygga upp tillgång till databasen
		private async Task<ToDoListDbContext> GetDbContext(int items)
		{
			var options = new DbContextOptionsBuilder<ToDoListDbContext>()
			   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			   .Options;

			var dbContext = new ToDoListDbContext(options);

			dbContext.Database.EnsureCreated();
			for (int i = 0; i < items; i++)
			{
				await dbContext.AddAsync(new ToDoItem(){ Id = i, Activity = "Activity" });
			}
			dbContext.SaveChanges();

			return dbContext;
		}

		[Fact]
		public async void ListOfTODOs_GetFiveItems_ShouldReturnListOfItems()
		{
			//ARRANGE
			var dbContext = await GetDbContext(5);
			var itemRepository = new Repository(dbContext);

			//ACT
			List<ToDoItem> result = await itemRepository.GetAllItems();
			//ASSERT

			result.Should().HaveCount(5);
			result.Should().NotBeNull();
		}

		[Fact]
		public async void ListOfTODOs_CreateItem_ShouldReturnBool()
		{
			//ARRANGE
			var dbContext = await GetDbContext(5);
			var itemRepository = new Repository(dbContext);

			//ACT
			var item = new ToDoItem()
			{
				Id = 1,
				Activity = "Ringa Carina"
			};

			var result = await itemRepository.CreateItem(item);

			//ASSERT
			result.Should().BeTrue();

		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async void ListOfTODOs_UpdateItemById_ShouldReturnItem(int id)
		{
			//ARRANGE
			var dbContext = await GetDbContext(5);
			var itemRepository = new Repository(dbContext);

			//ACT
			//ASSERT
		}

			[Theory]
		[InlineData(1)]
		public async void ListOfTODOs_DeleteItemById_ShouldReturnBool(int id)
		{
			//ARRANGE
			var dbContext = await GetDbContext(5);
			var itemRepository = new Repository(dbContext);

			//ACT
			var result = await itemRepository.DeleteItemById(id);

			//ASSERT
			result.Should().BeTrue();

		}
	}
}