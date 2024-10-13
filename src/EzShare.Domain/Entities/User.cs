using EzShare.Domain.Common;

namespace EzShare.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    
    public int RoleId { get; set; }
    public Role Role { get; init; }
}
