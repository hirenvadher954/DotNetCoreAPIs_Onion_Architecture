using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace CompanyEmployees.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        string dbPath = Path.Join(path, "CompanyEmployees.db");
        
        DbContextOptionsBuilder<RepositoryContext> builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlite($"Data Source={dbPath}",
            b => b.MigrationsAssembly("CompanyEmployees"));
        
        return new RepositoryContext(builder.Options);
    }
}