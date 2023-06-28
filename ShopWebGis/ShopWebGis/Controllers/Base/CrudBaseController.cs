using IRepository;
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
    public abstract class CrudBaseController<TPrimaryKey, TEntity, EntityDto> : ControllerBase where TEntity : EntityBase<TPrimaryKey>
        where EntityDto : IEntityDto<TPrimaryKey>
    {
        public ICrudApplication<TPrimaryKey, TEntity, EntityDto> iCrudApplication => (ICrudApplication<TPrimaryKey, TEntity, EntityDto>)ServiceManager.ServiceProvider.GetService(typeof(ICrudApplication<TPrimaryKey, TEntity, EntityDto>));

        public IUnitOfWork unitOfWork { get; set; }



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
        public virtual async Task DisableAsync(TPrimaryKey id)
        {
            await iCrudApplication.DisableAsync(id);
        }

        [HttpPost(nameof(DisableManyAsync))]
        /// <summary>
        /// Id禁用多个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DisableManyAsync(params TPrimaryKey[] ids)
        {
            await iCrudApplication.DisableManyAsync(ids);
        }

        [HttpPost(nameof(CreateAsync))]
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public virtual async Task CreateAsync(EntityDto entityDto)
        {
            await iCrudApplication.CreateAsync(entityDto);
        }

        [HttpDelete(nameof(DeleteAsync))]
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            await iCrudApplication.DeleteAsync(id);
        }

        [HttpDelete(nameof(DeleteManyAsync))]
        /// <summary>
        /// 删除多个数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task DeleteManyAsync(params TPrimaryKey[] ids)
        {
            await iCrudApplication.DeleteManyAsync(ids);
        }
    }
}
