using ToDoList.Properties.DAL.Storages;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Properties.DAL.Models;
using ListOfTODOs.Properties.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using ListOfTODOs.Properties.DAL.Repositories.Interfaces;
using ListOfTODOs.Properties.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

var server = "localhost";
var database = "ToDoListDb";

var connectionString = $"Server={server};Database={database};Trusted_Connection=True;TrustServerCertificate=True";
// Add services to the container.


builder.Services.AddDbContext<ToDoListDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IRepository, Repository>();
//builder.Services.AddSingleton<ItemStorage>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//Borde mina endpoints hamna i egen fil som i klappverkstan? 
app.MapPost("/ItemIndex", (IRepository repository, ToDoItem item) =>
    {
        var result = repository.CreateItem(item);

        return Results.Ok("Item added to list.");
        //if (repository.CreateItem(item))
        //{
        //    return Results.Ok(repository.GetAllItems());
        //}
        //return Results.BadRequest("Some reason.");//Vad ska skickas tillbaka? 
    });



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
