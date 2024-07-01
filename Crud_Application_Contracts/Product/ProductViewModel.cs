using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Application.Contracts.Product
{
    // Represents a view model for displaying product information.
    // Includes properties like Id, Name, ProduceDate, ManufacturePhone,
    // ManufactureEmail, and IsAvailable.
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public int ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
    }

}
