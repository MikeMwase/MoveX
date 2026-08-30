namespace MoveX.Application.Operations;

public interface IOperationsCommandService
{
    Task<AssignDriverResult> AssignDriverAsync(AssignDriverCommand command, CancellationToken cancellationToken = default);
    Task<ChangeBookingStatusResult> ChangeBookingStatusAsync(ChangeBookingStatusCommand command, CancellationToken cancellationToken = default);
}

public record AssignDriverResult(bool Succeeded, long? AssignmentId = null, string? Error = null);
public record ChangeBookingStatusResult(bool Succeeded, string? Status = null, string? Error = null);
