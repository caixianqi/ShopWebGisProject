/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisApplication.Base

 *文件名：  CrudApplication

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/26 10:34:06

 *描述：抽象应用基础增删改查类

/************************************************************************************/

using AutoMapper;
using IRepository;
using IRepository.Base;
using Repository.Base;
using ShopWebGisApplicationContract.Base;
using ShopWebGisDomain.Base;
using ShopWebGisDomainShare.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisApplication.Base
{
    public class CrudApplication<TPrimaryKey, Entity, EntityDto> : ICrudApplication<TPrimaryKey, Entity, EntityDto> where Entity : EntityBase<TPrimaryKey>
        where EntityDto : IEntityDto<TPrimaryKey>
    {
        private readonly IRepository<TPrimaryKey, Entity> _repository;
        private readonly IMapper _mapper;
        public CrudApplication(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _repository = iUnitOfWork.Repositorys<TPrimaryKey, Entity>();
            _mapper = mapper;
        }
        public virtual async Task<int> CreateAsync(EntityDto entityDto)
        {
            var menu = _mapper.Map<EntityDto, Entity>(entityDto);
            return await _repository.InsertAsync(menu);
        }

        public virtual async Task<int> UpdateAsync(Entity entity)
        {
            return await _repository.UpdateAsync(entity);            
        }

        public virtual async Task<int> DisableAsync(TPrimaryKey id)
        {
            return await _repository.SoftDeleteAsync(id);
        }

        public virtual async Task<int> DeleteAsync(TPrimaryKey id)
        {
            return await _repository.SoftDeleteAsync(id);
        }

        public virtual async Task<int> DisableManyAsync(params TPrimaryKey[] ids)
        {
            return await _repository.SoftDeleteManyAsync(ids);
        }

        public virtual async Task<int> DeleteManyAsync(params TPrimaryKey[] ids)
        {
            return await _repository.DeleteManyAsync(ids);
        }

        public virtual async Task<EntityDto> GetAsync(TPrimaryKey id)
        {
            var entity = await _repository.FindAsync(id);
            return _mapper.Map<Entity, EntityDto>(entity);
        }

        public virtual async Task<Page<EntityDto>> GetPageListAsync(int pageIndex, int pageSize)
        {
            var datas = await _repository.GetAvailablePageListAsync(pageIndex, pageSize);
            return _mapper.Map<Page<Entity>, Page<EntityDto>>(datas);
        }
    }
}
