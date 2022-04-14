using LaboWebAPI.Services.BouteilleServices;
using LaboWebAPI.Services.AdresseServices;
using LaboWebAPI.Services.EmplacementServices;
using LaboWebAPI.Services.FournisseurServices;
using System.Data;
using System.Data.SqlClient;
using LaboADO.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IBouteilleService, BouteilleService>();
builder.Services.AddScoped<IAdresseService, AdresseService>();
builder.Services.AddScoped<IEmplacementService, EmplacementService>();
builder.Services.AddScoped<IFournisseurService, FournisseurService>();

builder.Services.AddScoped<IDbConnection, SqlConnection>(
        b => new SqlConnection(builder.Configuration.GetConnectionString("default"))
);

builder.Services.AddScoped<EmplacementRepository>();
builder.Services.AddScoped<AdresseRepository>();
builder.Services.AddScoped<FournisseurRepository>();
builder.Services.AddScoped<BouteilleRepository>();

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
