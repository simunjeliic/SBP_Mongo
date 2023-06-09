﻿using SBP_Mongo.Models;
using SBP_Mongo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<VPDatabaseSettings>(
    builder.Configuration.GetSection("VPDatabase"));

builder.Services.AddSingleton<PozicijaService>();
builder.Services.AddSingleton<MarkaService>();
builder.Services.AddSingleton<ModelService>();
builder.Services.AddSingleton<LokacijaService>();
builder.Services.AddSingleton<VrstaService>();
builder.Services.AddSingleton<VoziloService>();
builder.Services.AddSingleton<UposlenikService>();
builder.Services.AddSingleton<DodjelaVozilaService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
