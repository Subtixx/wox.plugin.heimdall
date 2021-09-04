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

using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Wox.Plugin.Heimdall
{
    public class Settings : BaseModel
    {
        public string BrowserPath { get; set; }
        
        public string HeimdallUrl { get; set; }

        [JsonIgnore]
        public ObservableCollection<HeimdallApp> HeimdallApps { get; } = new ObservableCollection<HeimdallApp>
        {
        };
    }
}