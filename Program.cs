using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WellMI.Auth;

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container.
builder.Services.AddDbContext<UserContext> ( Options =>
Options.UseSqlServer ( builder.Configuration.GetConnectionString ( "UserCs" ) ) );

builder.Services.AddIdentity<IdentityUser, IdentityRole> ()
    .AddEntityFrameworkStores<UserContext> ()
    .AddDefaultTokenProviders ();

builder.Services.AddAuthentication ( options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
} )


.AddJwtBearer ( options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters ()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration ["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( builder.Configuration ["JWT:Secret"] ) )
    };
} );



//builder.Services.AddAuthentication ( JwtBearerDefaults.AuthenticationScheme ).
//    AddJwtBearer ( optins =>
//    {
//        optins.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidIssuer = builder.Configuration ["Jwt:Issuer"],
//            ValidAudience = builder.Configuration ["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( builder.Configuration ["Jwt:Key"] ) )

//        };

//    }
//    );
//builder.Services.AddAuthentication ( Options =>
//{
//    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    Options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}
//);

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
