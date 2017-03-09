namespace SampleReview.Business.Rules {
    public static class Rules {
        public static class Reviews {
            public static bool HaveMinRating(object review) {
                return true;
            }
        }
    }
}
