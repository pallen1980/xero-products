using XeroProducts.BL.Dtos.User;

namespace XeroProducts.Api.FormModels.User
{
    public class CreateUserFormModel : UserModel
    {
        /// <summary>
        /// Convert this FormModel to a Dto
        /// </summary>
        /// <returns></returns>
        public UserDto ToDto()
        {
            return new UserDto()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username,
                Password = Password
            };
        }
    }
}
