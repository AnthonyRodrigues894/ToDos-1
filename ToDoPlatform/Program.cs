using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoPlatform.Data;
using ToDoPlatform.Models;
using ToDoPlatform.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Conexão com banco
var conexao = builder.Configuration.GetConnectionString("Conexao");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(conexao)
);

// 🔐 Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.SignIn.RequireConfirmedAccount = false;
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// ⚠️ Necessário para IHttpContextAccessor (usado no UserService)
builder.Services.AddHttpContextAccessor();

// 👤 Registro do serviço
builder.Services.AddScoped<IUserService, UserService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🗄️ Criação do banco (dev only recomendado)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

// 🚦 Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ⚠️ necessário

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();