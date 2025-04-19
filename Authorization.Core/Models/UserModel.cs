using Authorization.Core.Contracts;
using Authorization.Core.DataAccess.Entities;
using CSharpFunctionalExtensions;
using DevOne.Security.Cryptography.BCrypt;

namespace Authorization.Core.Models;

public class UserModel : BaseModel
{
    public const int EmailMaxLength = 100;

    public const int PasswordMaxLength = 30;

    private UserModel(long id, DateTime createdAt, DateTime updatedAt, FullName fullName, string email, string password)
        : base(id, createdAt, updatedAt)
    {
        Email = email;
        Password = password;
        FullName = fullName;
    }

    public FullName FullName { get; }
    public string Email { get; }

    public string Password { get; }

    public static Result<UserModel, ApiError> Create(string firstName, string lastName, string middleName, string email,
        string password)
    {
        var fullNameResult = FullName.Create(firstName, lastName, middleName);
        if (fullNameResult.IsFailure) return Result.Failure<UserModel, ApiError>(fullNameResult.Error);

        if (string.IsNullOrWhiteSpace(email) || email.Length > EmailMaxLength)
        {
            return Result.Failure<UserModel, ApiError>(new ApiError(Messages.InvalidEmail,
                StatusCodes.Status400BadRequest));
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length > PasswordMaxLength)
        {
            return Result.Failure<UserModel, ApiError>(new ApiError(Messages.InvalidPassword,
                StatusCodes.Status400BadRequest));
        }

        var hashedPassword = BCryptHelper.HashPassword(password, email);

        var model = new UserModel(0, DateTime.UtcNow, DateTime.UtcNow, fullNameResult.Value, email,
            hashedPassword);
        return Result.Success<UserModel, ApiError>(model);
    }

    public static UserModel FromEntity(UserEntity entity)
    {
        return new UserModel(
            entity.Id,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.FullName,
            entity.Email,
            entity.Password);
    }
}