using ApiProject.Api.Authentication.TokenGenerators;
using ApiProject.Api.Authentication.TokenValidators;
using Microsoft.IdentityModel.Tokens;
using ApiProject.Api;
using System.Text;
using ApiProject.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
//Services dependancy injection
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
//JWT Token configuration
//DbContext

AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
//TODO:Bind authenticationConfiguration to JWT Token settings from User secrets;
builder.Services.AddSingleton(authenticationConfiguration);
builder.Configuration.Bind("Authentication",authenticationConfiguration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
        ValidIssuer = authenticationConfiguration.Issuer,
        ValidAudience = authenticationConfiguration.Audience,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
    };
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
