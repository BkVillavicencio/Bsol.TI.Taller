using System.ComponentModel.DataAnnotations.Schema;

namespace Bsol.TI.Taller.SharedKernel.Audit;

public class AuditableEntity : EntityBase, IAuditableEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    [NotMapped]
    public IEnumerable<IAuditEntity>? AuditEntities { get; set; }
}
