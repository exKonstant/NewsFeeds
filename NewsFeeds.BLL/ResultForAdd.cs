using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeeds.BLL
{
    public class ResultForAdd
    {
        public bool IsFailure { get; }
        public string Message { get; }

        protected ResultForAdd(bool isFailure, string message)
        {
            IsFailure = isFailure;
            Message = message;
        }

        public static ResultForAdd Fail(string message)
        {
            return new ResultForAdd(true, message);
        }

        public static ResultForAdd Fail<T>(string message)
        {
            return new ResultForAdd<T>(default(T), true, message);
        }

        public static ResultForAdd Ok()
        {
            return new ResultForAdd(false, string.Empty);
        }

        public static ResultForAdd Ok<T>(T value, string message = "")
        {
            return new ResultForAdd<T>(value, false, message);
        }
    }


    public class ResultForAdd<T> : ResultForAdd
    {
        public T Value { get; }

        protected internal ResultForAdd(T value, bool isFailure, string message) : base(isFailure, message)
        {
            Value = value;
        }
    }
}
