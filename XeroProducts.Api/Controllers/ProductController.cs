using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;

[ApiController]
[Route("api/product")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly Lazy<IProductProvider> _productProvider;
    private readonly Lazy<IProductOptionProvider> _productOptionProvider;

    protected IProductProvider ProductProvider => _productProvider.Value;
    protected IProductOptionProvider ProductOptionProvider => _productOptionProvider.Value;

    public ProductController(Lazy<IProductProvider> productProvider,
                             Lazy<IProductOptionProvider> productOptionProvider)
    {
        _productProvider = productProvider;
        _productOptionProvider = productOptionProvider;
    }

    #region Product_Endpoints

    /// <summary>
    /// Grab the product that matches the given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public virtual async Task<ActionResult<ProductViewModel>> GetProduct(Guid id)
    {
        //attempt to grab the matching product
        var product = await ProductProvider.GetProduct(id);

        //raise an exception if no matching product was found...
        if (product == null)
            throw new KeyNotFoundException(string.Format("Product with ID: {0} was not found", id));
        
        //return a Success along with the product
        return Ok(new ProductViewModel(product));
    }

    /// <summary>
    /// Create and Persist the Product given in the body
    /// </summary>
    /// <param name="productModel"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public virtual async Task<ActionResult<ProductViewModel>> Create([FromBody] CreateProductFormModel productModel)
    {
        //Convert the model to a type that can be saved
        var product = productModel.ToDto();

        //Save the type
        await ProductProvider.Save(product);

        //Return successfully "Created" action including the newly created product (and it's generated ID)
        return CreatedAtAction(nameof(Create), new ProductViewModel(product));
    }

    /// <summary>
    /// Update the Product that matches the given ID, to the state given in the body
    /// </summary>
    /// <param name="id"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("{id}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public virtual async Task<ActionResult<ProductViewModel>> Update(Guid id, [FromBody] UpdateProductFormModel product)
    {
        //try and grab the existing product
        var orig = await ProductProvider.GetProduct(id);

        //if we cant find a match, raise an exception...
        if (orig == null)
        {
            throw new KeyNotFoundException(string.Format("Product with ID: {0} was not found", id));
        }

        //update the product
        orig.Name = product.Name;
        orig.Description = product.Description ?? "";
        orig.Price = product.Price;
        orig.DeliveryPrice = product.DeliveryPrice;

        //save the updated properties
        await ProductProvider.Save(orig);

        //return a Success along with the updated product
        return Ok(new ProductViewModel(orig));
    }

    /// <summary>
    /// Delete the Product that matches the given ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    [Route("{id}")]
    public virtual async Task<ActionResult<Guid>> Delete(Guid id)
    {
        //Delete the product
        await ProductProvider.Delete(id);

        //return Success along with the id of the product that was deleted
        return Ok(id);
    }

    #endregion

    #region ProductOption_Endpoints

    /// <summary>
    /// Return all the options for the given product ID
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{productId}/options")]
    public virtual async Task<ActionResult<ProductOptionsViewModel>> GetOptions(Guid productId)
    {
        return Ok(new ProductOptionsViewModel(await ProductOptionProvider.GetProductOptions(productId)));
    }

    /// <summary>
    /// Grab the Option that matches the id
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{productId}/option/{id}")]
    public virtual async Task<ActionResult<ProductOptionViewModel>> GetOption(Guid productId, Guid id)
    {
        //Grab the option that matches the id
        var option = await ProductOptionProvider.GetProductOption(id);

        //if we didnt find one, raise an exception
        if (option.IsNew)
        {
            throw new KeyNotFoundException(string.Format("Option with ID: {0} was not found", id));
        }

        //return a Success code along with the found option
        return Ok(new ProductOptionViewModel(option));
    }

    /// <summary>
    /// Create and Persist the Option from the body
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [Route("{productId}/option")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public virtual async Task<ActionResult<ProductOptionViewModel>> CreateOption(Guid productId, [FromBody] CreateProductOptionFormModel option)
    {
        //convert the option to a type we can save
        var productOption = option.ToDto(productId);

        //save the new option
        await ProductOptionProvider.Save(productOption);

        //return a successful Created action and the newly created option (which will contain its newly generated ID)
        return CreatedAtAction(nameof(CreateOption), new ProductOptionViewModel(productOption));
    }

    /// <summary>
    /// Update the matching Option with the state from the body
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="id"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    [Route("{productId}/option/{id}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public virtual async Task<ActionResult<ProductOptionViewModel>> UpdateOption(Guid productId, Guid id, [FromBody] UpdateProductOptionFormModel option)
    {
        //attempt to grab the matching option from persisted storage
        var orig = await ProductOptionProvider.GetProductOption(id);

        //if we didnt find it, raise an exception
        if (orig.IsNew)
        {
            throw new KeyNotFoundException(string.Format("Option with ID: {0} was not found", id));
        }

        //update the properties on the existing option
        orig.Name = option.Name;
        orig.Description = option.Description ?? "";

        //save the updated option
        await ProductOptionProvider.Save(orig);

        //return a Success code along with the updated option
        return Ok(new ProductOptionViewModel(orig));
    }

    /// <summary>
    /// Delete the matching Option
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    [Route("{productId}/options/{id}")]
    public virtual async Task<ActionResult<Guid>> DeleteOption(Guid productId, Guid id)
    {
        //delete the matching option
        await ProductOptionProvider.Delete(id);

        //return a Success along with the ID of the deleted option
        return Ok(id);
    }

    #endregion
}
