using Ardalis.GuardClauses;
using Bsol.TI.Taller.Core.Aggregates.Test.Events;
using Bsol.TI.Taller.SharedKernel.Audit;
using Bsol.TI.Taller.SharedKernel.Interfaces;

namespace Bsol.TI.Taller.Core.Aggregates.Test;

public class Test : AuditableEntity, IAggregateRoot
{


    public string Name { get; set; }
    public TestType TestType { get; set; }
    public Test()
    { }
    public Test(string name, TestType testType)
    {
        Guard.Against.NullOrEmpty(name);
        Name = name;
        TestType = testType;
        RegisterDomainEvent(new CreateTestEvent(this));
    }

}
public enum TestType
{
    toDo,
    sucess,
    fail
}

