using Microsoft.EntityFrameworkCore;
using HostelManagementApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with PostgreSQL
builder.Services.AddDbContext<HostelManagementApi.HostelDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Test database connection endpoint
app.MapGet("/test-db", async (HostelManagementApi.HostelDbContext db) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        if (canConnect)
            return Results.Ok("✅ Database connection successful! Connected to myStay database.");
        else
            return Results.Problem("❌ Database connection failed.");
    }
    catch (Exception ex)
    {
        return Results.Problem($"❌ Database connection error: {ex.Message}");
    }
});

// GET /api/users - Fetch all users from the database
app.MapGet("/api/users", async (HostelManagementApi.HostelDbContext db) =>
{
    try
    {
        var users = await db.Users.ToListAsync();
        return Results.Ok(users);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error fetching users: {ex.Message}\nInner: {ex.InnerException?.Message}");
    }
})
.WithName("GetAllUsers");

// POST /api/insertuser - Insert a new user
app.MapPost("/api/insertuser", async (CreateUserRequest request, HostelManagementApi.HostelDbContext db) =>
{
    try
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest(new { error = "Name is required." });

        if (string.IsNullOrWhiteSpace(request.Email))
            return Results.BadRequest(new { error = "Email is required." });

        // Create user entity from request body
        var user = new User
        {
            Name = request.Name.Trim(),
            Email = request.Email.Trim(),
            City = request.City?.Trim(),
            State = request.State?.Trim(),
            DocType = request.DocType?.Trim(),
            DocNumber = request.DocNumber?.Trim(),
            CreatedAt = DateTimeOffset.UtcNow
        };

        // Insert into database
        db.Users.Add(user);
        await db.SaveChangesAsync();

        return Results.Created($"/api/users/{user.Id}", new
        {
            message = "User created successfully.",
            user
        });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error creating user: {ex.Message}\nInner: {ex.InnerException?.Message}");
    }
})
.WithName("InsertUser");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
