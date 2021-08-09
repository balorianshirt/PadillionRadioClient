using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PadillionRadio.Data.Contexts
{
    public class ContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
        {
            public DatabaseContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
             
                // получаем конфигурацию из файла appsettings.json
                ConfigurationBuilder builder = new ConfigurationBuilder();
             //   builder.SetBasePath(Directory.GetCurrentDirectory());
           //     builder.AddJsonFile("..\\PadillionRadio\\appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot config = builder
                   // .SetBasePath(@"D:\Users\nutvismunt\Desktop\Freelance\PadillionRadio\")
                    .AddJsonFile(@"D:\Users\nutvismunt\Desktop\Freelance\PadillionRadio\PadillionRadio\appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                // получаем строку подключения из файла appsettings.json
                optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));

                return new DatabaseContext(optionsBuilder.Options);
            }
        }
}