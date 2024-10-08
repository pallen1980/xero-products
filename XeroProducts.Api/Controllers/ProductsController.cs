﻿using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;

namespace XeroProducts.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly Lazy<IProductProvider> _productProvider;
        private readonly Lazy<IProductOptionProvider> _productOptionProvider;

        protected IProductProvider ProductProvider => _productProvider.Value;
        protected IProductOptionProvider ProductOptionProvider => _productOptionProvider.Value;

        public ProductsController(Lazy<IProductProvider> productProvider,
                                  Lazy<IProductOptionProvider> productOptionProvider)
        {
            _productProvider = productProvider;
            _productOptionProvider = productOptionProvider;
        }

        #region Products_Endpoints

        /// <summary>
        /// Grab All Products
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public virtual async Task<ActionResult<ProductsViewModel>> GetAll()
        {
            return Ok(new ProductsViewModel(await ProductProvider.GetProducts()));
        }


        /// <summary>
        /// Return all any product that contains the given string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}")]
        public virtual async Task<ActionResult<ProductsViewModel>> SearchByName(string name)
        {
            return Ok(new ProductsViewModel(await ProductProvider.GetProducts(name)));
        }

        #endregion

    }
}
