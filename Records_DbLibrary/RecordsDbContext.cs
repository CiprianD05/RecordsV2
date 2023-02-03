using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace RecordsDbLibrary
{
    public class RecordsDbContext : DbContext
    {

        private static IConfigurationRoot _configuration;

        public DbSet<RecordsModels.Citizen> Citizens{ get; set; }
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
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<RecordsDbContext>()
                .AddEnvironmentVariables();
                
                


                _configuration = builder.Build();
                
                var cnstr = _configuration.GetConnectionString("Records");

                optionsBuilder.UseSqlServer(cnstr);
            }
        }

        


    }
}