using System.Collections.Generic;

namespace Model.Util
{
    public static class ListExtensions
    {
        private static readonly System.Random _random = new System.Random();

        public static T Random<T>(this IList<T> list)
        {
            return list[_random.Next(list.Count)];
        }
    }
}