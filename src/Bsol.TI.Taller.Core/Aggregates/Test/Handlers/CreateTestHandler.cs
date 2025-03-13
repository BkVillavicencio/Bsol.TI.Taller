using Bsol.TI.Taller.Core.Aggregates.Test.Events;
using Bsol.TI.Taller.SharedKernel.Interfaces;
using MediatR;

namespace Bsol.TI.Taller.Core.Aggregates.Test.Handlers;
internal class CreateTestHandler : INotificationHandler<CreateTestEvent>
{
    private readonly IRepository<Test> _repository;

    public CreateTestHandler(IRepository<Test> repository)
    {
        _repository = repository;
    }
    public Task Handle(CreateTestEvent domainEvent, CancellationToken cancellationToken)
    {

        return _repository.SaveChangesAsync(cancellationToken);
    }
}
