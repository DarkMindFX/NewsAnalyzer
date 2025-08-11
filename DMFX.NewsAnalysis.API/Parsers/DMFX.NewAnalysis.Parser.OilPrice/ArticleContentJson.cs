using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Parser.OilPrice
{
    public class ArticleContentJson
    {
        public class AboutJson
        {
            [JsonPropertyName("@type")]
            public string Type { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

        }

        public class AuthorJson
        {
            [JsonPropertyName("@type")]
            public string Type { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class CopyrightHolderJson
        {
            [JsonPropertyName("@type")]
            public string Type { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }
        }

        public class PublisherLogoJson
        {
            [JsonPropertyName("@type")]
            public string Type { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }

            [JsonPropertyName("height")]
            public int Height { get; set; }

        }

        public class PublisherJson
        {
            [JsonPropertyName("@type")]
            public string Type { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("logo")]
            public List<PublisherLogoJson> Logo { get; set; }
        }

        public class AudienceJson
        {
            [JsonPropertyName("@type")]
            public string Type { get; set; }

            [JsonPropertyName("audienceType")]
            public string AudienceType { get; set; }
        }

        [JsonPropertyName("@context")]
        public string Context { get; set; }

        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }

        [JsonPropertyName("datePublished")]
        public DateTime DatePublished { get; set; }

        [JsonPropertyName("dateModified")]
        public DateTime DateModified { get; set; }

        [JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("wordCount")]
        public int WordCount { get; set; }

        [JsonPropertyName("inLanguage")]
        public string LnLanguage { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("articleBody")]
        public string ArticleBody { get; set; }

        [JsonPropertyName("headline")]
        public string Headline { get; set; }

        [JsonPropertyName("about")]
        public AboutJson About { get; set; }

        [JsonPropertyName("image")]
        public List<string> image { get; set; }

        [JsonPropertyName("author")]
        public List<AuthorJson> Author { get; set; }

        [JsonPropertyName("copyrightHolder")]
        public List<CopyrightHolderJson> CopyrightHolder { get; set; }

        [JsonPropertyName("publisher")]
        public List<PublisherJson> Publisher { get; set; }

        [JsonPropertyName("audience")]
        public List<AudienceJson> Audience { get; set; }
    }
}
