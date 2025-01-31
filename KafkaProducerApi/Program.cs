using KafkaPocCommon.Infrastructure;
using KafkaProducerApi.Controllers;
using KafkaProducerApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));

builder.Services.AddScoped<ShapeKafkaProducerService>();
builder.Services.AddScoped<MaterialKafkaProducerService>();
builder.Services.AddScoped<ShapeController>();
builder.Services.AddScoped<MaterialController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
