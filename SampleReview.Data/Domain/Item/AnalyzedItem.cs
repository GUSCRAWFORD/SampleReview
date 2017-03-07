namespace SampleReview.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AnalyzedItem : AnyItem
    {
        public int? ReviewCount { get; set; }

        public int? AverageRating { get; set; }

        public int? LowestRating { get; set; }

        public int? HighestRating { get; set; }

        public int? Popularity { get; set; }

        public DateTimeOffset? Date { get; set; }
    }
}
