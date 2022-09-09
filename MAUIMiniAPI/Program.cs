using MAUIMiniAPI.Data;
using MAUIMiniAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConn")));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    var welcome = "MAUI Mini API | Furkan Karataþ";
    return welcome;
});

app.MapGet("/api/todo", async (AppDbContext context) =>
{
    var items = await context.ToDos.ToListAsync();
    return Results.Ok(items);
});

app.MapPost("/api/todo", async (AppDbContext context, ToDo toDo) =>
{
    await context.ToDos.AddAsync(toDo);
    await context.SaveChangesAsync();
    return Results.Created($"api/todo/{toDo.Id}", toDo);
});

app.MapPut("/api/todo/{id}", async (AppDbContext context, int Id, ToDo toDo) =>
{
    var todoModel = await context.ToDos.FirstOrDefaultAsync(p => p.Id == Id);
    if (todoModel == null)
    {
        return Results.NotFound();
    }

    todoModel.TodoName = toDo.TodoName;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/todo/{id}", async (AppDbContext context, int Id) =>
{
    var todoModel = await context.ToDos.FirstOrDefaultAsync(p => p.Id == Id);
    if (todoModel == null)
    {
        return Results.NotFound();
    }
    context.ToDos.Remove(todoModel);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();