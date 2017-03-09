using SampleReview.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.Data.Context {
    public interface IDbContextFactory : IFactory<IDbContext>, IDisposable {

    }
}
