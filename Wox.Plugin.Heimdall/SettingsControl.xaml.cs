using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Wox.Plugin.Heimdall
{
    /// <summary>
    /// Interaction logic for WebSearchesSetting.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        private readonly Settings _settings;
        private readonly PluginInitContext _context;

        public SettingsControl(PluginInitContext context, SettingsViewModel viewModel)
        {
            InitializeComponent();
            _context = context;
            _settings = viewModel.Settings;
            DataContext = viewModel;
            browserPathBox.Text = _settings.BrowserPath;
        }
        
        private void OnChooseClick(object sender, RoutedEventArgs e)
        {
            var fileBrowserDialog = new OpenFileDialog();
            fileBrowserDialog.Filter = "Application(*.exe)|*.exe|All files|*.*";
            fileBrowserDialog.CheckFileExists = true;
            fileBrowserDialog.CheckPathExists = true;
            if (fileBrowserDialog.ShowDialog() == true)
            {
                browserPathBox.Text = fileBrowserDialog.FileName;
                _settings.BrowserPath = fileBrowserDialog.FileName;
            }
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            ((SettingsViewModel)DataContext).Save();
        }
    }
}