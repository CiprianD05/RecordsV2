using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configBuilder= new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                    reloadOnChange: true).AddUserSecrets<Program>();


builder.Services.AddDbContext<RecordsDbLibrary.RecordsDbContext>(opts => {
    opts.UseSqlServer(configBuilder.Build().GetConnectionString("Records"));
});
// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<RecordsRepositories.Interfaces.ICitizenRepo, 
    RecordsRepositories.ConcretRepos.SqlCitizensRepo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute(); 

//app.MapRazorPages();

app.Run();
