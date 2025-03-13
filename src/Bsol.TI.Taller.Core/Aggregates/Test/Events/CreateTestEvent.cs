using Bsol.TI.Taller.SharedKernel;

namespace Bsol.TI.Taller.Core.Aggregates.Test.Events;

public class CreateTestEvent : DomainEventBase
{
    public CreateTestEvent(Test newTest)
    {
        NewTest = newTest;
    }

    public Test NewTest { get; }
}
