using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using Wox.Infrastructure.Storage;

namespace Wox.Plugin.Heimdall
{
    public class Main : IPlugin, ISettingProvider, ISavable
    {
        private PluginInitContext _context;

        public const string Images = "Images";
        public static string ImagesDirectory;

        private readonly Settings _settings;
        private readonly SettingsViewModel _viewModel;

        private List<HeimdallApp> _appList = new List<HeimdallApp>();

        public Main()
        {
            _viewModel = new SettingsViewModel();
            _settings = _viewModel.Settings;
        }

        public void Init(PluginInitContext context)
        {
            _context = context;
            var pluginDirectory = _context.CurrentPluginMetadata.PluginDirectory;
            var bundledImagesDirectory = Path.Combine(pluginDirectory, Images);
            ImagesDirectory = Path.Combine(_context.CurrentPluginMetadata.PluginDirectory, Images);

            if (_settings.HeimdallUrl == "") return;

            _appList = Heimdall.RequestAppList2(_settings.HeimdallUrl, ImagesDirectory);
            foreach (var heimdallApp in _appList)
            {
                _settings.HeimdallApps.Add(heimdallApp);
            }
        }

        public List<Result> Query(Query query)
        {
            if (_settings.HeimdallUrl == "")
                return new List<Result>()
                {
                    new Result()
                    {
                        Title = "Heimdall URL missing!",
                        SubTitle = "Please enter the Heimdall URL in the settings!",
                        IcoPath = "Images\\icon.png",
                        Action = e => false
                    }
                };

            var results = new List<Result>();
            foreach (var heimdallApp in _appList)
            {
                if (!heimdallApp.Name.ToLower().Contains(query.Search.ToLower()) &&
                    !heimdallApp.Title.ToLower().Contains(query.Search.ToLower())) continue;

                results.Add(new Result()
                {
                    Title = heimdallApp.Name,
                    SubTitle = heimdallApp.Title,
                    IcoPath = "Images\\" + heimdallApp.Name + ".png",
                    Action = e =>
                    {
                        Process.Start(heimdallApp.Link);
                        return true;
                    }
                });
            }

            results.Add(new Result()
            {
                Title = "Refresh",
                SubTitle = "Refresh Cache",
                IcoPath = "Images\\icon.png",
                // Always align to bottom :)
                Score = -9999,
                Action = e =>
                {
                    _appList = Heimdall.RequestAppList2(_settings.HeimdallUrl, ImagesDirectory);
                    return true;
                }
            });

            return results;
        }

#region ISettingProvider Members

        public Control CreateSettingPanel()
        {
            return new SettingsControl(_context, _viewModel);
        }

#endregion

        public void Save()
        {
            _viewModel.Save();
        }
    }
}