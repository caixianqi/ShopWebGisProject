/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplicationContract.Base

 *文件名：  ICrudApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/7/26 10:25:23

 *描述：提供基本增删改查接口

/************************************************************************************/
using ShopWebGisDomain.Base;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplicationContract.Base
{
    public interface ICrudApplication<TPrimaryKey, Entity, EntityDto> where Entity : EntityBase<TPrimaryKey>
        where EntityDto : IEntityDto<TPrimaryKey>
    {
        Task<Page<EntityDto>> GetPageListAsync(int pageIndex, int pageSize);

        Task<EntityDto> GetAsync(TPrimaryKey id);

        Task<int> DisableAsync(TPrimaryKey id);

        Task<int> CreateAsync(EntityDto entityDto);

        Task<int> UpdateAsync(Entity entityDto);

        Task<int> DeleteAsync(TPrimaryKey id);

        Task<int> DisableManyAsync(params TPrimaryKey[] ids);

        Task<int> DeleteManyAsync(params TPrimaryKey[] ids);
    }
}
