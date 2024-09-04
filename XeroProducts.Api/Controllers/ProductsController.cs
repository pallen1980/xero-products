using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;

namespace XeroProducts.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider _productProvider;
        private readonly IProductOptionProvider _productOptionProvider;

        public ProductsController(  IProductProvider productProvider,
                                    IProductOptionProvider productOptionProvider)
        {
            _productProvider = productProvider;
            _productOptionProvider = productOptionProvider;
        }

        /// <summary>
        /// Grab All Products
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ActionResult<ProductsViewModel>> GetAll()
        {
            return Ok(new ProductsViewModel(await _productProvider.GetProducts()));
        }


        /// <summary>
        /// Return all any product that contains the given string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<ProductsViewModel>> SearchByName(string name)
        {
            return Ok(new ProductsViewModel(await _productProvider.GetProducts(name)));
        }

        /// <summary>
        /// Return all the options for the given product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productId}/options")]
        public async Task<ActionResult<ProductOptionsViewModel>> GetOptions(Guid productId)
        {
            return Ok(new ProductOptionsViewModel(await _productOptionProvider.GetProductOptions(productId)));
        }

    }
}
