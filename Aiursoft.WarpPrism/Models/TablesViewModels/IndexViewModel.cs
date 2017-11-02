using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aiursoft.WarpPrism.Models.TablesViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Table> Tables { get; set; }
        public int DatabaseId { get; set; }
    }
}
