using SkinData.Infrastructure;
using SkinData.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ISkinAnalysisRepository, SkinAnalysisRepository>();
builder.Services.AddTransient<ISkinDataService, SkinDataService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<IProductRecommendationRepository, ProductRecommendationRepository>();
builder.Services.AddTransient<IProductRecommendationService, ProductRecommendationService>();
builder.Services.AddTransient<IUserRoutineRepository, UserRoutineRepository>();
builder.Services.AddTransient<IUserRoutineService, UserRoutineService>();

var app = builder.Build();

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
