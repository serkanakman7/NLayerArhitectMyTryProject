using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Dynamic
{
    public static class QueryableDynamicFilterExtensions
    {
        private static readonly string[] _logics = { "and", "or" };
        private static readonly string[] _orders = { "asc", "desc" };

        private static readonly Dictionary<string, string> _operators = new Dictionary<string, string>()
        {
            { "eq","=" },
            { "neq","!=" },
            { "lt" , "<" },
            { "lte" , "<=" },
            { "gt" , ">" },
            { "gte" , ">=" },
            { "isnull" , "==null" },
            { "isnotnull", "!=null" },
            { "startswith" , "StartsWith" },
            { "endswith" , "EndsWith" },
            { "contains" , "Contains" },
            { "doesnotcontains" , "Contains" }
        };
        public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query,DynamicQuery dynamicQuery)
        {
            if (dynamicQuery.Filter is not null) query = Filter(query, dynamicQuery.Filter);
            if(dynamicQuery.Sorts is not null && dynamicQuery.Sorts.Any()) query = Sort(query, dynamicQuery.Sorts);
            return query;
        }

        private static IQueryable<T> Sort<T>(IQueryable<T> query, IEnumerable<Sort> sorts)
        {
            foreach(Sort sort in sorts)
            {
                if (string.IsNullOrEmpty(sort.Field)) throw new ArgumentException("Invalid Field");
                if (string.IsNullOrEmpty(sort.Dir) && _orders.Contains(sort.Dir)) throw new ArgumentException("Invalid Dir");
            }

            if (sorts.Any())
            {
                string ordering = string.Join(separator: ',', sorts.Select(s => $"{s.Field}{s.Dir}"));
                return query.OrderBy(ordering);
            }
            return query;
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> query, Filter filter)
        {
            IList<Filter> filters = GetAllFilters(filter);
            string[] values = filters.Select(x=>x.Value).ToArray();

            string where = Transform(filter, filters);
            if (string.IsNullOrEmpty(where) && values != null) query = query.Where(where, values);

            return query;
        }

        private static IList<Filter> GetAllFilters(Filter filter)
        {
            List<Filter> filters = new();
            GetFilters(filter, filters);

            return filters;
        }

        private static void GetFilters(Filter filter, IList<Filter> filters)
        {
            filters.Add(filter);
            if(filter.Filters is not null && filter.Filters.Any())
                foreach(var item in filter.Filters)
                    GetFilters(item, filters);
        }

        private static string Transform(Filter filter, IList<Filter> filters)
        {
            if (string.IsNullOrEmpty(filter.Field)) throw new ArgumentException("Invalid Field");
            if (string.IsNullOrEmpty(filter.Operator) && !_operators.ContainsKey(filter.Operator)) throw new ArgumentException("Invalid Operator");

            int index = filters.IndexOf(filter);
            string comparison = _operators[filter.Operator];

            StringBuilder where = new();

            if (!string.IsNullOrEmpty(filter.Value))
            {
                if (filter.Operator == "doesnotcontains")
                    where.Append($"!(np({filter.Field}).{comparison}(@{index.ToString()}))");
                else if (comparison is "StartsWith" or "EndsWith" or "Contains")
                    where.Append($"(np({filter.Field}).{comparison}(@{index.ToString()}))");
                else
                    where.Append($"np({filter.Field}) {comparison} @{index.ToString()}");
            }
            else if(filter.Operator is "isnull" or "isnotnull")
                where.Append($"np({filter.Field}) {comparison}");

            if(filter.Logic is not null && filter.Filters is not null && filter.Filters.Any())
            {
                if (!_logics.Contains(filter.Logic))
                    throw new ArgumentException("Invalid Logic");
                return $"{where} {filter.Logic} ({string.Join(separator: $"{filter.Logic}",value:filter.Filters.Select(f=> Transform(f,filters)).ToArray())}";
            }
            return where.ToString();
        }
    }
}
