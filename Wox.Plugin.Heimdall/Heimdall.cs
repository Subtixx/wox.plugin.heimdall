using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace Wox.Plugin.Heimdall
{
    public static class Heimdall
    {
        public static List<HeimdallApp> RequestAppList2(string url, string folderDir)
        {
            try
            {
                var uri = new Uri(url);
            }
            catch (Exception e)
            {
                return new List<HeimdallApp>();
            }
            
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var appList = doc.DocumentNode.SelectSingleNode("//div[@id='sortable']");
            var pinnedApps = appList.SelectNodes("section");

            var apps = new List<HeimdallApp>();
            foreach (var pinnedApp in pinnedApps)
            {
                var itemNode = pinnedApp.SelectSingleNode("div[@class='item']");
                if (itemNode == null)
                    continue; // Last node maybe?

                var imgNode = itemNode.SelectSingleNode("img[@class='app-icon']");
                var detailsNode = itemNode.SelectSingleNode("div[@class='details']");
                var titleNode = detailsNode.SelectSingleNode("div[contains(@class, 'title')]");
                var linkNode = itemNode.SelectSingleNode("a[contains(@class, 'link')]");

                var id = pinnedApp.GetDataAttribute("id").Value;
                var img = imgNode.GetAttributeValue("src", "");
                var appName = titleNode.InnerText;
                var link = linkNode.GetAttributeValue("href", "");
                var title = WebUtility.HtmlDecode(linkNode.GetAttributeValue("title", ""))
                    .Replace(System.Environment.NewLine, "");

                var heimdallApp = new HeimdallApp(id, appName, link, img, title);
                apps.Add(heimdallApp);

                if (File.Exists(Path.Combine(folderDir, heimdallApp.Name + ".png"))) continue;
                using (var wc = new WebClient())
                {
                    wc.DownloadFile(heimdallApp.Image, Path.Combine(folderDir, heimdallApp.Name) + ".png");
                }
            }

            return apps;
        }
    }
}