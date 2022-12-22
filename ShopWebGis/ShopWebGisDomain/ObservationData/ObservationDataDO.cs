/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.ObservationData

 *文件名：  ObservationDataDO

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/12/22 13:24:42

 *描述：

/************************************************************************************/

using NETCore.Encrypt;
using ShopWebGisDomainShare.Attributes;
using ShopWebGisDomainShare.Const;
using ShopWebGisDomainShare.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.ObservationData
{
    /// <summary>
    /// 大气观测数据
    /// </summary>
    [Table("data")]
    [Index(nameof(Code), nameof(TimePoint))]
    [Index(nameof(Code), nameof(TimePoint), nameof(PollutantCode))]
    public class ObservationDataDO
    {


        public ObservationDataDO()
        {
        }

        public ObservationDataDO(string code, DateTime timePoint, string pollutantCode)
        {
            Code = code;
            TimePoint = timePoint;
            PollutantCode = pollutantCode;
            Id = EncryptProvider.Sha1($"{Code}_{timePoint.ToTimeStamp()}_{PollutantCode}");
        }

        /// <summary>
        /// 数据唯一ID
        /// </summary>
        [Key]
        [Required]
        [Column("id")]
        [StringLength(50)]
        [Description("数据唯一ID, 根据站点编码、监测时间、监测项目编码hash加密得到")]
        public string Id { get; set; }

        /// <summary>
        /// 点位、城市、区域编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("code")]
        [Description("点位编码")]
        public string Code { get; set; }

        /// <summary>
        /// 数据采集时间
        /// </summary>
        [Required]
        [Column("time_point")]
        [Description("数据采集时间")]
        public DateTime TimePoint { get; set; }

        /// <summary>
        /// 监测项目编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column("pollutant_code")]
        [Description("监测项目编码")]
        public string PollutantCode { get; set; }

        /// <summary>
        /// 监测值
        /// </summary>
        [Required]
        [Column("mon_value")]
        [DecimalPrecision(12, 5)]
        [Description("监测值")]
        public decimal MonValue { get; set; } = SystemConst.InvaliValue;

        /// <summary>
        /// 数据标记
        /// </summary>
        [Required]
        [Column("mark")]
        [StringLength(20)]
        [DefaultValue("")]
        [Description("数据标记")]
        public string Mark { get; set; } = string.Empty;

    }
}
