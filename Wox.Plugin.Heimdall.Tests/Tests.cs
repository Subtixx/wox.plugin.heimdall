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