namespace MoveX.Application.Operations;

public interface IOperationsService
{
    Task<OperationsDashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DispatchCandidateDto>> GetDispatchCandidatesAsync(int bookingId, CancellationToken cancellationToken = default);
}
