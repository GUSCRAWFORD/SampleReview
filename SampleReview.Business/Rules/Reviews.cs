using SampleReview.Business.Exceptions;
using SampleReview.Business.Models;

namespace SampleReview.Business.Rules
{
    public static class Reviews
    {
        public static int MinRating = 1;
        public static int MaxRating = 5;
        public static int MinCommentLen = 4;

        public static bool HaveValidRating(Review review)
        {
            if(review.Rating >= MinRating && review.Rating <= MaxRating) return true;
            throw new RatingOutOfBoundsException(review.Rating);
        }
        public static bool HaveCommentWithMinLen(Review review)
        {
            if(review.Comment.Length >= MinCommentLen) return true;
            throw new CommentTooShortException(review.Comment);
        }
        public static bool HaveComment(Review review)
        {
            if (review.Comment != null && review.Comment != string.Empty) return true;
            throw new CommentRequiredException();
        }
    }
}
