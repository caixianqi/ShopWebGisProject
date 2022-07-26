/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisFreeSql.InterFace

 *文件名：  SubRuleResolver

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/5/20 17:35:23

 *描述：

/************************************************************************************/

using Microsoft.Extensions.Configuration;

using ShopWebGisDomainShare.CustomException;
using ShopWebGisFreeSql.AbstarctClass;
using ShopWebGisFreeSql.config;
using ShopWebGisFreeSql.Resolve;
using ShopWebGisFreeSql.SubRule;
using System;


namespace ShopWebGisFreeSql.InterFace
{
    public class SubRuleResolver : SubRuleAbstarctClass, ISubRuleResolver
    {
        private readonly IConfiguration _configuration;
        public SubRuleResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool GetReturn()
        {
            throw new NotImplementedException();
        }

        public SubRuleContext Resolve<T>() where T : class
        {
            
            var t = typeof(T);
            var fullName = $"{t.Namespace}.{t.Name}";
            var tableName = GetDefaultName<T>();
            var domainClassSection = _configuration.GetSection(fullName);// 获取实体命名空间以及类名
            var subRoute = domainClassSection.GetSection("SubRoute").Value; // 字段
            var subRuleValue = domainClassSection.GetSection("SubRuleType").Value;// 分表类型
            Type subRuleType = null;
            ISubRule iSubRule = null;
            if (!SubRuleTypeMapper.SubRuleTypeMapperDic.TryGetValue(subRuleValue, out subRuleType))// 获取Type
            {
                throw new ShopWebGisCustomException("分表类型不存在，无法确定分表规则!");
            }
            if (subRuleType == typeof(DateSubRule))
            {
                iSubRule = new DateSubRuleParse();
            }
            else
            {
            }
            SubRuleContext subRuleContext = new SubRuleContext(tableName, subRoute, subRuleType, iSubRule);

            return subRuleContext;
        }

        public SubRuleContext Resolve(Type type)
        {
            throw new NotImplementedException();
        }


    }
}
