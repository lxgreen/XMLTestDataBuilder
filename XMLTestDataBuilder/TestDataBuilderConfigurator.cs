using System;
using System.Collections.Generic;

namespace XMLTestDataBuilder
{
    public class TestDataBuilderConfigurator<T> where T : class, new()
    {
        public TestDataBuilder<T> Builder { get; }

        public Dictionary<string, PropertyActionBase> WithActions
        {
            get { return _withActionsByName; }
        }

        public Dictionary<string, PropertyActionBase> WithoutActions
        {
            get { return _withoutActionsByName; }
        }

        private readonly Dictionary<string, PropertyActionBase> _withActionsByName;
        private readonly Dictionary<string, PropertyActionBase> _withoutActionsByName;

        public TestDataBuilderConfigurator(TestDataBuilder<T> builder)
        {
            Builder = builder;
            _withActionsByName = new Dictionary<string, PropertyActionBase>();
            _withoutActionsByName = new Dictionary<string, PropertyActionBase>();
        }

        public TestDataBuilderConfigurator<T> With(string propertyName, Action<T> action)
        {
            var propertyAction = new PropertyAction<T>
            {
                Action = action
            };
            AppendActionToCollection(propertyName, propertyAction, _withActionsByName);
            return this;
        }

        public TestDataBuilderConfigurator<T> Without(string propertyName, Action<T> action)
        {
            var propertyAction = new PropertyAction<T>
            {
                Action = action
            };
            AppendActionToCollection(propertyName, propertyAction, _withoutActionsByName);
            return this;
        }

        public TestDataBuilderConfigurator<T> With<TParam1>(string propertyName, Action<T, TParam1> action)
        {
            var propertyAction = new PropertyAction<T, TParam1>
            {
                Action = action
            };
            AppendActionToCollection(propertyName, propertyAction, _withActionsByName);
            return this;
        }

        public TestDataBuilderConfigurator<T> With<TParam1, TParam2>(string propertyName, Action<T, TParam1, TParam2> action)
        {
            var propertyAction = new PropertyAction<T, TParam1, TParam2>
            {
                Action = action
            };
            AppendActionToCollection(propertyName, propertyAction, _withActionsByName);
            return this;
        }

        private void AppendActionToCollection(string propertyName, PropertyActionBase propertyAction, Dictionary<string, PropertyActionBase> collection)
        {
            PropertyActionBase existingAction;
            if (collection.TryGetValue(propertyName, out existingAction))
            {
                collection[propertyName] = propertyAction;
            }
            else
            {
                collection.Add(propertyName, propertyAction);
            }
        }
    }
}