namespace Authorization.Core.Contracts;

public record ApiError(string Message, int StatusCode);