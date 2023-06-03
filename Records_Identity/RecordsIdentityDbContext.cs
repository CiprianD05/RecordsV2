using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace Records_Identity
{
    public class RecordsIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public RecordsIdentityDbContext()
        {
            
        }

        public RecordsIdentityDbContext(DbContextOptions<RecordsIdentityDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string basePath = Directory.GetCurrentDirectory();
                string relativePath = @"..\Records\appsettings.Development.json";
                string absolutePath = Path.Combine(basePath, relativePath);

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile(absolutePath, optional: true)
                    .AddUserSecrets<RecordsIdentityDbContext>()
                    .Build();


                var conStrBuilder = new SqlConnectionStringBuilder(
                 configuration.GetConnectionString("Records"));
                conStrBuilder.UserID = configuration["UserId"];
                conStrBuilder.Password = configuration["Password"];
                var connection = conStrBuilder.ConnectionString;



                //optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(connection);
            }
        }


    }
}