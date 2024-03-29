﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopWebGisApplicationContract.User;
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomain.config;
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.Const;
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
    [Route("api/User")]
    public class LoginController : ControllerBase
    {

        private readonly IUserApplication _loginApplication;
        public LoginController(IUserApplication loginApplication)
        {
            _loginApplication = loginApplication;
        }

        /// <summary>
        /// 注册API
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost(nameof(Regisgter))]
        public async Task<string> Regisgter(UserDto user)
        {
            return await _loginApplication.ShopWebGisRegister(user);
        }

        /// <summary>
        /// 登录API
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassWord"></param>
        /// <param name="grant_type">授权方式</param>
        /// <returns></returns>
        [HttpPost(nameof(Login))]
        public async Task<ComplexToken> Login([FromForm] string userName, [FromForm] string userPassWord)
        {
            return await _loginApplication.ShopWebGisILogin(userName, userPassWord);
        }

        [HttpGet(nameof(RefreshToken))]
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ComplexToken RefreshToken(string refreshToken)
        {
            //var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            return _loginApplication.RefreshToken(refreshToken);
        }

        /// <summary>
        /// 获取PublicKey
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetCryptoPublicKey))]
        public string GetCryptoPublicKey()
        {
            return RSAHelper.publicKey;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet(nameof(GetUserInfo))]
        [Authorize]
        public IUser GetUserInfo()
        {
            return _loginApplication.GetUserInfo();
        }

        [HttpGet(nameof(GetUserList))]
        [Authorize]
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserDto>> GetUserList(string query)
        {
            return await _loginApplication.GetUserList(query);
        }

        [HttpPut(nameof(ModifyUser))]
        [Authorize]
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserDto> ModifyUser(UserUpdateDto user)
        {
            return await _loginApplication.UpdateUser(user);
        }

    }
}
