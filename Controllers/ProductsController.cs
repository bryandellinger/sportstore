using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsStore.Models;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class ProductsController : ApiController
    {
        private IRepository Repository { get; set; }
        public ProductsController()
        {
            Repository = new ProductRepository();
        }
        public IEnumerable<Product> GetProducts()
        {
            return Repository.Products;
        }
        public IHttpActionResult GetProduct(int id)
        {
            try
            {
                Product result = Repository.Products.Where(p => p.Id == id).FirstOrDefault();
                return result == null
                ? (IHttpActionResult)BadRequest("No Product Found") : Ok(result);
            }
            catch ( Exception ex )
            {

                return BadRequest(ex.Message);
            }
            
        }
        public async Task PostProduct(Product product)
        {
            await Repository.SaveProductAsync(product);
        }

        [Authorize(Roles = "Administrators")]
        public async Task DeleteProduct(int id)
        {
            await Repository.DeleteProductAsync(id);
        }
    }
}
