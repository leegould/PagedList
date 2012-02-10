using System.Collections.Generic;
using NUnit.Framework;

namespace PagedList.Test
{
    [TestFixture]
    public class PagedListExtensionsTests
    {
        [Test]
        public void ToPagedList_IndexParam_Valid()
        {
            const int index = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var actual = source.ToPagedList(index);

            Assert.AreEqual(source.Count, actual.TotalCount);
            Assert.AreEqual(source.Count, actual.Count);
        }

        [Test]
        public void ToPagedList_IndexPageSizeParams_Valid()
        {
            const int index = 1;
            const int pageSize = 1;

            List<string> source = new List<string> { "one", "two", "three" };

            var actual = source.ToPagedList(index, pageSize);

            Assert.AreEqual(source.Count, actual.TotalCount);
            Assert.AreEqual(pageSize, actual.Count);
        }

        [Test]
        public void ToPagedList_IndexPageSizeTotalCountParams_Valid()
        {
            const int index = 1;
            const int pageSize = 1;
            const int totalSize = 10;

            List<string> source = new List<string> { "one", "two", "three" };

            var actual = source.ToPagedList(index, pageSize, totalSize);

            Assert.AreEqual(totalSize, actual.TotalCount);
            Assert.AreEqual(source.Count, actual.Count);
        }
    }
}
