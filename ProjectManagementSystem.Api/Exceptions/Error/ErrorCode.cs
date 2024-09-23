
namespace ProjectManagementSystem.Api.Exceptions.Error;

public enum ErrorCode
{
    NoError = 0,
    UnKnown,
    BadRequest = 400,
    UareNotAuthorized = 401,
    ResourceNotFound = 404, 
    InternalserverError = 500,
    TokenGenerationError = 600,
    ValidationError = 700
}
