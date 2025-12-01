using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class ToggleExtensions
    {
        public static T Value<T>(this T control, ObservablePrimitive<bool> bindTo)
            where T : IControl<ToggleProps> => control.Value(x => x.props.value, bindTo);

        public static TControl Value<TControl, TValue>(this TControl control, ObservablePrimitive<TValue> bindTo, Func<bool, TValue> toSource, Func<TValue, bool> toControl)
            where TControl : IControl<ToggleProps> => control.Value(x => x.props.value, bindTo, toSource, toControl);
    }
}