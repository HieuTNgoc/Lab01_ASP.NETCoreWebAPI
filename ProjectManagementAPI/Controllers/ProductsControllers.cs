using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/product/[action]")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
     
        private IProductRepository repository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() => repository.GetCategories();

        [HttpGet("id")]
        public ActionResult<Product> GetProductById(int id) => repository.GetProductById(id);
		
		[HttpPost]
        public IActionResult PostProduct(Product p)
        {
            repository.SaveProduct(p);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NoContent();
            }
            repository.DeleteProduct(p);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product p)
        {
            var pTmp = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound();
            }
            repository.UpdateProduct(p);
            return NoContent();
        }

    }

	[Route("api/pro")]
	[ApiController]
	public class ProControllers : ControllerBase
	{

		private IProductRepository repository = new ProductRepository();

		[HttpGet("get-product-by-id/{id}/detail")]
		public ActionResult<Product> GetPById(int id)
        {
			return repository.GetProductById(id);
		}  


	}
}
