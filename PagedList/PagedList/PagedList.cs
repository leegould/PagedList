using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagedList
{
    public class PagedList<T> : List<T>, IPagedList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// Base constructor which sets the two params and clears all items in the list.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagedList(int index, int pageSize)
        {
            PageIndex = index;
            PageSize = pageSize;
            Clear();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// Passes an IEnumerable source, which is used for lazy evaluating size and what items
        /// to add. Use this constructor when you already have all the items/is a linq/EF collection.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagedList(IEnumerable<T> source, int index, int pageSize)
            : this(index, pageSize)
        {
            TotalCount = source.Count();
            AddRange(source.Skip((PageIndex > 0 ? PageIndex - 1 : 0) * pageSize).Take(pageSize).ToList());
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// Constructor that accepts IQueryable version of source. More for use with Linq/EF entities.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagedList(IQueryable<T> source, int index, int pageSize)
            : this(index, pageSize)
        {
            TotalCount = source.Count();
            AddRange(source.Skip((PageIndex > 0 ? PageIndex - 1 : 0) * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList&lt;T&gt;"/> class.
        /// This constructor forces the total count to be set and adds everything in the source parameter.
        /// Used for crowbaring a partial set (i.e. already paged List of items from sproc) into this item :)
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        public PagedList(IEnumerable<T> source, int index, int pageSize, int totalCount)
            : this(index, pageSize)
        {
            TotalCount = totalCount;
            AddRange(source);
        }

        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return ((PageIndex + 1) * PageSize) < TotalCount;
            }
        }

        /// <summary>
        /// Builds the paging links.
        /// </summary>
        /// <param name="shownEitherSide">The number of pages to show.</param>
        /// <param name="url">The base URL.</param>
        /// <returns></returns>
        public string BuildPagingLinks(int shownEitherSide, string url)
        {
            StringBuilder sbPageList = new StringBuilder();

            int numberOfPages = (int)Math.Ceiling((double)TotalCount / PageSize);

            Dictionary<string, int> pagingEntries = new Dictionary<string, int>();
            if (numberOfPages > 1)
            {
                pagingEntries.Add("First", 1);
                pagingEntries.Add("Prev", (PageIndex - 1) < 1 ? 1 : (PageIndex - 1));
            }

            int firstNum = (PageIndex - shownEitherSide) < 1 ? 1 : (PageIndex - shownEitherSide);
            int lastNum = (PageIndex + shownEitherSide) > numberOfPages ? numberOfPages : (PageIndex + shownEitherSide);

            if (firstNum > 1) { pagingEntries.Add("... ", firstNum - 1); }

            for (int i = firstNum; i <= lastNum; i++)
            {
                pagingEntries.Add(i.ToString(), i);
            }

            if (lastNum < numberOfPages) { pagingEntries.Add("...", lastNum + 1); }
            if (numberOfPages > 1)
            {
                pagingEntries.Add("Next", (PageIndex + 1) > numberOfPages ? numberOfPages : (PageIndex + 1));
                pagingEntries.Add("Last", numberOfPages);
            }

            sbPageList.Append("<div class=\"pagination\"><span class=\"page\"></span><ul>");
            foreach (KeyValuePair<string, int> pageEntry in pagingEntries)
            {
                sbPageList.Append(String.Format("<li{0}><a href=\"{1}\">{2}</a></li>",
                                    pageEntry.Key == PageIndex.ToString() ? " class=\"current\"" : string.Empty,
                                    url + (url.IndexOf("?", StringComparison.Ordinal) != -1 ? "&" : "?") + "page=" + pageEntry.Value.ToString(), pageEntry.Key));
            }
            sbPageList.Append("</ul></div>");

            return sbPageList.ToString();
        }
    }
}
