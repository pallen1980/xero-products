using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;
using XeroProducts.Types;

namespace XeroProducts.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private IProductProvider _productProvider;
        private IProductOptionProvider _productOptionProvider;

        public ProductsController(  IProductProvider productProvider,
                                    IProductOptionProvider productOptionProvider)
        {
            _productProvider = productProvider;
            _productOptionProvider = productOptionProvider;
        }


        [HttpGet]
        public async Task<Products> GetAll()
        {
            return await _productProvider.GetProducts();
        }

        [HttpGet]
        public async Task<Products> SearchByName(string name)
        {
            return await _productProvider.GetProducts(name);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _productProvider.GetProduct(id);

            if (product.IsNew)
                throw new KeyNotFoundException();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [HttpPost]
        public async Task Create(Product product)
        {
            await _productProvider.Save(product);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task Update(Guid id, Product product)
        {
            var orig = await _productProvider.GetProduct(id);

            orig.Name = product.Name;
            orig.Description = product.Description;
            orig.Price = product.Price;
            orig.DeliveryPrice = product.DeliveryPrice;

            if (!orig.IsNew)
            {
               await _productProvider.Save(orig);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await _productProvider.Delete(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public async Task<ProductOptions> GetOptions(Guid productId)
        {
            return await _productOptionProvider.GetProductOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public async Task<ProductOption> GetOption(Guid productId, Guid id)
        {
            var option = await _productOptionProvider.GetProductOption(id);
            
            if (option.IsNew)
                throw new KeyNotFoundException();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public async Task CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            
            await _productOptionProvider.Save(option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public async Task UpdateOption(Guid id, ProductOption option)
        {
            var orig = await _productOptionProvider.GetProductOption(id);

            orig.Name = option.Name;
            orig.Description = option.Description;

            if (!orig.IsNew)
                await _productOptionProvider.Save(orig);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public async Task DeleteOption(Guid id)
        {
            await _productOptionProvider.Delete(id);
        }
    }
}
