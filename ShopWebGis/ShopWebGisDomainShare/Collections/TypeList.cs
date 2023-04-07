/************************************************************************************

 * Copyright (c) 2023 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebCore.Collections

 *文件名：  TypeList

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2023/4/5 14:05:38

 *描述：

/************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ShopWebGisDomainShare.Collections
{
    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        private readonly List<Type> _typeList;

        public TypeList()
        {
            _typeList = new List<Type>();
        }

        public Type this[int index]
        {
            get => _typeList[index];
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        public int Count => _typeList.Count;

        public bool IsReadOnly => false;

        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }

        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        public void Clear()
        {
            _typeList.Clear();
        }

        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }

        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Type> GetEnumerator()
        {
           return _typeList.GetEnumerator();
        }

        public int IndexOf(Type item)
        {
            return _typeList.IndexOf(item);
        }

        public void Insert(int index, Type item)
        {
            _typeList.Insert(index, item);
        }

        public void Remove<T>() where T : TBaseType
        {
            Remove(typeof(T));
        }

        public bool Remove(Type item)
        {
           return _typeList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _typeList.RemoveAt(index);
        }

        public bool TryAdd<T>() where T : TBaseType
        {
            if (Contains<T>())
            {
                return false;
            }
            Add<T>();
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        /// <summary>
        /// 判断是否可分配
        /// </summary>
        /// <param name="item"></param>
        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
            {
                throw new ArgumentException($"Given type ({item.AssemblyQualifiedName}) should be instance of {typeof(TBaseType).AssemblyQualifiedName} ", nameof(item));
            }
        }
    }
}
