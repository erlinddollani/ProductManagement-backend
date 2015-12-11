using APM.WebAPI.Models;
using ProductManagement.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;

namespace ProductManagement.WebApi.Controllers
{   
    /*enable CORS from all the class, second star is for allowed headers, third star is for allowed methods*/

    [EnableCorsAttribute("http://localhost:8125", "*", "*")]
    
    public class ProductsController : ApiController
    {
        // GET: api/Products
        [EnableQuery()]
        public IQueryable<Product> Get()
        {
            var productRepository = new ProductRepository();
            return productRepository.Retrieve().AsQueryable();
        }

        // GET: api/Products/5
        public Product Get(int id)
        {
            Product product;
            var productRepository = new ProductRepository();

            if (id > 0)
            {
                var products = productRepository.Retrieve();
                product = products.FirstOrDefault(p => p.ProductId == id);
            }
            else
            {
                product = productRepository.Create();
            }
            return product;
        }

        // GET: Query String (with OData we dont need these anymore)
        public IEnumerable<Product> Get(string search)
        {
            var productRepository = new ProductRepository();
            var products = productRepository.Retrieve();

            /*we will use 'search' because 'this paraneter name' must match the 'query string parameter' from the frontend */
            if (search != "" && search != null)
            {   
                return products.Where(p => p.ProductCode.Contains(search));
            }
            else
            {
                return products;
            }
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }

    }
}
