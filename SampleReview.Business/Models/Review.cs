using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Business.Models {
    public class Review : AnyModel {
        public string Comment { get; set; }

        public int Rating { get; set; }

        public int Reviewing { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
