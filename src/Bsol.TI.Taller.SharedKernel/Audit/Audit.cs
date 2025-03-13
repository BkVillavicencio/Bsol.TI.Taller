
using Bsol.TI.Taller.SharedKernel.Interfaces;

namespace Bsol.TI.Taller.SharedKernel.Audit;

public class Audit : IAggregateRoot, IAuditEntity
{
    public int? Id { get; set; }
    public string? TableName { get; set; }
    public DateTime? DateTime { get; set; }
    public string? KeyValues { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

    public Audit(string tableName, string keyValues, string oldValues, string newValues)
    {
        TableName = tableName;
        KeyValues = keyValues;
        OldValues = oldValues;
        NewValues = newValues;
    }

    public Audit()
    {
    }


}
