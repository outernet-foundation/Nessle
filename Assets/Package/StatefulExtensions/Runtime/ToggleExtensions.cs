using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class ToggleExtensions
    {
        public static void Value(this IControl<ToggleProps> control, ObservablePrimitive<bool> bindTo)
            => control.Value(x => x.value, bindTo);

        public static void Value<T>(this IControl<ToggleProps> control, ObservablePrimitive<T> bindTo, Func<bool, T> toSource, Func<T, bool> toControl)
            => control.Value(x => x.value, bindTo, toSource, toControl);
    }
}