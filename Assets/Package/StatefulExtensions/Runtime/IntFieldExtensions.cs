using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class IntFieldExtensions
    {
        public static T Value<T>(this T control, ObservablePrimitive<int> bindTo)
            where T : IControl<IntFieldProps> => control.Value(x => x.props.value, bindTo);

        public static TControl Value<TControl, TValue>(this TControl control, ObservablePrimitive<TValue> bindTo, Func<int, TValue> toSource, Func<TValue, int> toControl)
            where TControl : IControl<IntFieldProps> => control.Value(x => x.props.value, bindTo, toSource, toControl);
    }
}
