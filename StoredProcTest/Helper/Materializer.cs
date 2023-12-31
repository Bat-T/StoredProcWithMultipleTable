﻿using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace StoredProcTest.Helper
{
    public static class Materializer
    {
        /// <summary>
        /// https://www.danylkoweb.com/Blog/aspnet-core-with-entity-framework-core-returning-multiple-resultsets-OL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <returns></returns>
        public static T Materialize<T>(IDataRecord record) where T : new()
        {
            var t = new T();
            foreach (var prop in typeof(T).GetProperties())
            {
                // 1). If entity reference, bypass it.
                if (prop.PropertyType.Namespace == typeof(T).Namespace)
                {
                    continue;
                }

                // 2). If collection, bypass it.
                if (prop.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    continue;
                }

                // 3). If property is NotMapped, bypass it.
                if (Attribute.IsDefined(prop, typeof(NotMappedAttribute)))
                {
                    continue;
                }

                // If the property doesn't map to an existing field, just continue.
                try
                {
                    record.GetOrdinal(prop.Name);
                }
                catch
                {
                    continue;
                }

                var dbValue = record[prop.Name];
                if (dbValue is DBNull) continue;

                if (prop.PropertyType.IsConstructedGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var baseType = prop.PropertyType.GetGenericArguments()[0];
                    var baseValue = Convert.ChangeType(dbValue, baseType);
                    var value = Activator.CreateInstance(prop.PropertyType, baseValue);
                    prop.SetValue(t, value);
                }
                else
                {
                    var value = Convert.ChangeType(dbValue, prop.PropertyType);
                    prop.SetValue(t, value);
                }
            }

            return t;
        }
    }

}
