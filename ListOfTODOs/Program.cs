using ToDoList.Properties.DAL.Models;
using ListOfTODOs.Properties.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using ListOfTODOs.Properties.DAL.Repositories.Interfaces;
using ListOfTODOs.Properties.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;

var builder = WebApplication.CreateBuilder(args);

var server = "localhost";
var database = "ToDoListDb";

var connectionString = $"Server={server};Database={database};Trusted_Connection=True;TrustServerCertificate=True";

builder.Services.AddDbContext<ToDoListDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IRepository, Repository>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapGet("/items", async (IRepository repository) =>
{
	var result = await repository.GetAllItems();

	return result.Count > 0 ? Results.Ok(result) : Results.NotFound("No items in your to do list.");
});

app.MapPost("/items", async (IRepository repository, ToDoItem item) =>
	{ 
		var result = await repository.CreateItem(item);

		if (result is false)
			return Results.BadRequest("You need to write an activity.");

		return Results.Ok($"Item {result} was added to list.");
    });

app.MapPut("/items/{id}", async (IRepository repository, ToDoItem updateItem, int id) =>
{
	if (id <= 0 || updateItem.Id.Equals("") ||updateItem.Activity.IsNullOrEmpty())
	{
		return Results.BadRequest("Couldn't update item.");
	}

	var result = await repository.UpdateItemById(updateItem, id);

    if (result.Id != id)
        return Results.NotFound($"{result.Id} No item with that id.");

    return Results.Ok($"{result} was updated.");
});

app.MapDelete("/items/{id}", async (IRepository repository, int id) =>
{
	var result = await repository.DeleteItemById(id);
	if (result is false) 
		return Results.BadRequest("Item could not be found, try another id.");

	return Results.Ok($"Item with id {result} was deleted");
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
