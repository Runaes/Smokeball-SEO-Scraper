using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Smokeball_SEO_Scraper
{
    /// <summary>
    /// Interaction logic for SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : Window
    {
        public SettingsPanel(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        MainWindow mainWindow;

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ParserSettings = new DocumentParserSettings(valueToFind: valueToFindInSEO.Text, resultRegex: resultFilterRegex.Text, urlToUseForParsing: urlToUseForParsing.Text, divReplacementRegex: divReplacementRegex.Text);
            Close();
        }
    }
}
