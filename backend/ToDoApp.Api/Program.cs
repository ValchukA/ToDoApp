var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, _, configuration) => configuration.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddApiServices();
builder.Services.AddBllServices();
builder.Services.AddMongoStorageServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestIdLoggingMiddleware>();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
