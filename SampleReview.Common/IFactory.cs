using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Common {
    public interface IFactory<T> {
        T Instance { get; }
    }
}
