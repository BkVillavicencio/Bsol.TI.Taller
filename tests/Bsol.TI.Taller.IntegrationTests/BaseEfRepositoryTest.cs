using Bsol.TI.Taller.Core.Aggregates.Test;
using Bsol.TI.Taller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bsol.TI.Taller.IntegrationTests;

public abstract class BaseEfRepositoryTest
{
    protected AppDbContext _dbContext;

    protected BaseEfRepositoryTest()
    {
        var options = CreateNewContextOptions();

        _dbContext = new AppDbContext(options);
    }

    protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseInMemoryDatabase("crmdb_test")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    public EfRepository<Test> GetTestRepository() => new(_dbContext);

}
