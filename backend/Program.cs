using backend.Services.CharacterService;
using backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow our web api to share data with the frontend: Configuring CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(p=>p.AddPolicy("corspolicy", build =>
{
    //build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddAutoMapper(typeof(Program).Assembly); // Register Auto Mapper
builder.Services.AddScoped<ICharacterService, CharacterService>(); // Evrry time we use the interface ICharacterService it will use an instance of CharacterService class

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

