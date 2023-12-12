using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WellMI.Data;

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container.
builder.Services.AddDbContext<UserContext> ( Options =>
Options.UseSqlServer ( builder.Configuration.GetConnectionString ("UserCs") ) );

builder.Services.AddAuthentication ( JwtBearerDefaults.AuthenticationScheme ).
    AddJwtBearer ( optins =>
    {
        optins.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration ["Jwt:Issuer"],
            ValidAudience = builder.Configuration ["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( builder.Configuration ["Jwt:Key"] ) )

        };

    }
    );
builder.Services.AddAuthentication ( Options =>
{
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
);

builder.Services.AddControllers ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build ();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment () )
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseHttpsRedirection ();

app.UseAuthorization ();
app.UseAuthorization ();

app.MapControllers ();

app.Run ();
