using System.Collections.Generic;
using System.Linq;

namespace TCRS.Shared.Helper
{
    public static class IEnumerableHandler
    {
        public static T UnpackIEnumerable<T>(IEnumerable<T> list)
        {
            return list.ToList().FirstOrDefault();
        }

        public static List<T> PackageInList<T>(T data)
        {
            var list = new List<T>();
            list.Add(data);
            return list;
        }
    }
}
