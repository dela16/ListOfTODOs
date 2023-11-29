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
				await dbContext.AddAsync(new ToDoItem(){Activity = "Activity" });
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

			for (int i = 0; i < 2; i++)
			{
				await itemRepository.CreateItem(new ToDoItem { Activity = $"Måla hela världen {i} gånger." }) ; //Ska id vara med? Bör jag göra propertyn unik?
			}

			var updateItemWith = new ToDoItem { Activity = "Dansa hela natten lång." };
			var itemToUpdate = await itemRepository.GetItemById(id);

			var ItemBeforeUpdate = new ToDoItem
			{
				Activity = itemToUpdate.Activity
			};

			//ACT
			ToDoItem result = await itemRepository.UpdateItemById(updateItemWith, id);

			//ASSERT
			result.Should().BeOfType<ToDoItem>();
			result.Should().NotBeNull(); 
			result.Should().NotBeSameAs(ItemBeforeUpdate);

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
		//Kan väl inte bara köra med happy cases?
		//[Theory]
		//[InlineData(-5)]
		//public async void ListOfTODOs_DeleteItemById_WhereIdIsNegative_ShouldReturnBool(int id)
		//{
		//	//ARRANGE
		//	var dbContext = await GetDbContext(5);
		//	var itemRepository = new Repository(dbContext);

		//	//ACT
		//	var result = await itemRepository.DeleteItemById(id);

		//	//ASSERT
		//	result.Should().BeFalse();

		//}
	}
}