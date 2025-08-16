using DMFX.NewsAnalysis.Parser.Common;
using DMFX.NewsAnalysis.Parser.OilPrice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test.DMFX.NewsAnalysis.DAL.MSSQL;

namespace DMFX.NewsAnalysis.Parsers.Test
{
    public class TestOilPriceSourceCrawler : TestBase
    {
        #region Test Classes
        class TestCaseSetup
        {
            public class ExpectedResultValues
            {
                [JsonPropertyName("ArticlesFound")]
                public int ArticlesFound { get; set; }
            }

            public TestCaseSetup()
            {
                ExpectedResult = new ExpectedResultValues();
            }

            [JsonPropertyName("SourceFolder")]
            public string SourceFolder { get; set; }

            [JsonPropertyName("DateStart")]
            public DateTime? DateStart { get; set; }

            [JsonPropertyName("DateEnd")]
            public DateTime? DateEnd { get; set; }

            [JsonPropertyName("ExpectedResult")]
            public ExpectedResultValues ExpectedResult
            {
                get; set;
            }
        }

        class TestPaginator : ISourcePaginator
        {
            private int _currentPage = 0;
            private readonly string _contentFolder;
            private List<string> _pages;
            public TestPaginator(string contentFolder)
            {
                _contentFolder = contentFolder;
            }

            public int TotalPagesCount
            {
                get
                {
                    if(_pages == null || _pages.Count == 0)
                    {
                        _pages = new List<string>();
                        Directory.EnumerateFiles(_contentFolder, "*.html")
                            .ToList()
                            .ForEach(file => _pages.Add(file) );
                    }
                    return _pages.Count;
                }
            }

            public bool HasNextPage
            {
                get
                {
                    return _currentPage < TotalPagesCount ;
                }
            }

            public string GetNextPageUrl()
            {
                return _pages[_currentPage++];
            }

            public void Initialize()
            {
                _currentPage = 1;
                _pages = new List<string>();
            }

            public void Reset()
            {
                _currentPage = 0;
            }
        }

        #endregion

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

        [TestCase("CrawlerTest00.json")]
        [TestCase("CrawlerTest01.json")]
        [TestCase("CrawlerTest02.json")]
        [TestCase("CrawlerTest03.json")]
        public void CrawlSource_Success(string caseName)
        {
            // Arrange

            var testCaseSetup = LoadTestCaseSetup(caseName);

            var crawler = new SourceCrawler();
            var paginator = new TestPaginator(Path.Combine(TestBaseFolder, "OilPrice", testCaseSetup.SourceFolder));
            int articlesCount = crawler.StartCrawling(new SourceCrawlerParams()
            {
                StartDate = testCaseSetup.DateStart,
                EndDate = testCaseSetup.DateEnd,
                Paginator = paginator
            });

            Assert.AreEqual(testCaseSetup.ExpectedResult.ArticlesFound, articlesCount, "Number of articles found does not match expected result.");
        }

        private TestCaseSetup LoadTestCaseSetup(string caseName)
        {
            string path = Path.Combine(TestBaseFolder, "OilPrice", caseName);
            var content = File.ReadAllText(path);

            var json = System.Text.Json.JsonSerializer.Deserialize<TestCaseSetup>(content);

            return json;
        }


    }
}
