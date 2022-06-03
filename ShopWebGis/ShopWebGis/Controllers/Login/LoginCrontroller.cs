using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopWebGisApplicationContract.Login.Models;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.CustomException;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGis.Controllers.Login
{
    [ApiController]
    [Route("api/Login")]
    public class LoginCrontroller : ControllerBase
    {
        private readonly IOptions<Jwt> _jwtConfig;
        public LoginCrontroller(IOptions<Jwt> jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        [HttpGet]
        public string Login(string userName,string userPassWord)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name,"JWT"),
                new Claim("UserId","123"),
                new Claim("UserName","caixianqi")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _jwtConfig.Value.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.Value.Expires),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpPost]
        //public string Regisgter()
        //{

        //}
    }
}
