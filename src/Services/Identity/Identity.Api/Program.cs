using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using System.Net.Http;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication().AddCookie();




builder.Services.AddHttpContextAccessor();

var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthentication();



app.MapGet("/login",async (HttpContext httpContext) =>
{
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name,"ramin")
    };

    var identityclaim = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var userprincipal = new ClaimsPrincipal(identityclaim);
    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,userprincipal);

    return Results.Ok("Login in true");
});

app.MapGet("/username", (HttpContext httpContext) =>
{
    return httpContext.User.Identity.Name;
});

app.Run();


public class ApiKeyAuthenticationOption: AuthenticationSchemeOptions
{
    public string headername { get; set; }
}
