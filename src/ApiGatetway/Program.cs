var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("Yarp"));
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapReverseProxy();
app.Run();

