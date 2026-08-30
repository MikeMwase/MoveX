namespace MoveX.Domain.Entities.Admin;

public class AdminRole
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<AdminUser> Users { get; set; } = new List<AdminUser>();
    public ICollection<AdminRolePermission> Permissions { get; set; } = new List<AdminRolePermission>();
}
