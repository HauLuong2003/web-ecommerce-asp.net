using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Reponsitory;
using Web_Ecommerce_Server.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//starting
//dang ky su dung database
builder.Services.AddDbContext<WebEcommerceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("connection string not found"));
});

builder.Services.AddScoped<IProduct, ProductReponsitory>();
builder.Services.AddScoped <IValidationService, ValidationService>();
builder.Services.AddScoped<IBrand, BrandReponsitory>();
builder.Services.AddScoped<IUserAccount, UserAccountReponsitory>();
builder.Services.AddScoped<IUserService, UserServiceReponsitory>();
builder.Services.AddScoped<IUser, UserReponsitory>();
builder.Services.AddScoped<ISaleReport, SaleReportReponsitory>();
builder.Services.AddScoped<IOrderManage, OrderManageReponsitory>();
builder.Services.AddScoped<ICart, CartReponsitory>();


// dang ky session
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(3000); // Set session timeout
    options.Cookie.HttpOnly = true; // Make the session cookie HttpOnly
    options.Cookie.IsEssential = true; // Make the session cookie essential
});
//ending
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.UseSession();
app.MapFallbackToFile("index.html");
app.Run();
