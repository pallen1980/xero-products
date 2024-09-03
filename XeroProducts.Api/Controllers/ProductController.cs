using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;

[ApiController]
[Route("api/product")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductProvider _productProvider;
    private readonly IProductOptionProvider _productOptionProvider;

    public ProductController(IProductProvider productProvider,
                             IProductOptionProvider productOptionProvider)
    {
        _productProvider = productProvider;
        _productOptionProvider = productOptionProvider;
    }

    #region Product_Endpoints

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ProductViewModel>> GetProduct(Guid id)
    {
        var product = await _productProvider.GetProduct(id);

        if (product == null)
            return NotFound(id);
            
        return Ok(new ProductViewModel(product));
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<ProductViewModel>> Create([FromBody] CreateProductFormModel productModel)
    {
        var product = productModel.ToType();

        await _productProvider.Save(product);

        return CreatedAtAction(nameof(Create), new ProductViewModel(product));
    }

    [HttpPut]
    [Route("{id}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<ProductViewModel>> Update(Guid id, [FromBody] UpdateProductFormModel product)
    {
        var orig = await _productProvider.GetProduct(id);

        orig.Name = product.Name;
        orig.Description = product.Description ?? "";
        orig.Price = product.Price;
        orig.DeliveryPrice = product.DeliveryPrice;

        if (orig != null)
        {
            await _productProvider.Save(orig);
        }
        else
        {
            return NotFound(id);
        }

        return Ok(new ProductViewModel(orig));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
        await _productProvider.Delete(id);

        return Ok(id);
    }

    #endregion

    #region ProductOption_Endpoints

    [HttpGet]
    [Route("{productId}/option/{id}")]
    public async Task<ActionResult<ProductOptionViewModel>> GetOption(Guid productId, Guid id)
    {
        var option = await _productOptionProvider.GetProductOption(id);

        if (option.IsNew)
            return NotFound(id);

        return Ok(new ProductOptionViewModel(option));
    }

    [HttpPost]
    [Route("{productId}/option")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<ProductOptionViewModel>> CreateOption(Guid productId, [FromBody] CreateProductOptionFormModel option)
    {
        var productOption = option.ToType(productId);

        await _productOptionProvider.Save(productOption);

        return CreatedAtAction(nameof(CreateOption), new ProductOptionViewModel(productOption));
    }

    [HttpPut]
    [Route("{productId}/option/{id}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<ProductOptionViewModel>> UpdateOption(Guid productId, Guid id, [FromBody] UpdateProductOptionFormModel option)
    {
        var orig = await _productOptionProvider.GetProductOption(id);

        orig.Name = option.Name;
        orig.Description = option.Description ?? "";

        if (!orig.IsNew)
            await _productOptionProvider.Save(orig);
        else
            return NotFound(new { ProductId = productId, Id = id });

        return Ok(new ProductOptionViewModel(orig));
    }

    [Route("{productId}/options/{id}")]
    [HttpDelete]
    public async Task<ActionResult<Guid>> DeleteOption(Guid id)
    {
        await _productOptionProvider.Delete(id);

        return Ok(id);
    }

    #endregion
}
