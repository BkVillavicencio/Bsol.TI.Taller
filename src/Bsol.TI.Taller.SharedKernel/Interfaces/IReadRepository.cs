using Ardalis.Specification;

namespace Bsol.TI.Taller.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
