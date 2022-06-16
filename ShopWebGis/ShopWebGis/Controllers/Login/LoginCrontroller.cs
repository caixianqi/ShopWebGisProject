using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopWebGisApplicationContract.Login;
using ShopWebGisApplicationContract.User;
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.CustomException;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        private readonly IUserApplication _loginApplication;
        public LoginCrontroller(IUserApplication loginApplication)
        {
            _loginApplication = loginApplication;
        }

        /// <summary>
        /// 登录API
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassWord"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ComplexToken> Login(string userName, string userPassWord)
        {
            return await _loginApplication.ShopWebGisILogin(userName, userPassWord);
        }

        /// <summary>
        /// 注册API
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Regisgter")]
        public async Task<string> Regisgter([FromBody] UserDto user)
        {
            return await _loginApplication.ShopWebGisRegister(user);
        }

        [HttpGet("RefreshToken")]
        [Authorize]
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ComplexToken RefreshToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            return _loginApplication.RefreshToken(token.Replace("Bearer ",""));
        }
    }
}
