namespace SampleReview.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review : AnyDomainModel
    {

        [Required]
        [StringLength(280)]
        public string Comment { get; set; }

        public int Rating { get; set; }

        public int Reviewing { get; set; }

        public DateTimeOffset Date { get; set; }

        public virtual Item Item { get; set; }
    }
}
