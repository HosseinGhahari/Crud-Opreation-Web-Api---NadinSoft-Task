using Crud_Application.Contracts.Product;
using Crud_Domain;
using Crud_Opreation.Context;
using Microsoft.EntityFrameworkCore;

namespace Crud_Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        // The injected MainContext allows access to the underlying database.
        private readonly MainContext _context;
        public ProductRepository(MainContext context)
        {
            _context = context;
        }

        // Adds a new product if not already existing.
        // Checks for duplicate ManufactureEmail and ProduceDate.
        // Persists changes to the database using Save().
        // Throws an exception if the product already exists.
        public void CreateProduct(AddProduct entity)
        {
            if (!Exist(entity.ManufactureEmail, entity.ProduceDate ))
            {
                var addproduct = new Product()
                {
                    Name = entity.Name,
                    ProduceDate = entity.ProduceDate,
                    ManufactureEmail = entity.ManufactureEmail,
                    ManufacturePhone = entity.ManufacturePhone,
                    IsAvailable = entity.IsAvailable
                };

                _context.Add(addproduct);
                
                Save();
            }
            else
            {
                throw new Exception("Email or Date Already exist");
            }
        }

        // Deletes a product by setting its availability to false.
        // If the product exists (based on the provided ID),
        // it marks it as unavailable.Persists changes to
        // the database using Save().
        public void DeleteProduct(long id)
        {
            var product = GetById(id);
            if (product != null)
            {
                product.IsAvailable = false;
            }
            Save();
        }

        // Retrieves a product by its unique ID.
        // Returns the first product with a matching
        // ID from the database.
        public Product GetById(long id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        // Retrieves available products as ProductViewModels.
        // Selects relevant properties (Id, Name, ProduceDate,
        // ManufacturePhone, ManufactureEmail, IsAvailable).
        // Filters by IsAvailable == true and returns a list.
        public List<ProductViewModel> GetProducts()
        {
            var query = _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ProduceDate = x.ProduceDate,
                ManufacturePhone = x.ManufacturePhone,
                ManufactureEmail = x.ManufactureEmail,
                IsAvailable = x.IsAvailable,
            });

            return query.Where(x => x.IsAvailable == true).ToList();
        }

        //  It’s responsible for persisting changes
        //  made to the database context.
        public void Save()
        {
            _context.SaveChanges();
        }

        // Checks if a product with the same email or date exists.
        // If an ID is provided, always returns false (ignores the check).
        public bool Exist(string email, DateTime date, long? id = null)
        {
            var query = _context.Products.AsQueryable();

            if (id.HasValue)
            {
                return false;
            }

            return query.Any(x => x.ManufactureEmail == email || x.ProduceDate == date);
        }

        // Updates a product if it exists, based on the provided entity.
        // Throws exceptions for non-existent products or duplicate email/date.
        public void UpdateProduct(UpdateProduct entity)
        {
            var product = _context.Products.Find(entity.Id);

            if (product == null)
            {
                throw new Exception("Product with this ID does not exist");
            }
            else if (Exist(entity.ManufactureEmail, entity.ProduceDate, entity.Id))
            {
                throw new Exception("Email or Date Already exist");
            }
            else
            {
                var updateProduct = new Product()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ProduceDate = entity.ProduceDate,
                    ManufactureEmail = entity.ManufactureEmail,
                    ManufacturePhone = entity.ManufacturePhone,
                    IsAvailable = entity.IsAvailable
                };
                _context.Entry(updateProduct).State = EntityState.Modified;
                Save();
            }
        }
    }
}
