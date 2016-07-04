using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XMLTestDataBuilder
{
    public interface IXmlTestDataBuilder
    {
        XDocument Build();

        IXmlTestDataBuilder SetRootElement(string elementName);

        IXmlTestDataBuilder AddElement(XElement child, XElement parent);

        IXmlTestDataBuilder RemoveElement(string elementName, XElement parent);
    }
}