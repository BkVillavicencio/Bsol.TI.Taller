namespace Bsol.TI.Taller.SharedKernel.Audit;

public interface IAuditEntity
{
    public int? Id { get; set; }
    public string? TableName { get; set; }
    public DateTime? DateTime { get; set; }
    public string? KeyValues { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

}
