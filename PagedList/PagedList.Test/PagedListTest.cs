using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PagedList;

namespace MallowStreet.Common.Tests.Paging
{
    [TestFixture]
    public class PagedListTests
    {
        [Test]
        public void PagedList_BasicConstructor_Valid()
        {
            const int page = 1;
            const int pageSize = 1;

            var pagedList = new PagedList<string>(page, pageSize);
            Assert.IsNotNull(pagedList);
            Assert.AreEqual(page, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }

        [Test]
        public void PagedList_EnumerableConstructor_ZeroPage_Valid()
        {
            const int page = 0;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            Assert.IsNotNull(pagedList);
            Assert.AreEqual(source.Count, pagedList.TotalCount);
            Assert.AreEqual(pageSize, pagedList.Count);
            Assert.AreEqual(page, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }

        [Test]
        public void PagedList_EnumerableConstructor_Valid()
        {
            const int page = 1;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            Assert.IsNotNull(pagedList);
            Assert.AreEqual(source.Count, pagedList.TotalCount);
            Assert.AreEqual(pageSize, pagedList.Count);
            Assert.AreEqual(page, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }

        [Test]
        public void PagedList_QueryableConstructor_ZeroPage_Valid()
        {
            const int page = 0;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsQueryable(), page, pageSize);

            Assert.IsNotNull(pagedList);
            Assert.AreEqual(source.Count, pagedList.TotalCount);
            Assert.AreEqual(pageSize, pagedList.Count);
            Assert.AreEqual(page, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }

        [Test]
        public void PagedList_QueryableConstructor_Valid()
        {
            const int page = 1;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsQueryable(), page, pageSize);

            Assert.IsNotNull(pagedList);
            Assert.AreEqual(source.Count, pagedList.TotalCount);
            Assert.AreEqual(pageSize, pagedList.Count);
            Assert.AreEqual(page, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }

        [Test]
        public void PagedList_ForcedTotalEnumerableConstructor_Valid()
        {
            const int page = 1;
            const int pageSize = 1;
            const int totalSize = 10;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize, totalSize);

            Assert.IsNotNull(pagedList);
            Assert.AreEqual(totalSize, pagedList.TotalCount);
            Assert.AreEqual(source.Count, pagedList.Count); // Because it adds all items in the source.
            Assert.AreEqual(page, pagedList.PageIndex);
            Assert.AreEqual(pageSize, pagedList.PageSize);
        }

        [Test]
        public void PagedList_HasPreviousPage_False_Valid()
        {
            const int page = 0;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            Assert.IsFalse(pagedList.HasPreviousPage);
        }

        [Test]
        public void PagedList_HasPreviousPage_True_Valid()
        {
            const int page = 1;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            Assert.IsTrue(pagedList.HasPreviousPage);
        }

        [Test]
        public void PagedList_HasNextPage_False_Valid()
        {
            const int page = 0;
            const int pageSize = 3;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            Assert.IsFalse(pagedList.HasNextPage);
        }

        [Test]
        public void PagedList_HasNextPage_True_Valid()
        {
            const int page = 1;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            Assert.IsTrue(pagedList.HasNextPage);
        }

        [Test]
        public void PagedList_BuildPagingLinks_Valid()
        {
            const int page = 1;
            const int pageSize = 1;
            const int pagesToShow = 5;
            const string url = "url";

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            var actual = pagedList.BuildPagingLinks(pagesToShow, url);

            const string expected = "<div class=\"pagination\">" +
                                        "<span class=\"page\"></span>" +
                                        "<ul>" +
                                            "<li><a href=\"url?page=1\">First</a></li>" +
                                            "<li><a href=\"url?page=1\">Prev</a></li>" +
                                            "<li class=\"current\"><a href=\"url?page=1\">1</a></li>" +
                                            "<li><a href=\"url?page=2\">2</a></li>" +
                                            "<li><a href=\"url?page=3\">3</a></li>" +
                                            "<li><a href=\"url?page=2\">Next</a></li>" +
                                            "<li><a href=\"url?page=3\">Last</a></li>" +
                                        "</ul>" +
                                    "</div>";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PagedList_BuildPagingFirstPageWithElipses_Valid()
        {
            const int page = 1;
            const int pageSize = 1;
            const int shownEitherSide = 1;
            const string url = "url";

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            var actual = pagedList.BuildPagingLinks(shownEitherSide, url);

            const string expected = "<div class=\"pagination\">" +
                                        "<span class=\"page\"></span>" +
                                        "<ul>" +
                                            "<li><a href=\"url?page=1\">First</a></li>" +
                                            "<li><a href=\"url?page=1\">Prev</a></li>" +
                                            "<li class=\"current\"><a href=\"url?page=1\">1</a></li>" +
                                            "<li><a href=\"url?page=2\">2</a></li>" +
                                            "<li><a href=\"url?page=3\">...</a></li>" +
                                            "<li><a href=\"url?page=2\">Next</a></li>" +
                                            "<li><a href=\"url?page=3\">Last</a></li>" +
                                        "</ul>" +
                                    "</div>";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PagedList_BuildPagingSecondPage_Valid()
        {
            const int page = 2;
            const int pageSize = 1;
            const int shownEitherSide = 1;
            const string url = "url";

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            var actual = pagedList.BuildPagingLinks(shownEitherSide, url);

            const string expected = "<div class=\"pagination\">" +
                                        "<span class=\"page\"></span>" +
                                        "<ul>" +
                                            "<li><a href=\"url?page=1\">First</a></li>" +
                                            "<li><a href=\"url?page=1\">Prev</a></li>" +
                                            "<li><a href=\"url?page=1\">1</a></li>" +
                                            "<li class=\"current\"><a href=\"url?page=2\">2</a></li>" +
                                            "<li><a href=\"url?page=3\">3</a></li>" +
                                            "<li><a href=\"url?page=3\">Next</a></li>" +
                                            "<li><a href=\"url?page=3\">Last</a></li>" +
                                        "</ul>" +
                                    "</div>";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PagedList_BuildPagingLastPage_Valid()
        {
            const int page = 3;
            const int pageSize = 1;
            const int shownEitherSide = 1;
            const string url = "url";

            List<string> source = new List<string> { "one", "two", "three" };

            var pagedList = new PagedList<string>(source.AsEnumerable(), page, pageSize);

            var actual = pagedList.BuildPagingLinks(shownEitherSide, url);

            const string expected = "<div class=\"pagination\">" +
                                        "<span class=\"page\"></span>" +
                                        "<ul>" +
                                            "<li><a href=\"url?page=1\">First</a></li>" +
                                            "<li><a href=\"url?page=2\">Prev</a></li>" +
                                            "<li><a href=\"url?page=1\">... </a></li>" +
                                            "<li><a href=\"url?page=2\">2</a></li>" +
                                            "<li class=\"current\"><a href=\"url?page=3\">3</a></li>" +
                                            "<li><a href=\"url?page=3\">Next</a></li>" +
                                            "<li><a href=\"url?page=3\">Last</a></li>" +
                                        "</ul>" +
                                    "</div>";

            Assert.AreEqual(expected, actual);
        }
    }
}
