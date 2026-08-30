namespace MoveX.Domain.Entities.Admin;

public class AdminPermission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Module { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<AdminRolePermission> RolePermissions { get; set; } = new List<AdminRolePermission>();
}
