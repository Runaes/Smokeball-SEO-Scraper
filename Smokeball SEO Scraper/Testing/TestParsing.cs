using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.IO;
using System.Reflection;

#if DEBUG

namespace Smokeball_SEO_Scraper.Tests
{
    class TestParsing
    {
        [SetUp]
        public void Setup()
        {
            loaderMock = new Mock<IHtmlLoader>();
            parser = new Document_Parser(DocumentParserSettings.Default, loaderMock.Object);

            loaderMock.Setup(m => m.GetResponseFromUrl(It.IsAny<string>())).Returns(GetHtmlForLoader());
        }

        string GetHtmlForLoader()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var name = assembly.GetName().Name;
            using (var stream = assembly.GetManifestResourceStream($"Smokeball_SEO_Scraper.Testing.test.txt"))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        Mock<IHtmlLoader> loaderMock;
        Document_Parser parser;

        [Test]
        public void TestParse()
        {
            parser.AddLocationsWhereExpectedValueFound();
            Assert.That(() => parser.LocationsFound.Contains(4), "The Parser should have found the result in the 4th location");
            Assert.That(() => parser.LocationsFound.Contains(27), "The Parser should have found the result in the 27th location");
            Assert.That(() => parser.LocationsFound.Count() == 2, "The Parser should have found only two results");
        }

        [Test]
        public void TestParse_InvalidHtml() 
        {
            loaderMock.Setup(m => m.GetResponseFromUrl(It.IsAny<string>())).Returns(string.Empty);
            parser.AddLocationsWhereExpectedValueFound();
            Assert.That(() => parser.LocationsFound.Contains(0), "The Parser should not have found any result and returned the default value 0");
            Assert.That(() => parser.LocationsFound.Count() == 1, "The Parser should have only found a single default result");
        }

        [Test]
        public void TestParse_ActualLoader() 
        {
           parser = new Document_Parser(DocumentParserSettings.Default, HtmlLoader.Default);
           Assert.DoesNotThrow(()=> parser.AddLocationsWhereExpectedValueFound());
        }
    }
}

#endif