using FluentResults;

namespace Bsol.TI.Taller.Core.Interfaces;

public interface ITestServices
{
    Task<Result<Boolean>> ValidateTestAsync(string testName);
}
