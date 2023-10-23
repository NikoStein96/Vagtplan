using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;

public class FirebaseAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public FirebaseAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            // The token is valid, and you can access the decoded claims (e.g., decodedToken.Claims).
            // You can also attach the decoded token to the HttpContext for further processing.

            context.Items["FirebaseToken"] = decodedToken;

            await _next(context);
        }
        catch (Exception)
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }
    }
}