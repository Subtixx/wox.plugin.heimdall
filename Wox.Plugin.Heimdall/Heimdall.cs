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
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace Wox.Plugin.Heimdall
{
    public static class Heimdall
    {
        public static List<HeimdallApp> RequestAppList(string url, string folderDir)
        {
            try
            {
                var uri = new Uri(url);
            }
            catch (Exception)
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