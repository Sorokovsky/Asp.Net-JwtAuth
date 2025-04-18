namespace Authorization.Core.Models;

public abstract class BaseModel
{
    public long Id { get; }
    
    public DateTime CreatedAt { get; }
    
    public DateTime UpdatedAt { get; }

    protected BaseModel(long id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}