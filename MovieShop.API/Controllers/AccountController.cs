using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using MovieShop.Core.Entities;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, IConfiguration config, ILogger<AccountController> logger)
        {
            _userService = userService;
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody]UserRegisterRequestModel user)
        {
            var createdUser = await _userService.CreateUser(user);
            _logger.LogInformation("User Registred", createdUser.Id);
            return Ok(createdUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequestModel loginU)
        {
            var loginUser = await _userService.ValidateUser(loginU.Email, loginU.Password);
            if(loginUser == null)
            {
                return Unauthorized("Please enter a valid email/password");
            }
            //once authenticated, generate token(JWT)

            return Ok(new { token = GenerateJWT(loginUser)});
        }

        // Method to create the Token (JWT) that takes User as input so that it can put user's Id, FirstName
        // LastName, Roles inside the payload of the Token
        // JWT has 3 parts
        // 1. Header part which wil have the Algorithm we use for generating the Token
        // 2. Payload - Information that we want inside our Token -
        // user's Id, FirstName LastName, Roles
        // 3.WE need to have a Secret to verify the Signature, make sure you don't use same secret for other applications
        // Store them securely

        private string GenerateJWT(User user)
        {
            var roles = new List<String>();
            foreach(var ur in user.UserRoles)
            {
                roles.Add(ur.Role.Name);
            }
            // claims are the one which will identity the user
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("role",JsonConvert.SerializeObject(roles))
                    //new Claim(JwtRegisteredClaimNames.,roles)
                    // we can store roles also
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            // read all the information from app.settings file to create the token
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSettings:PrivateKey"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_config.GetValue<double>("TokenSettings:ExpirationHours"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _config["TokenSettings:Issuer"],
                Audience = _config["TokenSettings:Audience"]
            };
            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(encodedJwt);

        }   
    }
}