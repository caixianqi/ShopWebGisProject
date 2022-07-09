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
using IRepository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopWebGisApplicationContract.User;
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisCache.UserCache;
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
        private readonly IConfiguration _configuration;
        private readonly IUser _iuser;
        private readonly IUserCache _iuserCache;
        public UserApplication(IMapper mapper, IRepository<int, UserInfo> userRepository, IOptions<Jwt> jwtConfig, IConfiguration configuration, IUser iuser, IUserCache userCache)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtConfig = jwtConfig;
            _configuration = configuration;
            _iuser = iuser;
            _iuserCache = userCache;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassWord"></param>
        /// <returns></returns>
        public async Task<ComplexToken> ShopWebGisILogin(string userName, string userPassWord)
        {
            #region rsa解析出明文，再用明文MD5比较
            var rsaDecryption = RSAHelper.Decrypt(userPassWord);
            var md5Encryption = MD5Helper.Encrypt(rsaDecryption, _configuration["MD5Key"]);
            #endregion
            ComplexToken complexToken = new ComplexToken();
            var user = await _userRepository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new ShopWebGisCustomException($"{SystemConst.LoginFailed}用户不存在!");
            }
            if (user.isSoftDelete)
            {
                throw new ShopWebGisCustomException(SystemConst.UserHasBeenDisabled);
            }
            if (user.UserPassword != md5Encryption)
            {
                await _iuserCache.LimitLoginTimes(userName);
            }
            else
            {
                await _iuserCache.UserIsFreeze(userName);
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name,"JWT"),
                new Claim(ClaimAttributes.UserId,user.Id.ToString()),
                new Claim(ClaimAttributes.UserName,user.UserName),

            };
            // 添加角色
            var claimList = claims.ToList();
            foreach (var role in user.Roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }
            return CreateToken(claimList.ToArray());
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>

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
            var rsaDecryption = RSAHelper.Decrypt(userDto.UserPassword);
            var password = MD5Helper.Encrypt(rsaDecryption, _configuration["MD5Key"]);
            userDto.UserPassword = password;
            var userInfo = _mapper.Map<UserInfo>(userDto);
            await _userRepository.InsertAsync(userInfo);
            response = SystemConst.RegisterSuccess;
            return response;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IUser GetUserInfo()
        {
            return _iuser;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IList<UserDto>> GetUserList(string query)
        {
            IList<UserDto> list = new List<UserDto>();
            var data = await _userRepository.GetAvailableListAsync(x => string.IsNullOrWhiteSpace(query) ? true : x.UserName.Contains(query) || x.UserPhone.Contains(query));
            if (data.Any())
            {
                var users = _mapper.Map<IList<UserInfo>, IList<UserDto>>(data);
                list = users;
            }
            return list;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DeleteUser(int id)
        {
            return _userRepository.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public Task<UserInfo> UpdateUser(UserDto userDto)
        {
            return _userRepository.UpdateActionAsync(userDto.Id, x =>
            {
                x.UserPassword = userDto.UserPassword;
                x.UserName = userDto.UserName;
                x.UserPhone = userDto.UserPhone;
            });
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DisableUser(int id)
        {
            var count = _userRepository.SoftDeleteAsync(id);
            return count;
        }
    }
}
