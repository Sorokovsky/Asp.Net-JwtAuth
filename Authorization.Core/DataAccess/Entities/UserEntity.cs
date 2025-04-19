using Authorization.Core.Models;

namespace Authorization.Core.DataAccess.Entities;

public class UserEntity : BaseEntity
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public FullName FullName { get; set; }
}