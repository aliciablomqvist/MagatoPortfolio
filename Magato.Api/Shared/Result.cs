// <copyright file="Result.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Shared;
public class Result
{
    public bool IsSuccess
    {
        get;
    }

    public List<string> Errors { get; } = new();

    private Result(bool success, List<string>? errors = null)
    {
        this.IsSuccess = success;
        if (errors != null)
        {
            this.Errors = errors;
        }
    }

    public static Result Success() => new(true);

    public static Result Failure(List<string> errors) => new(false, errors);
}
