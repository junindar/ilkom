using Blazor_Biometric.Entity;
using Blazor_Biometric.Models;
using Fido2NetLib;

namespace Blazor_Biometric.Data
{
    public interface IUserService
    {
         Task RegistrationUser(object obj,FidoUserRegistrationRequest fidoUserRegistration, RegistrationResponseModel responseModel, CancellationToken cancellationToken);

        RegistrationResponseModel GetRegisterOption(User objUser);
    }
}
