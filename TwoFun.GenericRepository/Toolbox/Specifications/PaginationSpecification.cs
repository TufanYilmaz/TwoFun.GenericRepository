using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFun.GenericRepository.Toolbox.Specifications
{
    public class PaginationSpecification<T> : SpecificationBase<T>
        where T: class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
