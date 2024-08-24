using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using Coffee.QR.API.DTOs;

namespace Coffee.QR_BackEnd.Startup
{
    public static class AuthConfiguration
    {

        public static IServiceCollection ConfigureAuth(this IServiceCollection services)
        {
            ConfigureAuthentication(services);
            ConfigureAuthorizationPolicies(services);
            return services;
        }

        private static void ConfigureAuthentication(IServiceCollection services)
        {
            string filePath = "Resources/appJwtSettings.json";
            string jsonString = File.ReadAllText(filePath);
            JWTCredentialsDto credentials = JsonSerializer.Deserialize<JWTCredentialsDto>(jsonString);

            var key = Environment.GetEnvironmentVariable("JWT_KEY") ?? credentials.Key;
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? credentials.Issuer;
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? credentials.Audience;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("AuthenticationTokens-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        private static void ConfigureAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("administratorPolicy", policy => policy.RequireRole("administrator"));
                options.AddPolicy("waiterPolicy", policy => policy.RequireRole("waiter"));
                options.AddPolicy("clientPolicy", policy => policy.RequireRole("Client"));
                options.AddPolicy("managerPolicy", policy => policy.RequireRole("manager"));
                options.AddPolicy("itSupportPolicy", policy => policy.RequireRole("itsupport"));
            });
        }

    }
}
