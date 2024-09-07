using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using XeroProducts.Api.FormModels.User;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;

namespace XeroProducts.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly Lazy<IUserProvider> _userProvider;

        protected IUserProvider UserProvider => _userProvider.Value;

        public UserController(Lazy<IUserProvider> userProvider)
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
        public virtual async Task<ActionResult<UserViewModel>> GetUser(Guid id)
        {
            //attempt to grab the matching product
            var user = await UserProvider.GetUser(id);

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
        public virtual async Task<ActionResult<UserViewModel>> Create([FromBody] CreateUserFormModel userModel)
        {
            //Convert the model to a dto that can be passed to BL
            var userDto = userModel.ToDto();

            //Save the user
            userDto.Id = await UserProvider.CreateUser(userModel.ToDto());

            //Return successfully "Created" action including the newly created product (and it's generated ID)
            return CreatedAtAction(nameof(Create), new UserViewModel(userDto));
        }

        /// <summary>
        /// Find the user matching the given ID, and update it with the details given in the body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public virtual async Task<ActionResult<UserViewModel>> Update(Guid id, [FromBody] UpdateUserFormModel userModel)
        {
            //Convert the model to a dto that can be passed to BL
            var userDto = userModel.ToDto();
            userDto.Id = id;

            //Update the user
            await UserProvider.UpdateUser(userDto);

            //return a Success along with the updated user
            return Ok(new UserViewModel(userDto));
        }

        /// <summary>
        /// Delete the User that matches the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public virtual async Task<ActionResult<Guid>> Delete(Guid id)
        {
            //Delete the product
            await UserProvider.DeleteUser(id);

            //return Success along with the id of the product that was deleted
            return Ok(id);
        }

    }
}
