using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMFX.NewsAnalysis.Parser.OilPrice;

namespace DMFX.NewsAnalysis.Parsers.Test
{
    public class TestOilPriceSourcePaginator
    {
        [SetUp]
        public void Setup()
        {
            // Initialize any resources needed for the tests
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup
        }

        [Test]
        public void GetPagesCount_Success()
        {
            // Arrange
            var paginator = new SourcePaginator();
            paginator.Initialize();
            // Act
            int pagesCount = paginator.TotalPagesCount;
            bool hasPages = paginator.HasNextPage;
            // Assert
            Assert.Greater(pagesCount, 0, "Pages count should be greater than zero.");
            Assert.IsTrue(hasPages, "Paginator should have next pages available.");
        }

        [Test]
        public void IterateThruPages_Success()
        {
            // Arrange
            var paginator = new SourcePaginator();
            paginator.Initialize();
            // Act
            int steps = 10;
            while(steps > 0)
            {
                if (paginator.HasNextPage)
                {
                    string nextPageUrl = paginator.GetNextPageUrl();
                    Assert.IsNotNull(nextPageUrl, "Next page URL should not be null.");
                    --steps;
                }
            }
        }

    }
}
