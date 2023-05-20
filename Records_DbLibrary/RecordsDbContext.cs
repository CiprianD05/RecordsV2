using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using RecordsModels;
using Microsoft.EntityFrameworkCore.Proxies;

namespace RecordsDbLibrary
{
    public class RecordsDbContext : DbContext
    {

        private static IConfigurationRoot _configuration;

        public DbSet<RecordsModels.Citizen> Citizens{ get; set; }
        public DbSet<RecordsModels.DocumentType> DocumentTypes { get; set; }
        public DbSet<Document>  Documents{ get; set; }

        public DbSet<PsychologicalProfile> PsychologicalProfiles { get; set; }
        public RecordsDbContext()
        {

        }

        public RecordsDbContext(DbContextOptions options)
            : base(options)
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
                    .AddUserSecrets<RecordsDbContext>()
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