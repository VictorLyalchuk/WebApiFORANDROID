using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

public class UserRoleEntity : IdentityUserRole<int>
{
    public virtual UserEntity? User { get; set; }
    public virtual RoleEntity? Role { get; set; }
}