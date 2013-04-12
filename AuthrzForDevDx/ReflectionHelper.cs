using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AuthrzForDevDx
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(Type t, object[] parms)
        {
            try
            {
                //string fullName = nameSpace + "." + className;                          //命名空间.类型名

                object ect = Activator.CreateInstance(t, parms);
                //object ect = Assembly.Load(assemblyName).CreateInstance(fullName);      //加载程序集, 创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
    }
}
