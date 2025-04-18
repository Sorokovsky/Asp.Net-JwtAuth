using Authorization.Core.Contracts;
using Authorization.Core.Models;
using CSharpFunctionalExtensions;

namespace Authorization.Core.Interfaces;

public interface IUsersService
{
    public Task<Result<UserModel, ApiError>> GetById(long id, CancellationToken cancellationToken = default);

    public Task<Result<UserModel, ApiError>> GetByEmail(string email, CancellationToken cancellationToken = default);

    public Task<Result<UserModel, ApiError>> Create(UserModel user, CancellationToken cancellationToken = default);

    public Task<Result<UserModel, ApiError>> Update(long id, UserModel user,
        CancellationToken cancellationToken = default);

    public Task<Result<UserModel, ApiError>> Delete(long id, CancellationToken cancellationToken = default);
}