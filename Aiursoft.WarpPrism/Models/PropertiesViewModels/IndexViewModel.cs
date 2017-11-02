using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aiursoft.WarpPrism.Models.PropertiesViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Property> AllProperTies { get; set; }
        public int DatabaseId { get; set; }
        public int TableId { get; set; }
    }
}
