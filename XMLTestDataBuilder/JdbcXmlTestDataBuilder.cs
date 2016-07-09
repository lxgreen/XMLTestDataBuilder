using System.Xml.Linq;

namespace XMLTestDataBuilder
{
    public class JdbcXmlTestDataBuilder : TestDataBuilder<XDocument>
    {
        public JdbcXmlTestDataBuilder()
        {
            TestData.AddFirst("jdbc-data-source");

            Configure()
                .With("jdbc-driver-params", td => AddOnce(td.Root, "jdbc-driver-params"))
                .Without("jdbc-driver-params", td => Remove(td.Root, "jdbc-driver-params"))
                .With("jdbc-data-source-params", td => AddOnce(td.Root, "jdbc-data-source-params"))
                .Without("jdbc-data-source-params", td => Remove(td.Root, "jdbc-data-source-params"))
                .With("properties", td =>
                {
                    AddOnce(td.Root, "jdbc-driver-params");
                    AddOnce(td.Element("jdbc-driver-params"), "properties");
                })
                .Without("properties", td => Remove(td.Root, "properties"))
                .With<string>("driver-name", (td, name) =>
                {
                    AddOnce(td.Root, "jdbc-driver-params");
                    AddOnce(td.Element("jdbc-driver-params"),
                        new XElement("driver-name") { Value = name });
                }).
                Without("driver-name", td => Remove(td.Root, "driver-name"))
                .With<string, string>("property", (td, name, value) =>
                {
                    AddOnce(td.Root, "jdbc-driver-params");
                    AddOnce(td.Element("jdbc-driver-params"), "properties");
                    AddOnce(td.Element("properties"),
                        new XElement("property",
                            new XElement("name") { Value = name },
                            new XElement("value") { Value = value }));
                });
        }

        private void Remove(XElement container, string elementName)
        {
            var element = container.GetElementByName(elementName);
            if (element != null)
            {
                element.Remove();
            }
        }

        private void AddOnce(XElement parent, string elementName)
        {
            if (parent.Contains(elementName)) return;
            var element = new XElement(elementName);
            parent.Add(element);
        }

        private void AddOnce(XElement parent, XElement element)
        {
            if (parent.Contains(element.Name.LocalName)) return;
            parent.Add(element);
        }
    }
}