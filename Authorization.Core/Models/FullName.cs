using Authorization.Core.Contracts;
using CSharpFunctionalExtensions;

namespace Authorization.Core.Models;

public class FullName : ValueObject
{
    public const int FirstNameMaxLength = 20;

    public const int LastNameMaxLength = 20;

    public const int MiddleNameMaxLength = 20;

    private FullName(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string MiddleName { get; }

    public static Result<FullName, ApiError> Create(string firstName, string lastName, string middleName)
    {
        if (!string.IsNullOrWhiteSpace(firstName) || firstName.Length > FirstNameMaxLength)
            return Result.Failure<FullName, ApiError>(new ApiError(Messages.InvalidFirstName,
                StatusCodes.Status400BadRequest));

        if (!string.IsNullOrWhiteSpace(lastName) || lastName.Length > LastNameMaxLength)
            return Result.Failure<FullName, ApiError>(new ApiError(Messages.InvalidLastName,
                StatusCodes.Status400BadRequest));

        if (!string.IsNullOrWhiteSpace(middleName) || middleName.Length > MiddleNameMaxLength)
            return Result.Failure<FullName, ApiError>(new ApiError(Messages.InvalidMiddleName,
                StatusCodes.Status400BadRequest));

        return Result.Success<FullName, ApiError>(new FullName(firstName, lastName, middleName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}