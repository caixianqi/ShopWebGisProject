using System;

namespace ShopWebGisDomainShare.Attributes
{
    /// <summary>
    /// 自定义小数精度数据特性
    /// 在不同orm进行实现扩展支持
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DecimalPrecisionAttribute : Attribute
    {
        /// <summary>
        /// 自定义Decimal类型的精确度属性
        /// </summary>
        /// <param name="precision">精度, decimal数值的总长度(默认18)</param>
        /// <param name="scale">小数位数(默认2)</param>
        public DecimalPrecisionAttribute(byte precision = 18, byte scale = 2)
        {
            Precision = precision;
            Scale = scale;
        }

        /// <summary>
        /// 精度，精度, decimal数值的总长度(默认18)
        /// </summary>
        public int Precision { get; private set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public int Scale { get; private set; }
    }
}
