namespace DMFX.NewsAnalysis.Parser.Common
{
    public interface IArticleParser
    {
        DMFX.NewsAnalysis.Interfaces.Entities.Article Parse(string rawContent);
    }
}
