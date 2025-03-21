

using Ardalis.GuardClauses;
using Bsol.TI.Taller.SharedKernel.Audit;
using Bsol.TI.Taller.SharedKernel.Interfaces;

namespace Bsol.TI.Taller.Core.Aggregates.Cuentas;

public class Account : AuditableEntity, IAggregateRoot
{
    public Account(string accountcode, bool isActive)
    {
        Guard.Against.NullOrEmpty(accountcode);
        Accountcode = accountcode;
        IsActive = isActive;
    }

    public string Accountcode { get; set; }
    public bool IsActive { get; set; }


    public void DeleteAccount()
    {
        IsActive = false;
    }
}
