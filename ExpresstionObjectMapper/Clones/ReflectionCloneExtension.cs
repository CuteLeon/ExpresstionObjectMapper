using System;
using System.Linq;

namespace ExpresstionObjectMapper.Clones
{
    /// <summary>
    /// 反射克隆扩展
    /// </summary>
    public static class ReflectionCloneExtension
    {
        public static TModel Clone<TModel>(TModel source)
        {
            Type modelType = typeof(TModel);
            TModel cloneInstance = Activator.CreateInstance<TModel>();

            // 拷贝属性
            foreach (var property in modelType
                .GetProperties()
                .Where(property => property.CanWrite))
            {
                property.SetValue(cloneInstance, property.GetValue(source, null), null);
            }

            return cloneInstance;
        }
    }
}
