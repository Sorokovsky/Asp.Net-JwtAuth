using Authorization.Core.Contracts;
using Authorization.Core.DataAccess.Entities;
using CSharpFunctionalExtensions;

namespace Authorization.Core.Models;

public class UserModel : BaseModel
{
    public const int FirstNameMaxLength = 20;

    public const int LastNameMaxLength = 20;

    public const int MiddleNameMaxLength = 20;

    public const int EmailMaxLength = 100;

    public const int PasswordMaxLength = 30;

    private UserModel(long id, DateTime createdAt, DateTime updatedAt, string firstName, string middleName,
        string lastName, string email, string password)
        : base(id, createdAt, updatedAt)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        MiddleName = middleName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string MiddleName { get; }

    public string Email { get; }

    public string Password { get; }

    public static Result<UserModel, ApiError> Create(string firstName, string lastName, string middleName, string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > FirstNameMaxLength)
        {
            return Result.Failure<UserModel, ApiError>(new ApiError(Messages.InvalidFirstName,
                StatusCodes.Status400BadRequest));
        }

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > LastNameMaxLength)
        {
            return Result.Failure<UserModel, ApiError>(new ApiError(Messages.InvalidLastName,
                StatusCodes.Status400BadRequest));
        }

        if (string.IsNullOrWhiteSpace(middleName) || middleName.Length > MiddleNameMaxLength)
        {
            return Result.Failure<UserModel, ApiError>(new ApiError(Messages.InvalidMiddleName,
                StatusCodes.Status400BadRequest));
        }

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

        var model = new UserModel(0, DateTime.UtcNow, DateTime.UtcNow, firstName, middleName, lastName, email,
            password);
        return Result.Success<UserModel, ApiError>(model);
    }

    public static UserModel FromEntity(UserEntity entity)
    {
        return new UserModel(
            entity.Id,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.FirstName,
            entity.MiddleName,
            entity.LastName,
            entity.Email,
            entity.Password);
    }
}