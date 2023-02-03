using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RecordsDbLibrary
{
    public class RecordsDbContext : DbContext
    {

        private static IConfigurationRoot _configuration;

        
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
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();                
                var cnstr = _configuration.GetConnectionString("Records");
                optionsBuilder.UseSqlServer(cnstr);
            }
        }



    }
}