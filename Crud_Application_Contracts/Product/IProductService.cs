using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Application.Contracts.Product
{
    public interface IProductService
    {
        List<ProductViewModel> GetProducts();
        void CreateProduct(AddProduct entity);
        void UpdateProduct(UpdateProduct entity);
        void DeleteProduct(long id);
    }
}
