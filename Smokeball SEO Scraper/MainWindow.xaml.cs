using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Smokeball_SEO_Scraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public DocumentParserSettings ParserSettings
        {
            get;
            set;
        } = DocumentParserSettings.Default;

        private void Activator_Click(object sender, RoutedEventArgs e)
        {
            var parser = new Document_Parser(ParserSettings, HtmlLoader.Default);
            Browser.Navigate(ParserSettings.Url);
            parser.AddLocationsWhereExpectedValueFound();

            if (parser.LocationsFound.Contains(0))
            {
                Output.Text = "Expected text was not found in the search";
            }
            else
            {
                Output.Text = $"Text found at the following locations:\r\n{string.Join("\r\n", parser.LocationsFound.Select(i => i.RankingToEnglishConverter()))} Place.";
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsPanel(this);
            settings.ShowDialog();
        }
    }
}