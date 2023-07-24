using Blazor_BiometricWebApi.DataAccess;
using Blazor_BiometricWebApi.Models.Requests;
using Fido2NetLib.Objects;
using Fido2NetLib;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blazor_BiometricWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly Fido2 _fido;
        private readonly TheDbContext _dbContext;
        public AuthController(Fido2 fido, TheDbContext dbContext)
        {
            _fido = fido;
            _dbContext = dbContext;
        }

        [HttpPost("registration-options")]
        public IActionResult GetRegistrationOptions([FromBody] RegistrationRequest request)
        {
            try
            {
                var user = new Fido2User
                {
                    Id = Encoding.UTF8.GetBytes(request.Username),
                    Name = request.Username,
                    DisplayName = request.DisplayName,
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
                HttpContext.Session.SetString("fido2.attestationOptions", options.ToJson());

                return CreateApiResult(success: true, "Registration options retrieved successfully!", options);
            }
            catch (Exception ex)
            {
                return CreateApiResultForException();
            }
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] FidoUserRegistrationRequest fidoUserRegistration, CancellationToken cancellationToken)
        {
            try
            {
                // 1. get the options we sent to the client
                var jsonOptions = HttpContext.Session.GetString("fido2.attestationOptions");
                var options = CredentialCreateOptions.FromJson(jsonOptions ?? fidoUserRegistration.MakeCredentialOptions);

                // 2. Create callback so that lib can verify credential id is unique to this user
                var success = await _fido.MakeNewCredentialAsync(fidoUserRegistration.Response, options, async (args, ctoken) =>
                {
                    var credentialIdArray = args.CredentialId.ToArray();
                    bool hasUser = await _dbContext.Users.AnyAsync(x => x.Username == args.User.Name);
                    return !hasUser;
                }, cancellationToken: cancellationToken);
                var result = success.Result;
                await _dbContext.Users.AddAsync(new DataAccess.Models.User
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
                return CreateApiResult(true, "Registration successfull!", success);
            }
            catch (Exception ex)
            {
                return CreateApiResultForException();
            }
        }

        [HttpPost("login-options")]
        public async Task<IActionResult> GetLoginOptions([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _dbContext.Users.Where(x => x.Username == request.Username).Select(x => new
                {
                    x.CredentialID,
                }).FirstOrDefaultAsync();
                if (user is null)
                {
                    return CreateApiResult(false, "User not found!");
                }
                var existingCredentials = new List<PublicKeyCredentialDescriptor>
                {
                    new PublicKeyCredentialDescriptor
                    {
                        Type = PublicKeyCredentialType.PublicKey,
                        Id = user.CredentialID,
                    },
                };
                var exts = new AuthenticationExtensionsClientInputs()
                {
                    Extensions = true,
                    UserVerificationMethod = true,
                };
                var options = _fido.GetAssertionOptions(existingCredentials, UserVerificationRequirement.Discouraged, exts);
                HttpContext.Session.SetString("fido2.assertionOptions", options.ToJson());
                return CreateApiResult(true, "Login options retrieved successfully!", options);
            }
            catch (Exception ex)
            {
                return CreateApiResultForException();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] FidoUserLoginRequest fidoUserLogin, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Get the assertion options we sent the client
                var jsonOptions = HttpContext.Session.GetString("fido2.assertionOptions");
                var options = AssertionOptions.FromJson(jsonOptions ?? fidoUserLogin.MakeAssertionOptions);
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.CredentialID.SequenceEqual(fidoUserLogin.Response.Id));
                if (user is null)
                {
                    return CreateApiResult(false, "user not found");
                }
                var result = await _fido.MakeAssertionAsync(fidoUserLogin.Response, options, user.PublicKey, user.Counter, (args, ctoken) =>
                {
                    bool hasUserHandle = args.UserHandle.SequenceEqual(user.UserHandle);
                    return Task.FromResult(hasUserHandle);
                }, cancellationToken: cancellationToken);
                user.Counter = result.Counter;
                await _dbContext.SaveChangesAsync();
                return CreateApiResult(true, "Logged in successfull!", result);
            }
            catch (Exception ex)
            {
                return CreateApiResultForException();
            }
        }
    }
}
