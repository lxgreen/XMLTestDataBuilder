using NUnit.Framework;
using XMLTestDataBuilder;

namespace TestDataBuilderTests
{
    [TestFixture]
    public class TestDataBuilderTester
    {
        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [Test]
        public void Instance()
        {
            var builder = new TestDataBuilder<Person>().Configure()
                .With<string>("Name", (td, name) => td.Name = name)
                .With<int>("Age", (td, age) => td.Age = age)
                .Builder;

            var data = builder
                .With("Name", "tester")
                .With("Age", 15)
                .Build();

            Assert.AreEqual("tester", data.Name);
            Assert.AreEqual(15, data.Age);
        }
    }
}