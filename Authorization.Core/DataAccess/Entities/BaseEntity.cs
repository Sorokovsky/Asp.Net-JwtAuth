using CSharpFunctionalExtensions;

namespace Authorization.Core.DataAccess.Entities;

public class BaseEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}