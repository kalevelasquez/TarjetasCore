using TarjetasCore.Service.Application;
using TarjetasCore.Service.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAplication();
builder.Services.AddInfrastructure();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection"), 
        name: "SQL Server"
    );

var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:3000")  
          .AllowAnyMethod()
          .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");


app.Run();
