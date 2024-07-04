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

        // This method adds a new product to the system.
        // It checks if a product with the same email and
        // production date already exists. If not, it adds
        // the new product and saves the changes asynchronously
        // If an existing product is found, it throws an exception.
        public async Task CreateProductAsync(Product entity)
        {
            if (!await ExistAsync(entity.ManufactureEmail, entity.ProduceDate , entity.Id))
            {
                _context.AddAsync(entity);
                await SaveAsync();
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
        public async Task DeleteProductAsync(long id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                product.IsAvailable = false;
            }
            await SaveAsync();
        }


        // Retrieves a product by its unique ID.
        // Returns the first product with a matching
        // ID from the database.
        public async Task<Product> GetByIdAsync(long id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }


        // This asynchronous method, named `GetProductsAsync`, retrieves
        // a list of available products from the system. It filters products
        // based on the `IsAvailable` property, returning only those that
        // are marked as available.
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.Where(x => x.IsAvailable == true).ToListAsync();
        }


        //  It’s responsible for persisting changes
        //  made to the database context.
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }



        // Checks if a product with the same email or date exists.
        // If an ID is provided, always returns false (ignores the check).
        public async Task<bool> ExistAsync(string email, DateTime date, long? id)
        {
            var query = _context.Products.AsQueryable();

            if (id.HasValue)
            {
                return true;
            }

            return await query.AnyAsync(x => x.ManufactureEmail == email || x.ProduceDate == date);
        }



        // Updates a product if it exists, based on the provided entity.
        // Throws exceptions for non-existent products or duplicate email/date.
        public async Task UpdateProductAsync(Product entity)
        {
            var product = _context.Products.FindAsync(entity.Id);

            if (product == null)
            {
                throw new Exception("Product with this ID does not exist");
            }
            else if (await ExistAsync(entity.ManufactureEmail, entity.ProduceDate, entity.Id))
            {
                throw new Exception("Email or Date Already exist");
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
                await SaveAsync();
            }
        }


    }
}
