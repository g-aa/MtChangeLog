using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Extensions
{
    public static class QueryableExtensions
    {
        public static T Search<T>(this IQueryable<T> queryable, Guid guid) where T : IIdentifiable
        {
            var result = queryable.FirstOrDefault(e => e.Id == guid);
            if (result is null)
            {
                throw new ArgumentException($"Сущность с ID = \"{guid}\" не найдена в БД");
            }
            return result;
        }
        public static T Search<T>(this IQueryable<T> queryable, T entity) where T : IEqualityPredicate<T>
        {
            var result = queryable.FirstOrDefault(entity.GetEqualityPredicate());
            if (result is null)
            {
                throw new ArgumentException($"Сущность \"{entity}\" не найдена в БД");
            }
            return result;
        }

        public static T SearchOrDefault<T>(this IQueryable<T> queryable, Guid guid) where T : IIdentifiable, IDefaultable
        {
            var result = queryable.FirstOrDefault(e => e.Id == guid);
            if (result is not null)
            {
                return result;
            }
            result = queryable.FirstOrDefault(e => e.Default);
            if (result is null)
            {
                throw new ArgumentException($"Сущность с ID = \"{guid}\" или значение сущности по умолчанию не были найдены в БД");
            }
            return result;
        }
        public static T SearchOrDefault<T>(this IQueryable<T> queryable, T entity) where T : IDefaultable, IEqualityPredicate<T>
        {
            var result = queryable.FirstOrDefault(entity.GetEqualityPredicate());
            if (result is not null)
            {
                return result;
            }
            result = queryable.FirstOrDefault(e => e.Default);
            if (result is null)
            {
                throw new ArgumentException($"Сущность \"{entity}\" или значение сущности по умолчанию не были найдены в БД");
            }
            return result;
        }

        public static T SearchOrNull<T>(this IQueryable<T> queryable, Guid guid) where T : IIdentifiable
        {
            return queryable.FirstOrDefault(e => e.Id == guid);
        }

        public static IQueryable<T> SearchManyOrDefault<T>(this IQueryable<T> queryable, IEnumerable<Guid> guids) where T : IDefaultable, IIdentifiable
        {
            var result = queryable.Where(e => guids.Contains(e.Id));
            if (result.Any())
            {
                return result;
            }
            result = queryable.Where(e => e.Default);
            if (result is null)
            {
                throw new ArgumentException($"Не удалось найти запрашиваемые обьекты в БД по следующим ключам: \"{string.Join(", ", guids)}\"");
            }
            return result;
        }
                
        public static bool IsContained<T>(this IQueryable<T> queryable, T entity) where T : IEqualityPredicate<T>
        {
            return queryable.FirstOrDefault(entity.GetEqualityPredicate()) != null;
        }
    }
}
