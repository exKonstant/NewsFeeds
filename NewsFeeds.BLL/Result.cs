using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeeds.BLL
{
    public class Result
    {
        public bool IsFailure { get; }
        public string Message { get; }

        protected Result(bool isFailure, string message)
        {
            IsFailure = isFailure;
            Message = message;
        }

        public static Result Fail(string message)
        {
            return new Result(true, message);
        }

        public static Result Fail<T>(string message)
        {
            return new Result<T>(default(T), true, message);
        }

        public static Result Ok()
        {
            return new Result(false, string.Empty);
        }

        public static Result Ok<T>(T value, string message = "")
        {
            return new Result<T>(value, false, message);
        }
    }


    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result(T value, bool isFailure, string message) : base(isFailure, message)
        {
            Value = value;
        }
    }
}
