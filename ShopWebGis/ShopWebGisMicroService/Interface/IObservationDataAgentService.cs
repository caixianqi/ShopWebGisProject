/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisMicroService.Interface

 *文件名：  IObservationDataAgentService

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人： 蔡显麒

 *创建时间：2022/9/29 14:35:58

 *描述：

/************************************************************************************/
using ShopWebGisDomainShare.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisMicroService.Interface
{
    [NacosClient(Name = "suncere-airbase")]
    public interface IObservationDataAgentService
    {
        [GetMapping(Route = "/api/AirBase/AqiStandard/GetAqiStandard")]
        Task<IList<AqiStandardDto>> GetAqiStandardAsync();
    }

    public class AqiStandardDto 
    {

        /// <summary>
        /// 评价
        /// </summary>
        public string Evaluate { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 罗马符号级别
        /// </summary>
        public string RomaLevel { get; set; }

        /// <summary>
        /// 表征颜色
        /// </summary>
        public string CharacterizationColor { get; set; }

        /// <summary>
        /// 颜色16进制编码
        /// </summary>
        public string ColorCode { get; set; }

        /// <summary>
        /// 标准下限表达式
        /// </summary>
        public string DownExpression { get; set; }

        /// <summary>
        /// 标准下限
        /// </summary>
        public string DownLimit { get; set; }

        /// <summary>
        /// 标准上限表达式
        /// </summary>
        public string UpExpression { get; set; }

        /// <summary>
        /// 标准上限
        /// </summary>
        public string UpLimit { get; set; }

        /// <summary>
        /// 健康影响
        /// </summary>
        public string HealthEffects { get; set; }

        /// <summary>
        /// 建议措施
        /// </summary>
        public string RecommendedMeasure { get; set; }
    }
}
