using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Api.Data;
using ProjectManagementSystem.Api.Helpers.GenerateToken;
using ProjectManagementSystem.Api.Models;
using System.Text;
using ProjectManagementSystem.Api.Profiles;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    #region DbContext
    builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
    });
    #endregion

    #region  Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    #endregion

    #region Mediator
    //builder.Services.AddMediatR(typeof(Program).Assembly);
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });
    #endregion

    #region AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    #endregion


    #region  OptionsPattern
    builder.Services.AddOptions<JwtOptions>()
        .BindConfiguration(JwtOptions.SectionName)
        .ValidateDataAnnotations();
    var jwtSettings = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;
    #endregion

    #region Authentication && Jwt
    builder.Services.AddSingleton<IJwtGenerator, JwtGenerator>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });
    #endregion

}

var app = builder.Build();
{

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
}
