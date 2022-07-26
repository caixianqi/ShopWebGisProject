/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Login

 *文件名：  ILogin

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/5/9 15:52:26

 *描述：用户--登录，注册等接口

/************************************************************************************/
using ShopWebGisApplicationContract.User.Models;
using ShopWebGisDomain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplicationContract.User
{
    public interface IUserApplication
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task<ComplexToken> ShopWebGisILogin(string userName, string userPassWord);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task<string> ShopWebGisRegister(UserDto userDto);

        /// <summary>
        /// Token刷新
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ComplexToken RefreshToken(string token);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        IUser GetUserInfo();

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<IList<UserDto>> GetUserList(string query);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        Task<int> DeleteUser(int id);

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DisableUser(int id);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        Task<UserDto> UpdateUser(UserUpdateDto userDto);


    }
}
