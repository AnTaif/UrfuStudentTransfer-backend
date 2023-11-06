using StudentTransfer.Dal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString is not null)
{
    var dbPassword = System.Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
    connectionString += $"Password={dbPassword}";
}

// Add layers
builder.Services.AddDataLayer(connectionString ?? "");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();