using System.Data.Common;
using Bsol.TI.Taller.SharedKernel.Interfaces;

namespace Bsol.TI.Taller.Infrastructure.Data;
public abstract class QueryRepository<T>(DbConnection connection) : IQueryRepository<T> where T : class
{
    private readonly DbConnection _connection = connection;
}
