using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RoomBookingApp.Api", Version = "v1" });
});

#region SqlLite
var connString = "Filename=:memory:";
var conn = new SqliteConnection(connString);
conn.Open();

builder.Services.AddDbContext<RoomBookingAppDbContext>(opt => opt.UseSqlite(conn));
var sqlBuilder = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
sqlBuilder.UseSqlite(conn);

using var context = new RoomBookingAppDbContext(sqlBuilder.Options);
context.Database.EnsureCreated();
#endregion

builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoomBookingApp.Api v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
