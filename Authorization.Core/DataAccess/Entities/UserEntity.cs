namespace Authorization.Core.DataAccess.Entities;

public class UserEntity : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string MiddleName { get; set; } = string.Empty;
}