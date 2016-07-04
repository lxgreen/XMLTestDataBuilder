using System;
using System.Xml.Linq;

namespace XMLTestDataBuilder
{
    public class JdbcXmlTestDataBuilder : XmlTestDataBuilder
    {
        public JdbcXmlTestDataBuilder()
        {
            ((IXmlTestDataBuilder) this).SetRootElement("jdbc-data-source");
        }

        public JdbcXmlTestDataBuilder WithJdbcDriverParams()
        {
            if (!Document.Root.Contains("jdbc-driver-params"))
            {
                var driverParams = new XElement("jdbc-driver-params");
                ((IXmlTestDataBuilder) this).AddElement(driverParams, Document.Root);
            }
            return this;
        }

        public JdbcXmlTestDataBuilder WithProperties()
        {
            if (!Document.Root.Contains("properties"))
            {
                var properties = new XElement("properties");
                ((IXmlTestDataBuilder)WithJdbcDriverParams()).AddElement(properties, 
                    Document.Element("jdbc-driver-params"));
            }
            return this;
        }

        public JdbcXmlTestDataBuilder WithJdbcDataSourceParams()
        {
            if (!Document.Root.Contains("jdbc-data-source-params"))
            {
                var dataSourceParams = new XElement("jdbc-data-source-params");
                ((IXmlTestDataBuilder)this).AddElement(dataSourceParams, Document.Root); 
            }
            return this;
        }
        public JdbcXmlTestDataBuilder WithDriverName(string driverName)
        {
            if (!Document.Root.Contains("driver-name"))
            {
                var driverNameElement = new XElement("driver-name")
                {
                    Value = driverName
                };
                ((IXmlTestDataBuilder) WithJdbcDriverParams()).AddElement(driverNameElement,
                    Document.Element("jdbc-driver-params"));
            }
            return this;
        }

        public JdbcXmlTestDataBuilder WithProperty(string name, string value)
        {
            var property = new XElement("property", 
                new XElement("name") { Value = name }, 
                new XElement("value") { Value = value});

            ((IXmlTestDataBuilder)WithProperties()).AddElement(property,
                Document.Element("properties"));
            return this;
        }

        public JdbcXmlTestDataBuilder WithEncryptedPassword(string password)
        {
            throw new NotImplementedException();
        }

        public JdbcXmlTestDataBuilder WithUrl(string url)
        {
            throw new NotImplementedException();
        }

        public JdbcXmlTestDataBuilder WithDataType(string dataType)
        {
            throw new NotImplementedException();
        }

        public JdbcXmlTestDataBuilder Valid
        {
            get
            {
                return WithDriverName("oracle.driver")
                    .WithProperty("user", "someuser")
                    .WithJdbcDataSourceParams();
            }
        }

    }
}