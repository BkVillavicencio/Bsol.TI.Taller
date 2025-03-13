using Bsol.TI.Taller.SharedKernel.Audit;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bsol.TI.Taller.Infrastructure;
public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }

    public EntityEntry Entry { get; }
    public string TableName { get; set; } = "";
    public Dictionary<string, object?> KeyValues { get; } = new();
    public Dictionary<string, object?> OldValues { get; } = new();
    public Dictionary<string, object?> NewValues { get; } = new();
    public List<PropertyEntry> TemporaryProperties { get; } = new();

    public bool HasTemporaryProperties => TemporaryProperties.Any();

    public Audit ToAudit()
    {
        var audit = new Audit
        {
            TableName = TableName,
            DateTime = DateTime.UtcNow,
            KeyValues = System.Text.Json.JsonSerializer.Serialize(KeyValues),
            OldValues = OldValues.Count == 0 ? null : System.Text.Json.JsonSerializer.Serialize(OldValues),
            NewValues = NewValues.Count == 0 ? null : System.Text.Json.JsonSerializer.Serialize(NewValues)
        };
        return audit;
    }
}
