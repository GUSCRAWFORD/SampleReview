using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Business.Models {
    public class Page<TAnyModel>
        where TAnyModel : AnyModel {
        public IEnumerable<TAnyModel> Collection { get; set; }
        public int OfTotalItems { get; set; }
    }
}
