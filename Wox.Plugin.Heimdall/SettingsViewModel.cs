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
using Wox.Infrastructure.Storage;

namespace Wox.Plugin.Heimdall
{
    public class SettingsViewModel
    {
        private readonly PluginJsonStorage<Settings> _storage;
        public SettingsViewModel()
        {
            _storage = new PluginJsonStorage<Settings>();
            Settings = _storage.Load();
        }

        public Settings Settings { get; set; }

        public void Save()
        {
            _storage.Save();
        }
    }
}