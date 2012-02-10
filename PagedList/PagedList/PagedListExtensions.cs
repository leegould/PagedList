using System.Collections.Generic;

namespace PagedList
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// Convert an IEnumerable source to a PagedList. Specifying page size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        /// <summary>
        /// Convert an IEnumerable source to a PagedList. With a default page size of 10.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index)
        {
            return new PagedList<T>(source, index, 10);
        }

        /// <summary>
        /// Convert an IEnumerable source to a PagedList. Forces the total and adds all the source items to the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize, int totalCount)
        {
            return new PagedList<T>(source, index, pageSize, totalCount);
        }
    }
}
