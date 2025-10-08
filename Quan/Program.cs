var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var products = new List<Product>
{
    new Product { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Price = 35.00m, Stock = 10 },
    new Product { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Price = 40.00m, Stock = 5 }
};
var nextId = 3;

// Condition 1: Return all stored books
app.MapGet("/api/books", () => Results.Ok(products));

// Condition 2: Return selected book based on Id
app.MapGet("/api/books/{id:int}", (int id) =>
{
    var p = products.FirstOrDefault(x => x.Id == id);
    return p is null ? Results.NotFound(new { message = "Book not found." }) : Results.Ok(p);
});

// Condition 3: Create from JSON
app.MapPost("/api/books", (ProductCreate input) =>
{
    if (string.IsNullOrWhiteSpace(input.Title) || input.Title.Length < 2 || input.Title.Length > 100)
        return Results.BadRequest(new { message = "Title must be 2-100 characters." });
    if (string.IsNullOrWhiteSpace(input.Author) || input.Author.Length < 2 || input.Author.Length > 60)
        return Results.BadRequest(new { message = "Author must be 2-60 characters." });
    if (input.Price < 0)
        return Results.BadRequest(new { message = "Price must be >= 0." });
    if (input.Stock < 0)
        return Results.BadRequest(new { message = "Stock must be >= 0." });

    var p = new Product
    {
        Id = nextId++,
        Title = input.Title,
        Author = input.Author,
        Price = input.Price,
        Stock = input.Stock
    };

    products.Add(p);
    return Results.Created($"/api/books/{p.Id}", p);
});

// Condition 4: Replace / Update
app.MapPut("/api/books/{id:int}", (int id, ProductUpdate input) =>
{
    var p = products.FirstOrDefault(x => x.Id == id);
    if (p is null)
        return Results.NotFound(new { message = "Book not found." });

    if (string.IsNullOrWhiteSpace(input.Title) || input.Title.Length < 2 || input.Title.Length > 100)
        return Results.BadRequest(new { message = "Title must be 2-100 characters." });
    if (string.IsNullOrWhiteSpace(input.Author) || input.Author.Length < 2 || input.Author.Length > 60)
        return Results.BadRequest(new { message = "Author must be 2-60 characters." });
    if (input.Price < 0)
        return Results.BadRequest(new { message = "Price must be >= 0." });
    if (input.Stock < 0)
        return Results.BadRequest(new { message = "Stock must be >= 0." });

    p.Title = input.Title;
    p.Author = input.Author;
    p.Price = input.Price;
    p.Stock = input.Stock;

    return Results.Ok(p);
});

// Condition 5: Delete
app.MapDelete("/api/books/{id:int}", (int id) =>
{
    var p = products.FirstOrDefault(x => x.Id == id);
    if (p is null)
        return Results.NotFound(new { message = "Book not found." });

    products.Remove(p);
    return Results.NoContent();
});

app.Run();

// ---- Types ----
public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public record ProductCreate(string Title, string Author, decimal Price, int Stock);
public record ProductUpdate(string Title, string Author, decimal Price, int Stock);
