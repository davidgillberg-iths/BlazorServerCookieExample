using Microsoft.AspNetCore.Http;
using System;

public class CookieService
{
    /* IHttpContextAccessor är ett gränssnitt i ASP.NET Core 
       som gör det möjligt att komma åt den aktuella HTTP-kontexten (HttpContext)
       från kod som normalt sett inte har direkt tillgång till den,
       t.ex. från tjänster (services) som inte är bundna till ett specifikt request. */
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetCookie(string key, string value, int? expireTimeMinutes = null)
    {
        var option = new CookieOptions();

        if (expireTimeMinutes.HasValue)
            option.Expires = DateTime.Now.AddMinutes(expireTimeMinutes.Value);
        else
            option.Expires = DateTime.Now.AddDays(7);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, option);
    }

    public string? GetCookie(string key)
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies[key];
    }
}
