﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpresstionObjectMapper.Clones
{
    /// <summary>
    /// 表达式树克隆扩展
    /// </summary>
    public static class ExpressionCloneExtension<TSource, TTarget>
    {
        /// <summary>
        /// 映射方法
        /// </summary>
        /// <remarks>会缓存在静态泛型类中</remarks>
        private static readonly Func<TSource, TTarget> MapFunc = CreateMapExpression();

        /// <summary>
        /// 克隆
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Clone(TSource source)
        {
            return MapFunc(source);
        }

        /// <summary>
        /// 创建映射表达式
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns></returns>
        public static Func<TSource, TTarget> CreateMapExpression()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "source");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var targetProperty in typeof(TTarget).GetProperties()
                .Where(p => p.CanWrite))
            {
                MemberExpression propertyExpression = Expression.Property(
                    parameterExpression,
                    typeof(TSource).GetProperty(targetProperty.Name));

                MemberBinding memberBinding = Expression.Bind(targetProperty, propertyExpression);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(
                Expression.New(typeof(TTarget)),
                memberBindingList);

            Expression<Func<TSource, TTarget>> mapExpression = Expression.Lambda<Func<TSource, TTarget>>(
                memberInitExpression,
                new ParameterExpression[] { parameterExpression });

            Func<TSource, TTarget> mapFunc = mapExpression.Compile();
            return mapFunc;
        }
    }
}
