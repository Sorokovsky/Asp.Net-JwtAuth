using Authorization.Core.Contracts;
using Authorization.Core.DataAccess.Entities;
using Authorization.Core.Interfaces;
using Authorization.Core.Models;
using CSharpFunctionalExtensions;

namespace Authorization.Core.Application;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repository;

    public UsersService(IUsersRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<UserModel, ApiError>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.FindById(id, cancellationToken);
        if (result.IsFailure) return Result.Failure<UserModel, ApiError>(result.Error);
        var model = UserModel.FromEntity(result.Value);
        return Result.Success<UserModel, ApiError>(model);
    }

    public async Task<Result<UserModel, ApiError>> GetByEmail(string email,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.FindByEmail(email, cancellationToken);
        if (result.IsFailure) return Result.Failure<UserModel, ApiError>(result.Error);
        var model = UserModel.FromEntity(result.Value);
        return Result.Success<UserModel, ApiError>(model);
    }

    public async Task<Result<UserModel, ApiError>> Create(UserModel user, CancellationToken cancellationToken = default)
    {
        var candidateResult = await GetByEmail(user.Email, cancellationToken);
        var error = new ApiError(Messages.UserAlreadyExists, StatusCodes.Status400BadRequest);
        if (candidateResult.IsSuccess) return Result.Failure<UserModel, ApiError>(error);
        var entity = ModelToEntity(candidateResult.Value);
        var createdResult = await _repository.Create(entity, cancellationToken);
        if (createdResult.IsFailure) return Result.Failure<UserModel, ApiError>(createdResult.Error);
        return Result.Success<UserModel, ApiError>(UserModel.FromEntity(createdResult.Value));
    }

    public async Task<Result<UserModel, ApiError>> Update(long id, UserModel user,
        CancellationToken cancellationToken = default)
    {
        var candidateResult = await GetById(id, cancellationToken);
        if (candidateResult.IsFailure) return Result.Failure<UserModel, ApiError>(candidateResult.Error);
        var entity = ModelToEntity(candidateResult.Value);
        var updated = await _repository.Update(id, entity, cancellationToken);
        if (updated.IsFailure) return Result.Failure<UserModel, ApiError>(updated.Error);
        return Result.Success<UserModel, ApiError>(UserModel.FromEntity(updated.Value));
    }

    public async Task<Result<UserModel, ApiError>> Delete(long id, CancellationToken cancellationToken = default)
    {
        var candidateResult = await GetById(id, cancellationToken);
        if (candidateResult.IsFailure) return Result.Failure<UserModel, ApiError>(candidateResult.Error);
        var deleted = await _repository.Delete(id, cancellationToken);
        if (deleted.IsFailure) return Result.Failure<UserModel, ApiError>(deleted.Error);
        return Result.Success<UserModel, ApiError>(UserModel.FromEntity(deleted.Value));
    }

    private static UserEntity ModelToEntity(UserModel model)
    {
        return new UserEntity
        {
            Email = model.Email,
            FullName = model.FullName,
            Password = model.Password
        };
    }
}