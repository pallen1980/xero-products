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
        public async Task<ActionResult<UserViewModel>> GetUser(Guid id)
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
        public async Task<ActionResult<UserViewModel>> Create([FromBody] CreateUserFormModel userModel)
        {
            //Convert the model to a dto that can be passed to BL
            var userDto = userModel.ToDto();

            //Save the user
            userDto.Id = await _userProvider.CreateUser(userModel.ToDto());

            //Return successfully "Created" action including the newly created product (and it's generated ID)
            return CreatedAtAction(nameof(Create), new UserViewModel(userDto));
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<UserViewModel>> Update(Guid id, [FromBody] UpdateUserFormModel userModel)
        {
            //Convert the model to a dto that can be passed to BL
            var userDto = userModel.ToDto();
            userDto.Id = id;

            //Update the user
            await _userProvider.UpdateUser(userDto);

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
        public async Task<ActionResult<Guid>> Delete(Guid id)
        {
            //Delete the product
            await _userProvider.DeleteUser(id);

            //return Success along with the id of the product that was deleted
            return Ok(id);
        }

    }
}
