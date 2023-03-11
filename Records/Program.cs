using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

var conStrBuilder = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("Records"));
conStrBuilder.UserID = builder.Configuration["UserId"];
conStrBuilder.Password = builder.Configuration["Password"];
var connection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<RecordsDbLibrary.RecordsDbContext>(opts =>
{
    opts.UseSqlServer(connection);
});
// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<RecordsRepositories.Interfaces.ICitizenRepo, 
    RecordsRepositories.ConcretRepos.SqlCitizensRepo>();

builder.Services.AddAutoMapper(typeof(RecordsDTOs.AnchorProfile));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("https://localhost:7098")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowOrigin");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.UseRouting();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapControllers();
});


app.MapDefaultControllerRoute(); 

//app.MapRazorPages();

app.Run();
