var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, _, configuration) => configuration.ReadFrom.Configuration(builder.Configuration));

var authOptions = builder.Configuration.GetRequiredSection(KeycloakOptions.SectionKey).Get<KeycloakOptions>()!;
new KeycloakOptionsValidator(builder.Environment.IsDevelopment()).ValidateAndThrow(authOptions);

builder.Services.AddApiServices(authOptions, builder.Environment);
builder.Services.AddBllServices();
builder.Services.AddMongoStorageServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId(authOptions.SwaggerClientId);
        options.OAuthUsePkce();
    });
}

app.UseMiddleware<RequestIdLoggingMiddleware>();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers().RequireAuthorization();

app.Run();
