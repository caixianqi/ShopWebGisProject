using Microsoft.AspNetCore.Mvc;
using ShopWebGisApplicationContract.Base;
using ShopWebGisDomain.Base;
using ShopWebGisDomainShare.Common;
using ShopWebGisIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebGis.HttApi.Host.Controllers
{
    [Route("api/[Controller]")]
    public abstract class CrudBaseController<TPrimaryKey, Entity, EntityDto> : ControllerBase where Entity : EntityBase<TPrimaryKey>
        where EntityDto : IEntityDto<TPrimaryKey>
    {
        public ICrudApplication<TPrimaryKey, Entity, EntityDto> iCrudApplication => (ICrudApplication<TPrimaryKey, Entity, EntityDto>)ServiceManager.ServiceProvider.GetService(typeof(ICrudApplication<TPrimaryKey, Entity, EntityDto>));

        [HttpGet(nameof(GetPageListAsync))]
        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Page<EntityDto>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return await iCrudApplication.GetPageListAsync(pageIndex, pageSize);
        }

        [HttpGet(nameof(GetAsync))]
        /// <summary>
        /// Id获取实体数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<EntityDto> GetAsync(TPrimaryKey id)
        {
            return await iCrudApplication.GetAsync(id);
        }

        [HttpDelete(nameof(DisableAsync))]
        /// <summary>
        /// Id禁用实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> DisableAsync(TPrimaryKey id)
        {
            return await iCrudApplication.DisableAsync(id);
        }

        [HttpPost(nameof(DisableManyAsync))]
        /// <summary>
        /// Id禁用多个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> DisableManyAsync(params TPrimaryKey[] ids)
        {
            return await iCrudApplication.DisableManyAsync(ids);
        }

        [HttpPost(nameof(CreateAsync))]
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public virtual async Task<int> CreateAsync(EntityDto entityDto)
        {
            return await iCrudApplication.CreateAsync(entityDto);
        }

        [HttpDelete(nameof(DeleteAsync))]
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(TPrimaryKey id)
        {
            return await iCrudApplication.DeleteAsync(id);
        }

        [HttpDelete(nameof(DeleteManyAsync))]
        /// <summary>
        /// 删除多个数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteManyAsync(params TPrimaryKey[] ids)
        {
            return await iCrudApplication.DeleteManyAsync(ids);
        }
    }
}
