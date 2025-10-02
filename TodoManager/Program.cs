// Program.cs - Trecho de configura��o
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoManager.Data;
using TodoManager.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)); // Usando UseNpgsql

// 2. Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Configurar JWT Authentication (Opcional: Necess�rio para a API)
// Configura��o detalhada do JWT (Key, Issuer, Audience) deve ser adicionada aqui e em appsettings.json.
// ... C�digo de configura��o do JWT Bearer, usando AddAuthentication e AddJwtBearer ...


builder.Services.AddAuthorization();

// 4. Configurar Servi�os e Reposit�rios (DI)
// Exemplo: builder.Services.AddScoped<ITaskRepository, TaskRepository>();
// Exemplo: builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();