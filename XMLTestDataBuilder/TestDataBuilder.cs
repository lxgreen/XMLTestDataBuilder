using System;

namespace XMLTestDataBuilder
{
    public class TestDataBuilder<T> where T : class, new()
    {
        protected readonly T TestData;
        private readonly TestDataBuilderConfigurator<T> _configurator;

        public TestDataBuilderConfigurator<T> Configure()
        {
            return _configurator;
        }

        public T Build()
        {
            return TestData;
        }

        public TestDataBuilder()
        {
            TestData = new T();
            _configurator = new TestDataBuilderConfigurator<T>(this);
        }

        public TestDataBuilder<T> With<TProperty>(string name, TProperty value)
        {
            PropertyActionBase action;
            if (!_configurator.WithActions.TryGetValue(name, out action))
            {
                throw new ArgumentException(name, "name");
            }

            var propertyAction = action as PropertyAction<T, TProperty>;
            if (propertyAction != null)
            {
                propertyAction.Action(TestData, value);
            }

            return this;
        }
    }
}