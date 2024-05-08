namespace Smokeball_SEO_Scraper
{
    public class DocumentParserSettings
    {
        public DocumentParserSettings(string valueToFind, string resultRegex, string urlToUseForParsing, string divReplacementRegex)
        {
            this.valueToFind = valueToFind;
            this.resultRegex = resultRegex;
            this.urlToUseForParsing = urlToUseForParsing;
            this.divReplacementRegex = divReplacementRegex;
        }

        public static DocumentParserSettings Default => new DocumentParserSettings(valueToFind: @"www.smokeball.com.au",
                resultRegex: @"<div class=""Gx5Zad fP1Qef xpd EtOod pkphOe"">[\s\S]*?<\/div>",
                urlToUseForParsing: "http://www.google.com.au/search?num=100&q=conveyancing+software",
                divReplacementRegex: @"<div class=""Gx5Zad fP1Qef xpd EtOod pkphOe"">");

        readonly string valueToFind;
        readonly string resultRegex;
        readonly string urlToUseForParsing;
        readonly string divReplacementRegex;
        readonly Func<string, string>? regexReplacementToMakeAsFinalisation;

        public string RegexReplacementToMakeAsFinalisation(string result) => regexReplacementToMakeAsFinalisation?.Invoke(result) ?? result;
        public string ValueToFind => valueToFind;
        public string ResultRegex => resultRegex;
        public string Url => urlToUseForParsing;
        public string DivReplacementRegex => divReplacementRegex;
    }
}
