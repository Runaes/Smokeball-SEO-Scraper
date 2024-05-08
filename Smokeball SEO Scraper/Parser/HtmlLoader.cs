using System.Net.Http;

namespace Smokeball_SEO_Scraper
{
    class HtmlLoader : IHtmlLoader
    {
        public string GetResponseFromUrl(string fullUrl)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(fullUrl);
                return response.Result;
            }
        }

        public static HtmlLoader Default => new HtmlLoader();
    }

    public interface IHtmlLoader
    {
        string GetResponseFromUrl(string fullUrl);
    }
}
