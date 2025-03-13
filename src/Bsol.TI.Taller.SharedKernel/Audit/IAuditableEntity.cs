namespace Bsol.TI.Taller.SharedKernel.Audit;

public interface IAuditableEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public IEnumerable<IAuditEntity>? AuditEntities { get; set; }
}
