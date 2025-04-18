using Authorization.Core.Contracts;
using Authorization.Core.DataAccess.Entities;
using CSharpFunctionalExtensions;

namespace Authorization.Core.Interfaces;

public interface IUsersRepository
{
    public Task<Result<UserEntity, ApiError>> Create(UserEntity user, CancellationToken cancellationToken);
    
    public Task<Result<UserEntity, ApiError>> FindById(long id, CancellationToken cancellationToken);
    
    public Task<Result<UserEntity, ApiError>> Update(long id, UserEntity user, CancellationToken cancellationToken);
    
    public Task<Result<UserEntity, ApiError>> Delete(long id, CancellationToken cancellationToken);
}