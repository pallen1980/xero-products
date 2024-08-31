using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using XeroProducts.BL.Providers;
using XeroProducts.Types;

namespace XeroProducts.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private ProductProvider _productProvider => new ProductProvider();
        private ProductOptionProvider _productOptionProvider => new ProductOptionProvider();


        [HttpGet]
        public Products GetAll()
        {
            return _productProvider.GetProducts();
        }

        [HttpGet]
        public Products SearchByName(string name)
        {
            return _productProvider.GetProducts(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = _productProvider.GetProduct(id);
            if (product.IsNew)
                throw new KeyNotFoundException();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [HttpPost]
        public void Create(Product product)
        {
            _productProvider.Save(product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            var orig = _productProvider.GetProduct(id);

            orig.Name = product.Name;
            orig.Description = product.Description;
            orig.Price = product.Price;
            orig.DeliveryPrice = product.DeliveryPrice;

            if (!orig.IsNew)
            {
                _productProvider.Save(orig);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _productProvider.Delete(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return _productOptionProvider.GetProductOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = _productOptionProvider.GetProductOption(id);
            if (option.IsNew)
                throw new KeyNotFoundException();
                //throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            _productOptionProvider.Save(option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var orig = _productOptionProvider.GetProductOption(id);

            orig.Name = option.Name;
            orig.Description = option.Description;

            if (!orig.IsNew)
                _productOptionProvider.Save(orig);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            _productOptionProvider.Delete(id);
        }
    }
}
