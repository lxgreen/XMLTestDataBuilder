using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLTestDataBuilder
{
    public class PropertyAction<TOwner, TProperty> : PropertyActionBase
    {
        public Action<TOwner, TProperty> Action { get; set; }
    }

    public class PropertyAction<TOwner, TProperty, TValue> : PropertyActionBase
    {
        public Action<TOwner, TProperty, TValue> Action { get; set; }
    }

    public class PropertyAction<TOwner> : PropertyActionBase
    {
        public Action<TOwner> Action { get; set; }
    }
}