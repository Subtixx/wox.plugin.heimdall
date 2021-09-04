namespace Wox.Plugin.Heimdall
{
    public class HeimdallApp
    {
        public string Id { get; }
        
        public string Title { get; }
        public string Image { get; }
        public string Name { get; }
        public string Link { get; }

        public HeimdallApp(string id, string name, string link, string image, string title)
        {
            Id = id;
            Name = name;
            Link = link;
            Image = image;
            Title = title;
        }
    }
}