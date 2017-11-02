using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aiursoft.WarpPrism.Models.ItemsViewModels
{
    public class EditViewModel
    {
        public List<Property> Properties { get; set; }
        public int TableId { get; set; }
        public int TargetItemId { get; set; }
    }
}
