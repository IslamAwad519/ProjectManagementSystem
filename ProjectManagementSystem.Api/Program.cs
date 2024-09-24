using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Api.Data;
using ProjectManagementSystem.Api.Data.SeedData;
using ProjectManagementSystem.Api.Helpers;
using ProjectManagementSystem.Api.Helpers.GenerateToken;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Profiles;
using ProjectManagementSystem.Api.Repositories;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.Services.ForgetPassword;
using ProjectManagementSystem.Api.Services.IOTPService;
using ProjectManagementSystem.Api.Services.VerifyAccount;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
    });

    #region  Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    #endregion

    //builder.Services.AddMediatR(typeof(Program).Assembly);
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });
    builder.Services.AddTransient<IMailService, MailService>();
    builder.Services.AddTransient<IJwtGenerator, JwtGenerator>();

    builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
  
    builder.Services.AddTransient<IOTPService, OTPService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    #region AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(ProjectProfile).Assembly);
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

    MapperHelper.Mapper = app.Services.GetService<IMapper>()!;

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

    // Initialize the database.
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var context = services.GetRequiredService<ApplicationDbContext>();

        await DbInitializer.Initialize(userManager, roleManager, context);
    }
    app.Run();

}