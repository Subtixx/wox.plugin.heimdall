using System;
using System.Collections.Generic;
using System.Diagnostics;
using HtmlAgilityPack;
using NUnit.Framework;

namespace Wox.Plugin.Heimdall.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            //Assert.IsNotEmpty(apps);
            /*var pinList = doc.DocumentNode.SelectSingleNode("//ul[@id='pinlist']");
            var nodes = pinList.SelectNodes("li");

            var appList = new Dictionary<string, string>();
            var linkAmount = 0;
            foreach (var htmlNode in nodes)
            {
                var linkNode = htmlNode.SelectSingleNode("a");
                
                // hide "app.dashboard"
                if (linkNode.GetDataAttribute("id").Value == "0")
                    continue;
                
                linkAmount++;
                var link = linkNode.GetAttributeValue("href", "");
                var name = htmlNode.InnerText;
                if (appList.ContainsKey(name))
                {
                    continue;
                }
                
                appList.Add(name, link);
            }

            Assert.AreEqual(linkAmount, appList.Count);
            
            Assert.IsNotEmpty(appList);*/
        }
    }
}