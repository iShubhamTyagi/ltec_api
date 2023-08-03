using LTEC.Interfaces;
using LTEC.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:7257", "http://localhost:5257") // Swagger UI's and your React app's URL
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddTransient<IGoogleSheetsService, GoogleSheetsService>();
builder.Services.AddTransient<IPatientDataService, PatientDataService>();
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
