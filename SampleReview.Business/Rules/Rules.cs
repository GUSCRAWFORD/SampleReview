namespace SampleReview.Business.Rules {
    public static class Rules {
        public static class Defaults {
            public static readonly string[] OrderBy = new string[] { "id" };
        }
        public static class Reviews {
            public static bool HaveMinRating(object review) {
                return true;
            }
        }
    }
}
