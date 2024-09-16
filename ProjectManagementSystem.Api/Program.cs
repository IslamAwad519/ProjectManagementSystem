using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.Data;
using ProjectManagementSystem.Api.Helpers.GenerateToken;
using ProjectManagementSystem.Api.Models;

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
}

    builder.Services.AddTransient<IMailService, MailService>();
    builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddTransient<IOTPService, OTPService>();
    builder.Services.AddTransient<IUserService, UserService>();
    var app = builder.Build();

    MapperHelper.Mapper = app.Services.GetService<IMapper>()!;

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