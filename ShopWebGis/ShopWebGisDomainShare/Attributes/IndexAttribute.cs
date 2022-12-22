using System;

namespace ShopWebGisDomainShare.Attributes
{
    /// <summary>
    /// 索引数据标识特性
    /// 在不同orm进行实现扩展支持
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IndexAttribute : Attribute
    {
        /// <summary>
        /// 索引特性
        /// </summary>
        /// <param name="fields">索引字段, 以","分隔</param>
        public IndexAttribute(params string[] fields)
        {
            Fields = fields;
            Unique = false;
        }

        /// <summary>
        /// 索引特性
        /// </summary>
        /// <param name="name">索引名称</param>
        /// <param name="fields">索引字段数组</param>
        public IndexAttribute(string name, string[] fields)
        {
            Name = name;
            Fields = fields;
            Unique = false;
        }

        /// <summary>
        /// 索引特性
        /// </summary>
        /// <param name="name">索引名称</param>
        /// <param name="fields">索引字段数组</param>
        /// <param name="unique">是否唯一索引</param>
        public IndexAttribute(string name, string[] fields, bool unique)
        {
            Name = name;
            Unique = unique;
            Fields = fields;
        }

        ///// <summary>
        ///// 索引特性
        ///// </summary>
        ///// <param name="name">索引名称</param>
        ///// <param name="keys">索引字段, 以","分隔</param>
        ///// <param name="filter"></param>
        //public IndexAttribute(string name, string keys, string filter)
        //{
        //    Name = name;
        //    Filter = filter;
        //    Keys = keys;
        //}

        public string Name { get; private set; }

        public string[] Fields { get; private set; }

        public bool Unique { get; private set; }

        //public string Filter { get; set; }
    }
}
