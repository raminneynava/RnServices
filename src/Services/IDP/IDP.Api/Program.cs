using Asp.Versioning;

using AutoMapper;

using IDP.Api.Application.Helper;
using IDP.Api.Application.User.Command.Insert;
using IDP.Api.Domain.IRepository.Command;
using IDP.Api.Domain.IRepository.Command.Base;
using IDP.Api.Domain.IRepository.Query;
using IDP.Api.Infra.Data;
using IDP.Api.Infra.Repository.Command;
using IDP.Api.Infra.Repository.Query;

using MassTransit;

using MediatR;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(UserInsertHandler).GetTypeInfo().Assembly);
builder.Services.AddStackExchangeRedisCache(opition =>
{
    opition.Configuration = builder.Configuration.GetValue<string>("CashSetting:RedisUrl");
});
// Auto Mapper Configurations
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
builder.Services.AddTransient<IdpCommandDbContext>();
builder.Services.AddTransient<IdpQueryDbContext>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
.AddMvc() // This is needed for controllers
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddMassTransit(busConfig =>
{

    busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, cfg) =>
    {
        var ctr = builder.Configuration.GetValue<string>("Rabbit:Host");

        cfg.Host(new Uri(builder.Configuration.GetValue<string>("Rabbit:Host")), h =>
        {
            h.Username(builder.Configuration.GetValue<string>("Rabbit:UserNmae"));
            h.Password(builder.Configuration.GetValue<string>("Rabbit:Password"));
        });

        cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));

        cfg.ConfigureEndpoints(context);
    });
});
Auth.Extensions.AddJwt(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();

