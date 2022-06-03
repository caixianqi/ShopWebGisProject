using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopWebGis.Model
{
    public class ResponseDto<T>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T ResultData { get; set; }

        /// <summary>
        /// HTTP响应码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        ///  错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否调用成功
        /// </summary>
        public bool Success { get; set; }
    }
}
