/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.Login

 *文件名：  LoginApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/9 15:58:22

 *描述：登录应用

/************************************************************************************/

using AutoMapper;
using IRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopWebGisApplicationContract.User;
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomain.config;
using ShopWebGisDomain.User;
using ShopWebGisDomainShare.Common;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.CustomException;
using ShopWebGisFreeSql.InterFace;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplication.User
{
    public class UserApplication : IUserApplication
    {
        private readonly IMapper _mapper;
        private readonly IRepository<int, UserInfo> _userRepository;
        private readonly IOptions<Jwt> _jwtConfig;
        public UserApplication(IMapper mapper, IRepository<int, UserInfo> userRepository, IOptions<Jwt> jwtConfig)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtConfig = jwtConfig;
        }



        public async Task<ComplexToken> ShopWebGisILogin(string userName, string userPassWord)
        {
            ComplexToken complexToken = new ComplexToken();
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == userName && x.UserPassword == userPassWord);
            if (user == null)
            {
                throw new ShopWebGisCustomException($"{SystemConst.LoginFailed}用户不存在或者密码错误!");
            }
            var claims = new[] {
                new Claim(ClaimTypes.Name,"JWT"),
                new Claim(ClaimAttributes.UserId,user.Id.ToString()),
                new Claim(ClaimAttributes.UserName,user.UserName)
            };
            return CreateToken(claims);
        }

        public async Task<string> ShopWebGisRegister(UserDto userDto)
        {
            #region 模型认证
            var response = SystemConst.RegisterFailed;
            UserDtoValidator userValidator = new UserDtoValidator();
            var validateResult = userValidator.Validate(userDto);
            if (!validateResult.IsValid)
            {
                throw new ShopWebGisCustomException(response + validateResult.ToString());
            }
            #endregion
            // 判断是否存在相同的用户名
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == userDto.UserName);
            if (user != null)
            {
                throw new ShopWebGisCustomException(response + "用户已存在,请修改用户名,重新注册!");
            }
            var userInfo = _mapper.Map<UserInfo>(user);
            await _userRepository.InsertAsync(userInfo);
            response = SystemConst.RegisterSuccess;
            return response;
        }


        public ComplexToken RefreshToken(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            bool isCan = jwtSecurityTokenHandler.CanReadToken(token);//验证Token格式
            if (!isCan)
            {
                throw new ShopWebGisCustomException("传入访问令牌格式错误!");
            }
            var payLoad = jwtSecurityTokenHandler.ReadJwtToken(token).Payload;
            var claims = payLoad.Claims.ToArray();
            return CreateToken(claims);
        }

        /// <summary>
        /// 创建双Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private ComplexToken CreateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
               issuer: _jwtConfig.Value.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.Value.AccessTokenExpires),
                signingCredentials: credentials);

            var refreshToken = new JwtSecurityToken(
               issuer: _jwtConfig.Value.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtConfig.Value.RefreshTokenExpires),
                signingCredentials: credentials);
            ComplexToken complexToken = new ComplexToken();
            complexToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
            complexToken.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            return complexToken;
        }
    }
}