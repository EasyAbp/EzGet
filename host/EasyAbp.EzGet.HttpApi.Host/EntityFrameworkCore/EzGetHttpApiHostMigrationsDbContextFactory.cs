using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.EzGet.EntityFrameworkCore
{
    public class EzGetHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<EzGetHttpApiHostMigrationsDbContext>
    {
        public EzGetHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<EzGetHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("EzGet"));

            return new EzGetHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
