using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


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

var conStrBuilder2 = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("RecordsIdentity"));
conStrBuilder2.UserID = builder.Configuration["UserId"];
conStrBuilder2.Password = builder.Configuration["Password"];
var connection2 = conStrBuilder2.ConnectionString;

builder.Services.AddDbContext<Records_Identity.RecordsIdentityDbContext>(opts => {
    opts.UseSqlServer(connection2,
    opts => opts.MigrationsAssembly("Records_Identity")
    );
});




//builder.Services.AddDbContext<Records_Identity.RecordsIdentityDbContext>(opts =>
//{
//    opts.UseSqlServer(connection2);
//});




// Add services to the container.
//builder.Services.AddRazorPages();
//builder.Services.AddControllersWithViews();

builder.Services.AddScoped<RecordsRepositories.Interfaces.ICitizenRepo, 
    RecordsRepositories.ConcretRepos.SqlCitizensRepo>();

builder.Services.AddScoped<RecordsRepositories.Interfaces.IDocumentTypeRepo,
    RecordsRepositories.ConcretRepos.SqlDocumentTypeRepo>();

builder.Services.AddScoped<RecordsRepositories.Interfaces.IDocumentRepo,
    RecordsRepositories.ConcretRepos.SqlDocumentRepo>();

builder.Services.AddScoped<RecordsRepositories.Interfaces.IPsychologicalProfileRepo,
    RecordsRepositories.ConcretRepos.SqlPsychologicalProfileRepo>();

builder.Services.AddScoped<Records.Functionalities.Interfaces.IStringManipulation,
    Records.Functionalities.ConcreteImpl.StringManipulation>();

builder.Services.AddScoped<Records.Functionalities.Interfaces.IFolderManipulation,
    Records.Functionalities.ConcreteImpl.FolderManipulation>();
builder.Services.AddScoped<Records_ML.IPsychProfilesSimilarities,
    Records_ML.PsychProfilesSimilarities>();

builder.Services.AddAutoMapper(typeof(RecordsDTOs.AnchorProfile));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("https://localhost:7098")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => {
//    opts.Password.RequiredLength = 8;
//    opts.Password.RequireDigit = false;
//    opts.Password.RequireLowercase = false;
//    opts.Password.RequireUppercase = false;
//    opts.Password.RequireNonAlphanumeric = false;
//    opts.SignIn.RequireConfirmedAccount = true;
//}).AddEntityFrameworkStores<Records_Identity.RecordsIdentityDbContext>();
//builder.Services.ConfigureApplicationCookie(opts => {
//    opts.LoginPath = "/Identity/SignIn";
//    //opts.LogoutPath = "/Identity/SignOut";
//    opts.AccessDeniedPath = "/Identity/Forbidden";
//});
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
app.UseCors("AllowOrigin");
//app.UseAuthorization();
//app.UseAuthentication();



app.UseEndpoints(endpoints =>
{ 
    endpoints.MapControllers();
});


app.MapDefaultControllerRoute(); 

//app.MapRazorPages();

app.Run();
