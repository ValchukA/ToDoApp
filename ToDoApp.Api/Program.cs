using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, _, configuration) => configuration.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
