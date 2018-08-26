using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SampleSolution.Data.Contexts.Factories
{
    public static class EntityFrameworkContextFactory
    {
        public static SomeDataContext CreateSomeDataContext(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var optionsBuilder = new DbContextOptionsBuilder<SomeDataContext>();
            optionsBuilder.UseSqlServer(configuration.GetSection("SomeDataContextConnection:ConnectionString").Value,
                b => b.MigrationsAssembly("SampleSolution.Data"));

            return new SomeDataContext(optionsBuilder.Options);
        }
    }
}
