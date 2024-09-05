using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;

namespace XeroProducts.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserProvider _userProvider;

        public UserController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        /// <summary>
        /// Grab the user that matches the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetUser(Guid id)
        {
            //attempt to grab the matching product
            var user = await _userProvider.GetUser(id);

            //return a Success along with the product
            return Ok(new UserViewModel(user));
        }

        /// <summary>
        /// Create and Persist the User given in the body
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<ProductViewModel>> Create([FromBody] CreateUserFormModel userModel)
        {
            //Convert the model to a type that can be saved
            var userDto = userModel.ToDto();

            //Save the user
            userDto.Id = await _userProvider.CreateUser(userModel.ToDto());

            //Return successfully "Created" action including the newly created product (and it's generated ID)
            return CreatedAtAction(nameof(Create), new UserViewModel(userDto));
        }

        /// <summary>
        /// Delete the User that matches the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<Guid>> Delete(Guid id)
        {
            //Delete the product
            await _userProvider.DeleteUser(id);

            //return Success along with the id of the product that was deleted
            return Ok(id);
        }

    }
}
