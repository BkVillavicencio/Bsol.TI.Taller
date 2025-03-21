using Bsol.TI.Taller.Core.Aggregates.Test;
using Bsol.TI.Taller.Infrastructure.Data;
using FastEndpoints;

namespace Bsol.TI.Taller.Api.Endpoints.Home;

public class Home : EndpointWithoutRequest<EmptyResponse>
{
    private readonly EfRepository<Test> _testRepository;
    public Home(EfRepository<Test> testRepository)
    {
        _testRepository = _testRepository;
    }
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }
    public override Task HandleAsync(CancellationToken cancellationToken)
    {
       var newTest= _testRepository.AddAsync(new Test("Hola mundo", TestType.toDo));


        _testRepository.SaveChangesAsync();
        return Task.CompletedTask;
    }
}
