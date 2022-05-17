using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Product.API.Extensions;
using Product.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    //config.Authority = builder.Configuration["IdentityURL"];
    // config.RequireHttpsMetadata = false;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // `iss` claim
        ValidateAudience = false, // `aud` claim
        ValidateLifetime = false, // `exp`, `nbf` claims
        ValidateIssuerSigningKey = false, // signature
        SignatureValidator = (token, parameters) => new JwtSecurityToken(token), // don't validate signature
    };
    config.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            var act = context.Request.Query["access_token"];
            if (string.IsNullOrEmpty(act) == false)
            {
                context.Token = act;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("Admin", policy =>
    {
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireClaim("Role", "Admin");
    });
});
builder.Services.AddHttpClient<IIdentityService, IdentityService>(config => {
    config.BaseAddress = new Uri("http://localhost:5295");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.useAuthMiddleware();

app.Run();
