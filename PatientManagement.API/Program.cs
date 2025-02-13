using Microsoft.EntityFrameworkCore;
using PatientManagement.Infrastructure.Services;
using PatientManagement.Infrastructure.Data;
using PatientManagement.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your DbContext
builder.Services.AddDbContext<PatientManagementContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))); // From appsettings.json

// Register your services
builder.Services.AddScoped<IPatientService, PatientService > ();
builder.Services.AddScoped<IPatientRecordService, PatientRecordService > ();

var app = builder.Build();

//Apply migrations during startup
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<PatientManagementContext>();
//    dbContext.Database.Migrate(); // Ensure database is up-to-date
//}dotnet ef database update --project PatientManagement.Infrastructure --startup-project PatientManagement.API


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
