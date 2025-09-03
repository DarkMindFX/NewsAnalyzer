using DMFX.NewsAnalysis.Interfaces.Entities;
using DMFX.NewsAnalysis.Parser.OilPrice;
using NUnit.Framework;
using System.Text.Json.Serialization;
using Test.DMFX.NewsAnalysis.DAL.MSSQL;

namespace DMFX.NewsAnalysis.Parsers.Test
{

    public class TestOilPriceArticleParser : TestBase
    {
        #region Test Classes
        class TestCaseSetup
        {
            [JsonPropertyName("SourceFile")]
            public string SourceFile { get; set; }

            [JsonPropertyName("ExpectedResult")]
            public Article ExpectedResult
            {
                get; set;
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

        [TestCase("ArticleTest00.json")]
        [TestCase("ArticleTest01.json")]
        [TestCase("ArticleTest02.json")]
        [TestCase("ArticleTest03.json")]
        [TestCase("ArticleTest04.json")]
        [TestCase("ArticleTest05.json")]
        public void ParseArticle_Success(string caseName)
        {
            // Arrange
            var parser = new ArticleParser();
            var caseSetup = LoadTestCaseSetup(caseName);
            var content = File.ReadAllText(Path.Combine(TestBaseFolder, "OilPrice", caseSetup.SourceFile));
            // Act
            var article = parser.Parse(content);
            // Assert
            Assert.IsTrue(article.Timestamp <= DateTime.UtcNow);
            Assert.AreEqual(article.Title, caseSetup.ExpectedResult.Title);
            Assert.AreEqual(article.Content, caseSetup.ExpectedResult.Content);
            Assert.AreEqual(article.NewsTime, caseSetup.ExpectedResult.NewsTime);
            Assert.AreEqual(article.Url, caseSetup.ExpectedResult.Url);
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
