using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManagementApplication.Interfaces;
using TaskManagementApplication.Models;
using TaskManagementApplication.Repositories;
using TaskManagementApplication.Services;
using TaskManagementApplication.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<TaskContext>(options =>
        options.UseLazyLoadingProxies().UseSqlite("Data Source=TaskDB.db"));

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Repositories (if you need to inject them directly)
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register Services
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("CustomAuthScheme")
    .AddCookie("CustomAuthScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddControllers();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/"); // Require authorization for all pages
    options.Conventions.AllowAnonymousToPage("/Account/Login"); // Allow anonymous access to the login page
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<TaskManagementApplication.Middleware.ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", async context =>
{
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Account/Login"); 
    }
    else
    {
        context.Response.Redirect("/Index"); 
    }
});
app.MapRazorPages();


app.Run();
