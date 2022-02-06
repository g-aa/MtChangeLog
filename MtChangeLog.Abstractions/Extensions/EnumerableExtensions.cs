using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Search<T>(this IEnumerable<T> enumerable, Guid guid) where T : IIdentifiable
        {
            var result = enumerable.FirstOrDefault(e => e.Id == guid);
            if (result is null)
            {
                throw new ArgumentException($"Сущность с ID = \"{guid}\" не найдена в БД");
            }
            return result;
        }
        public static T Search<T>(this IEnumerable<T> enumerable, T entity) where T : IEqualityPredicate<T>
        {
            var result = enumerable.FirstOrDefault(entity.GetEqualityPredicate());
            if (result is null)
            {
                throw new ArgumentException($"Сущность \"{entity}\" не найдена в БД");
            }
            return result;
        }

        public static T SearchOrDefault<T>(this IEnumerable<T> enumerable, Guid guid) where T : IIdentifiable, IDefaultable
        {
            var result = enumerable.FirstOrDefault(e => e.Id == guid);
            if (result is not null)
            {
                return result;
            }
            result = enumerable.FirstOrDefault(e => e.Default);
            if (result is null)
            {
                throw new ArgumentException($"Сущность с ID = \"{guid}\" или значение сущности по умолчанию не были найдены в БД");
            }
            return result;
        }
        public static T SearchOrDefault<T>(this IEnumerable<T> enumerable, T entity) where T : IDefaultable, IEqualityPredicate<T>
        {
            var result = enumerable.FirstOrDefault(entity.GetEqualityPredicate());
            if (result is not null)
            {
                return result;
            }
            result = enumerable.FirstOrDefault(e => e.Default);
            if (result is null)
            {
                throw new ArgumentException($"Сущность \"{entity}\" или значение сущности по умолчанию не были найдены в БД");
            }
            return result;
        }

        public static T SearchOrNull<T>(this IEnumerable<T> enumerable, Guid guid) where T : IIdentifiable
        {
            return enumerable.FirstOrDefault(e => e.Id == guid);
        }

        public static IEnumerable<T> SearchManyOrDefault<T>(this IEnumerable<T> enumerable, IEnumerable<Guid> guids) where T : IDefaultable, IIdentifiable
        {
            var result = enumerable.Where(e => guids.Contains(e.Id));
            if (result.Any())
            {
                return result;
            }
            result = enumerable.Where(e => e.Default);
            if (result is null)
            {
                throw new ArgumentException($"Не удалось найти запрашиваемые обьекты в БД по следующим ключам: \"{string.Join(", ", guids)}\"");
            }
            return result;
        }

        public static bool IsContained<T>(this IEnumerable<T> enumerable, T entity) where T : IEqualityPredicate<T>
        {
            return enumerable.FirstOrDefault(entity.GetEqualityPredicate()) != null;
        }
    }
}
