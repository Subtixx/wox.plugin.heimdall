/*
    Wox.Plugin.Heimdall - A Heimdall integration for wox
    Copyright (C) 2021 Dominic Hock

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

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

            _appList = Heimdall.RequestAppList(_settings.HeimdallUrl, ImagesDirectory);
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
                    _appList = Heimdall.RequestAppList(_settings.HeimdallUrl, ImagesDirectory);
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