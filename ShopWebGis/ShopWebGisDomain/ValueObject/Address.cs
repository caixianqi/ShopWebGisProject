/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomain.ValueObject

 *文件名：  Address

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/5 22:06:33

 *描述：地址

/************************************************************************************/

using Microsoft.EntityFrameworkCore;
using ShopWebGisDomain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopWebGisDomain.ValueObject
{
    [Owned]
    public class Address : IValueObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="province">省</param>
        /// <param name="city">市</param>
        /// <param name="county">镇</param>
        /// <param name="addressDetails">详细地址</param>
        public Address(string province, string city, string county, string addressDetail)
        {
            Province = province;
            City = city;
            County = county;
            AddressDetail = addressDetail;
        }
        /// <summary>
        /// 无参构造
        /// </summary>
        public Address()
        {

        }

        #region 地址属性

        /// <summary>
        /// 省
        /// </summary>
        [StringLength(25)]
        [Column("province")]
        [Comment("省份")]
        public string Province { get; set; }

        /// <summary>
        /// 市县
        /// </summary>
        [StringLength(25)]
        [Column("city")]
        [Comment("城市")]
        public string City { get; set; }

        /// <summary>
        /// 镇
        /// </summary>
        [StringLength(25)]
        [Column("county")]
        [Comment("区县镇")]
        public string County { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [StringLength(200)]
        [Column("addressdetail")]
        [Comment("详细地址")]
        public string AddressDetail { get; set; }
        #endregion

        /// <summary>
        /// 重写tostring 方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = new string[] { Province, City, County, AddressDetail };

            return string.Join(",", str);
        }
    }
}
