using Authorization.Core.DataAccess.Entities;

namespace Authorization.Core.Interfaces;

public interface IUsersRepository
{
    public Task<UserEntity> Create(UserEntity user, CancellationToken cancellationToken);
    
    public Task<UserEntity?> FindById(long id, CancellationToken cancellationToken);
    
    public Task<UserEntity> Update(long id, UserEntity user, CancellationToken cancellationToken);
    
    public Task<long> Delete(long id, CancellationToken cancellationToken);
}