using Bsol.TI.Taller.Core.Interfaces;
using FluentResults;

namespace Bsol.TI.Taller.Infrastructure.Services
{
    class TestServices : ITestServices
    {
        async Task<Result<bool>> ITestServices.ValidateTestAsync(string testName)
        {
            if (testName == "base de datos")
            {
                return Result.Fail("No se puede continuar");
            }
            return Result.Ok(true);

        }
    }
}
