namespace Bsol.TI.Taller.Infrastructure.Data;
public class CommandRepository<T>(AppDbContext dbContext) : CommandBaseRepository<T>(dbContext) where T : class
{
    private readonly AppDbContext _dbContext = dbContext;
}
