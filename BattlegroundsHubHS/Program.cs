using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Подключаем базу данных SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=battlegrounds.db"));

// Регистрируем DataImporter
builder.Services.AddScoped<DataImporter>();

var app = builder.Build();


// Статические файлы
app.UseDefaultFiles();  // index.html открывается по умолчанию
app.UseStaticFiles();   // Раздаёт файлы из папки wwwroot


// Настраиваем конвейер HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Создаём базу данных, если её нет
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();