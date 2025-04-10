namespace Magato.Api.Shared;
public class Result
{
    public bool IsSuccess { get; }
    public List<string> Errors { get; } = new();

    private Result(bool success, List<string>? errors = null)
    {
        IsSuccess = success;
        if (errors != null)
            Errors = errors;
    }

    public static Result Success() => new(true);
    public static Result Failure(List<string> errors) => new(false, errors);
}
