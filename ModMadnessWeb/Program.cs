using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModMadnessDomain.Identity;
using ModMadnessRepository.Data;
using ModMadnessRepository.Implementation;
using ModMadnessRepository.Interface;
using ModMadnessService.API;
using ModMadnessService.Implementation;
using ModMadnessService.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IGameVersionService, GameVersionService>();
builder.Services.AddTransient<IDLCService, DLCService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();
builder.Services.AddTransient<IModService, ModService>();
builder.Services.AddHttpClient<IRawgApiService, RawgApiService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<MadnessUser>(options =>

    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.Configure<RawgSettings>(
    builder.Configuration.GetSection("ExternalApis:Rawg"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
