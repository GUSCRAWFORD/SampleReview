using System;
namespace SampleReview.Business.Exceptions
{
    public class ReviewException : Exception
    {
        public ReviewException(string msg) : base(msg) { }

        public static class Messages
        {
            public const string RatingOutOfBounds = "rating '{0}' is not valid (between {1}, {2})";
            public const string CommentTooShort = "comment '{0}' is not long enough (at least {1} chars)";
        }
    }

    public class RatingOutOfBoundsException : ReviewException
    {
        public RatingOutOfBoundsException(int badRating)
            : base(String.Format(ReviewException.Messages.RatingOutOfBounds,
                badRating,
                Rules.Reviews.MinRating,
                Rules.Reviews.MaxRating)
            ) { }
    }
    public class CommentTooShortException : ReviewException
    {
        public CommentTooShortException(string shortComment)
            : base(String.Format(ReviewException.Messages.CommentTooShort,
                shortComment,
                Rules.Reviews.MinCommentLen)
            )
        { }
    }
}
