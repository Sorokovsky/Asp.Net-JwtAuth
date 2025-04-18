using System.Linq.Expressions;
using Authorization.Core.Contracts;
using Authorization.Core.DataAccess;
using Authorization.Core.DataAccess.Entities;
using Authorization.Core.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AuthorizationDatabaseContext _context;

    public UsersRepository(AuthorizationDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Result<UserEntity, ApiError>> Create(UserEntity user, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var created = await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success<UserEntity, ApiError>(created.Entity);
        }
        catch (Exception)
        {
            var error = new ApiError(Messages.SomethingWentWrong, StatusCodes.Status500InternalServerError);
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<UserEntity, ApiError>(error);
        }
    }

    public async Task<Result<UserEntity, ApiError>> FindById(long id, CancellationToken cancellationToken)
    {
        return await Find(user => user.Id == id, cancellationToken);
    }

    public async Task<Result<UserEntity, ApiError>> FindByEmail(string email, CancellationToken cancellationToken)
    {
        return await Find(user => user.Email == email, cancellationToken);
    }

    public async Task<Result<UserEntity, ApiError>> Update(long id, UserEntity user,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var candidateResult = await FindById(id, cancellationToken);
            if (candidateResult.IsFailure) return candidateResult;
            var candidate = candidateResult.Value;
            _context.Users.Attach(candidate);
            if (!string.IsNullOrWhiteSpace(candidate.Password)) candidate.Password = user.Password;
            if (!string.IsNullOrWhiteSpace(candidate.Email)) candidate.Email = user.Email;
            if (!string.IsNullOrWhiteSpace(candidate.FirstName)) candidate.FirstName = user.FirstName;
            if (!string.IsNullOrWhiteSpace(candidate.LastName)) candidate.LastName = user.LastName;
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result.Success<UserEntity, ApiError>(candidate);
        }
        catch (Exception)
        {
            var error = new ApiError(Messages.SomethingWentWrong, StatusCodes.Status500InternalServerError);
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<UserEntity, ApiError>(error);
        }
    }

    public async Task<Result<UserEntity, ApiError>> Delete(long id, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var candidateResult = await FindById(id, cancellationToken);
            if (candidateResult.IsFailure) return candidateResult;
            await _context.Users.Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return candidateResult;
        }
        catch (Exception)
        {
            var error = new ApiError(Messages.SomethingWentWrong, StatusCodes.Status500InternalServerError);
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<UserEntity, ApiError>(error);
        }
    }

    private async Task<Result<UserEntity, ApiError>> Find(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        var error = new ApiError(Messages.UserNotFound, StatusCodes.Status404NotFound);
        return user is null ? Result.Failure<UserEntity, ApiError>(error) : Result.Success<UserEntity, ApiError>(user);
    }
}