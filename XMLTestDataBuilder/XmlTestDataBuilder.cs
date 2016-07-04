using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XMLTestDataBuilder
{
    public class XmlTestDataBuilder : IXmlTestDataBuilder
    {
        protected XDocument Document;

        public XmlTestDataBuilder()
        {
            Document = new XDocument();
        }

        public XDocument Build()
        {
            return Document;
        }

        IXmlTestDataBuilder IXmlTestDataBuilder.SetRootElement(string elementName)
        {
            Document.RemoveNodes();
            Document.AddFirst(new XElement(elementName));
            return this;
        }

        IXmlTestDataBuilder IXmlTestDataBuilder.AddElement(XElement child, XElement parent)
        {
            if (Document.Root == null)
            {
                return this;
            }

            if (Document.Root.Name == parent.Name)
            {
                Document.Root.Add(child);
            }
            else if (Document.Descendants().Any())
            {
                Document.Descendants()
                    .Single(e => e.Name == parent.Name).Add(child);
            }

            return this;
        }

        IXmlTestDataBuilder IXmlTestDataBuilder.RemoveElement(string elementName, XElement parent)
        {
            if (Document.Root == null)
            {
                return this;
            }

            if (Document.Root.Name == parent.Name)
            {
                Document.Root.Descendants()
                    .Where(e=> e.Name == elementName).Remove();
            }

            return this;
        }
    }
}