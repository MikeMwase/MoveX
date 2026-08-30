namespace MoveX.Domain.Entities.Customers;

public class CustomerProfile
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CustomerNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public ICollection<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
}

public enum CustomerStatus
{
    Pending,
    Active,
    Suspended,
    Deactivated
}
