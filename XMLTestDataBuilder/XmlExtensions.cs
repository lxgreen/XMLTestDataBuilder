using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLTestDataBuilder
{
    public static class XmlExtensions
    {
        public static bool Contains(this XContainer container, string elementName)
        {
            return container.Elements().Any() &&
                   container.Descendants().Any(e => e.Name == elementName);
        }

        public static XElement GetElementByName(this XContainer container, string elementName)
        {
            return container.Descendants().SingleOrDefault(e => e.Name == elementName);
        }
    }
}
