namespace MoveX.Domain.Entities.Admin;

public class AdminRolePermission
{
    public int AdminRoleId { get; set; }
    public int AdminPermissionId { get; set; }

    public AdminRole? Role { get; set; }
    public AdminPermission? Permission { get; set; }
}
