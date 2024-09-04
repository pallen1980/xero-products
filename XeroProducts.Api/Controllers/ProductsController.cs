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

        [HttpGet]
        public async Task<ActionResult<ProductsViewModel>> GetAll()
        {
            return Ok(new ProductsViewModel(await _productProvider.GetProducts()));
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<ProductsViewModel>> SearchByName(string name)
        {
            return Ok(new ProductsViewModel(await _productProvider.GetProducts(name)));
        }

        [HttpGet]
        [Route("{productId}/options")]
        public async Task<ActionResult<ProductOptionsViewModel>> GetOptions(Guid productId)
        {
            return Ok(new ProductOptionsViewModel(await _productOptionProvider.GetProductOptions(productId)));
        }

    }
}
