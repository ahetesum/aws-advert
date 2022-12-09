
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime.Internal.Transform;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webAdvert.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAdvert.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;



        public AccountController(SignInManager<CognitoUser> signInManager, UserManager<CognitoUser> userManager,CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;

        }
        [HttpGet]
        public async Task<string> Index()
        {
            return "Something went Wrong";

        }



        // POST api/values
        [HttpPost("signup")]
        public async Task<string> Signup(SignupModel signupModel)
        {

            var user= _pool.GetUser(signupModel.Email);

            if (user.Status != null)
            {
                return "User already Exists!";
            }

            user.Attributes.Add(CognitoAttribute.Name.ToString(), signupModel.Email);
            var createUser = await _userManager.CreateAsync(user,signupModel.Password).ConfigureAwait(false);
            if (createUser.Succeeded)
            {
                return "Succesfully User Created";
            }

            return "Something went Wrong";


        }

        // POST api/values
        [HttpPost("confirm")]
        public async Task<string> Confirm(ConfirmModel confirmModel)
        {

            var user = await _userManager.FindByEmailAsync(confirmModel.Email);

            if (user.Status != null)
            {
                var result = await (_userManager as CognitoUserManager<CognitoUser>).ConfirmSignUpAsync(user,confirmModel.Code,true).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    return "Valid User";
                }
            }
            return "User does not Exists";


        }



        // POST api/values
        [HttpPost("signin")]
        public async Task<string> Signin(LoginModel loginModel)
        {

            var result = await _signInManager.PasswordSignInAsync(loginModel.Email,loginModel.Password,loginModel.isRemeber,false).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    return "Login Sucsess";
                }

            return "Login Failed";


        }

    }
}

