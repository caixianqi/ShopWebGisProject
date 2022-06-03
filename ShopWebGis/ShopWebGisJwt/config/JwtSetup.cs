/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisJwt.config

 *文件名：  JwtSetup

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/9 14:47:57

 *描述：

/************************************************************************************/

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopWebGisDomain.config;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisJwt.config
{
    public static class JwtSetup
    {
        public static void ShopWebGisJwtSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;

                    x.SaveToken = true;

                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateLifetime = true,
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                        {
                            bool t = DateTime.UtcNow < expires;
                            return t;
                        },

                        ValidateAudience = false,

                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:Issuer"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    };
                });
        }

    }
}
