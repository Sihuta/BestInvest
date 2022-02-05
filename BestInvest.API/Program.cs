using BestInvest.API.BLL.Interfaces;
using BestInvest.API.BLL.Services;
using BestInvest.API.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BestInvest.API.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.WithOrigins("http://localhost:4200")); // url of the frontend
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
}
).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ForStartuper", policyBuilder => policyBuilder.RequireClaim("Role", "startuper"));
    options.AddPolicy("ForInvestor", policyBuilder => policyBuilder.RequireClaim("Role", "investor"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IdentityService>();
builder.Services.AddTransient<Mapper>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IStartuperService, StartuperService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITeamService, TeamService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
