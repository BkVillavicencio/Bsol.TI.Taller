using System.Reflection;
using Ardalis.Specification.EntityFrameworkCore;
using Bsol.TI.Taller.SharedKernel.Audit;
using Bsol.TI.Taller.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bsol.TI.Taller.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    private readonly AppDbContext _dbContext;
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    private async Task<IEnumerable<IAuditEntity>?> GetAudits<TAE>(TAE auditableEntity, CancellationToken cancellationToken = default) where TAE : AuditableEntity
    {
        var entityType = _dbContext.Model.FindEntityType(typeof(TAE));
        if (entityType == null) return null;
        var tableName = entityType.GetTableName();
        return await _dbContext.Audits
           .Where(a => a.KeyValues!.Contains($":{auditableEntity.Id}}}") && a.TableName == tableName)
           .ToListAsync(cancellationToken);
    }
    private async Task<AuditableEntity> LoadAuditsChild(AuditableEntity auditableEntity, CancellationToken cancellationToken = default)
    {
        foreach (PropertyInfo prop in auditableEntity
                                .GetType()
                                .GetProperties()
                                .Where(p => p.PropertyType.IsSubclassOf(typeof(AuditableEntity))))
        {

            var auditableChildProperty = auditableEntity.GetType().GetProperty(prop.Name);
            var propertyName = "AuditEntities";
            var propertyInfoChild = prop.PropertyType.GetProperty(propertyName);

            if (propertyInfoChild != null)
            {
                var auditEntityChild = auditableChildProperty?.GetValue(auditableEntity);
                if (auditEntityChild is not null)
                {
                    //TODO : implement Iconvertible For Childs
                    var value = await GetAudits((AuditableEntity)auditEntityChild, cancellationToken);

                    Type t = Nullable.GetUnderlyingType(propertyInfoChild.PropertyType) ?? propertyInfoChild.PropertyType;
                    object safeValue = value ?? Convert.ChangeType(value!, t);

                    propertyInfoChild.SetValue(propertyInfoChild, safeValue, null);
                }

            }

        }
        // TODO: Add Load List of Childs
        return auditableEntity;
    }
    public async Task<TAE?> LoadAudits<TAE>(TAE auditableEntity, CancellationToken cancellationToken = default) where TAE : AuditableEntity, IAggregateRoot
    {

        var entityType = _dbContext.Model.FindEntityType(typeof(TAE));
        if (entityType == null) return null;

        auditableEntity.AuditEntities = await GetAudits(auditableEntity, cancellationToken);
        auditableEntity = (TAE)await LoadAuditsChild(auditableEntity);
        return auditableEntity;
    }
}
