using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Application.Contracts.Product
{
    // Represents input data for creating a new product.
    // Includes properties like Name, ProduceDate, ManufacturePhone,
    // ManufactureEmail, and IsAvailable.
    // Annotated with [Required] to ensure necessary information.
    public class AddProduct
    {
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product date is required.")]
        public DateTime ProduceDate { get; set; }
        [Required(ErrorMessage = "Product phone is required.")]
        public int ManufacturePhone { get; set; }
        [Required(ErrorMessage = "Product email is required.")]
        public string ManufactureEmail { get; set; }
        [Required(ErrorMessage = "Product inventory status is required.")]
        public bool IsAvailable { get; set; }
    }
}
