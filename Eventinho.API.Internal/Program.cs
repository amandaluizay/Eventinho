using Eventinho.Database;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EventinhoDbcontext>((serviceProvider, options) =>
{
    options.UseSqlServer("Server=localhost,1433;Database=eventinho-test;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
});

//builder.Services.AddAuthentication(opt =>
//{
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test123")),
//        ClockSkew = TimeSpan.Zero,
//    };
//});

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
