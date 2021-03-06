﻿using System;
using System.Runtime.Serialization;

namespace SampleReview.Business.Exceptions
{
    public class ReviewException : Exception, ISerializable
    {
        public ReviewException(string msg) : base(msg) { }

        public static class Messages
        {
            public const string RatingOutOfBounds = "rating '{0}' is not valid (between {1}, {2})";
            public const string CommentTooShort = "comment '{0}' is not long enough (at least {1} chars)";
            public const string CommentRequired = "comment is required";
        }
    }

    public class RatingOutOfBoundsException : ReviewException
    {
        public RatingOutOfBoundsException(int badRating)
            : base(String.Format(Messages.RatingOutOfBounds,
                badRating,
                Rules.Reviews.MinRating,
                Rules.Reviews.MaxRating)
            ) { }
    }
    public class CommentTooShortException : ReviewException
    {
        public CommentTooShortException(string shortComment)
            : base(String.Format(Messages.CommentTooShort,
                shortComment,
                Rules.Reviews.MinCommentLen)
            )
        { }
    }
    public class CommentRequiredException : ReviewException
    {
        public CommentRequiredException()
            : base(Messages.CommentRequired)
        { }
    }
}
