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