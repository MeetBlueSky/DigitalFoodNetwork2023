﻿using DFN2023.Entities.Models;

using System;
using System.Linq;
using System.Linq.Expressions;

namespace DFN2023.Entities.Extention
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query,
            string orderByMember,
            DtOrderDir ascendingDirection)
        {
            var param = Expression.Parameter(typeof(T), "c");

            var body = orderByMember.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            var queryable = ascendingDirection == DtOrderDir.Asc ?
                (IOrderedQueryable<T>)Queryable.OrderBy(query.AsQueryable(), (dynamic)Expression.Lambda(body, param)) :
                (IOrderedQueryable<T>)Queryable.OrderByDescending(query.AsQueryable(), (dynamic)Expression.Lambda(body, param));

            return queryable;
        }

        public static IQueryable<T> WhereDynamic<T>(
            this IQueryable<T> sourceList, string query)
        {

            if (string.IsNullOrEmpty(query))
            {
                return sourceList;
            }

            try
            {

                var properties = typeof(T).GetProperties()
                    .Where(x => x.CanRead && x.CanWrite && !x.GetGetMethod().IsVirtual);

                //Expression
                sourceList = sourceList.Where(c =>
                    properties.Any(p => p.GetValue(c) != null && p.GetValue(c).ToString()
                        .Contains(query, StringComparison.InvariantCultureIgnoreCase)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return sourceList;
        }
    }
}
