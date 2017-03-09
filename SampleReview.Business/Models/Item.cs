using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Business.Models {
    public class Item : AnyModel {

        public string Color { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public int? ReviewCount { get; set; }

        public int? AverageRating { get; set; }

        public int? LowestRating { get; set; }

        public int? HighestRating { get; set; }

        public int? Popularity { get; set; }

        public DateTimeOffset? Date { get; set; }
    }
}
