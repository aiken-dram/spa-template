namespace Application.Common.Interfaces;

/// <summary>
/// Service for custom audit in application
/// </summary>
public interface IAuditService
{
#warning SAMPLE, remove in actual application
    /// <summary>
    /// Audit for batch updating sample entity
    /// </summary>
    /// <param name="entity">Sample entity</param>
    /// <returns>Audit</returns>
    Task<Audit> SampleBatchUpdateAsync(Domain.Entities.Sample entity);
}
