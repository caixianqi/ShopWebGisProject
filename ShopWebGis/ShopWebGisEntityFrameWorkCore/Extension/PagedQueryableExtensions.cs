/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  PagedQueryableExtensions

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/7/15 23:16:00

 *描述：IQueryExcention IQueryable拓展类

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopWebGisDomainShare.Extension
{
    public static class PagedQueryableExtensions
    {
        public static async Task<Page<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> entities,
            int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
            
        {
            if (pageIndex <= 0)
                throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

            var totalCount = await entities.CountAsync(cancellationToken);
            var items = await entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new Page<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }
    }
}
