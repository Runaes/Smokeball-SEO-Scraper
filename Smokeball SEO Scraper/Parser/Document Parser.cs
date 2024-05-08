using System.Text.RegularExpressions;
using System.Xml;

namespace Smokeball_SEO_Scraper
{
    internal class Document_Parser
    {
        #region Constructor

        public Document_Parser(DocumentParserSettings settings, IHtmlLoader loader) 
        {
            this.settings = settings;
            this.loader = loader;
        }
        IHtmlLoader loader;
        List<int> locationsFound = new List<int>();
        List<string> nodesFound = new List<string>();
        DocumentParserSettings settings;
        XmlDocument? xmlDocument;

        #endregion

        #region AddLocationsWhereExpectedValueFound

        public void AddLocationsWhereExpectedValueFound()
        {
            xmlDocument = xmlDocument ?? CreateXDocFromUrl(settings.Url);
            var root = xmlDocument.ChildNodes.Item(0);
            var location = 0;
            foreach (XmlNode child in root.ChildNodes)
            {
                location++;
                if (child.InnerXml.Contains(settings.ValueToFind))
                {
                    locationsFound.Add(location);
                    nodesFound.Add(child.InnerXml);
                }
            }
            if (locationsFound.Count == 0)
            {
                locationsFound.Add(0);
                return;
            }
        }

        #endregion

        #region CreateXDocFromUrl 

        XmlDocument CreateXDocFromUrl(string url)
        {
            var html = loader.GetResponseFromUrl(url);

            html = html.Replace("><", ">\r\n<"); // For Readability when Debugging.
            var results = new List<string>();
            var result = Regex.Match(html, settings.ResultRegex).Value;
            while (!string.IsNullOrEmpty(result))
            {
                result = CloseOffHangingDivTags(result, html);
                results.Add(result);
                html = html.Replace(result, string.Empty);
                result = Regex.Match(html, settings.ResultRegex).Value;
            }
            var resultsAsString = string.Join(string.Empty, results);

            var doc = new XmlDocument();

            resultsAsString = Regex.Replace(resultsAsString, @"<img[\w\W]*?>", string.Empty);
            resultsAsString = Regex.Replace(resultsAsString, @"<br>", string.Empty);
            resultsAsString = Regex.Replace(resultsAsString, @"&middot", string.Empty);

            doc.LoadXml("<root>" + resultsAsString + "</root>");
            return doc;
        }

        #endregion

        #region CloseOffHangingDivTags

        string CloseOffHangingDivTags(string result, string html)
        {
            var placeHolder = result;
            var resultHolder = result;
            while (Regex.Matches(resultHolder, @"<div").Count - Regex.Matches(resultHolder, @"</div").Count > 0)
            {
                if (!string.IsNullOrEmpty(placeHolder))
                {
                    html = html.Replace(placeHolder, settings.DivReplacementRegex);
                }
                placeHolder = Regex.Match(html, settings.ResultRegex).Value;
                resultHolder = resultHolder + placeHolder.Replace(settings.DivReplacementRegex, string.Empty);
            }
            return resultHolder;
        }

        #endregion

        public IEnumerable<int> LocationsFound => locationsFound;
        public IEnumerable<string> NodesFound => nodesFound;
    }
}
