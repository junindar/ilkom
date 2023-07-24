using System.Net.Http.Headers;
using System.Net.Http;
using Fido2NetLib;
using Azure.Core;
using System.Text;
using Blazor_Biometric.Entity;
using Fido2NetLib.Objects;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Blazor_Biometric.Models;

namespace Blazor_Biometric.Data
{
    public class UserService: IUserService
    {
        private readonly Fido2 _fido;
        private readonly BioDbContext _dbContext;
        public HttpClient _httpClient { get; }
        public UserService(Fido2 fido, BioDbContext dbContext,HttpClient httpClient)
        {
            _fido = fido;
            _dbContext = dbContext;
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            _httpClient = httpClient;
        }
      

        public async Task RegistrationUser(object obj,FidoUserRegistrationRequest fidoUserRegistration, RegistrationResponseModel responseModel, CancellationToken cancellationToken)
        {
          //  var options = CredentialCreateOptions.FromJson(responseModel.attestationOptions?? fidoUserRegistration.MakeCredentialOptions);
            var success = await _fido.MakeNewCredentialAsync(fidoUserRegistration.Response, responseModel.credentialCreateOptions, async (args, _ctoken) =>
            {
                var credentialIdArray = args.CredentialId.ToArray();
                bool hasUser = await _dbContext.Users.AnyAsync(x => x.Username == args.User.Name);
                return !hasUser;
            }, cancellationToken: cancellationToken);
            var result = success.Result;
            if (result != null) {
                await _dbContext.Users.AddAsync(new User
                {
                    CredentialID = result.CredentialId,
                    Username = result.User.Name,
                    DisplayName = result.User.DisplayName,
                    Counter = result.Counter,
                    UserHandle = result.User.Id,
                    CredType = result.CredType,
                    PublicKey = result.PublicKey,
                });
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Registration failed, please check with Admin");
            }
            
           
        }

        public RegistrationResponseModel GetRegisterOption(User objUser)
        {
            var user = new Fido2User
            {
                Id = Encoding.UTF8.GetBytes(objUser.Username),
                Name = objUser.Username,
                DisplayName = objUser.DisplayName,
            };
            var existingCredentials = new List<PublicKeyCredentialDescriptor>();
            var authenticatorSelection = new AuthenticatorSelection
            {
                RequireResidentKey = false,
                UserVerification = UserVerificationRequirement.Discouraged,
                AuthenticatorAttachment = AuthenticatorAttachment.Platform,
            };
            var exts = new AuthenticationExtensionsClientInputs()
            {
                Extensions = true,
                UserVerificationMethod = true,
            };
            var options = _fido.RequestNewCredential(user, existingCredentials, authenticatorSelection, AttestationConveyancePreference.None, exts);
           
            RegistrationResponseModel result = new RegistrationResponseModel();
            result.credentialCreateOptions = options;
            result.attestationOptions = options.ToJson();
            result.responseObject = options;
            return result;
        }
    }
}
