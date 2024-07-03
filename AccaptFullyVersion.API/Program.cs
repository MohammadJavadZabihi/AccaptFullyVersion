using AccaptFullyVersion.Core.Servies;
using AccaptFullyVersion.Core.Servies.Interface;
using AccaptFullyVersion.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region DataContext

builder.Services.AddDbContext<AccaptContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionDB")));

#endregion

#region IOC

builder.Services.AddScoped<IUserServies, UserServies>();

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
