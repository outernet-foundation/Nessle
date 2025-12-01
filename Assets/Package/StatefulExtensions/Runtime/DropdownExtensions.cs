using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class DropdownExtensions
    {
        public static void Value(this IControl<DropdownProps> control, ObservablePrimitive<int> bindTo)
            => control.Value(x => x.value, bindTo);

        public static void Value<T>(this IControl<DropdownProps> control, ObservablePrimitive<T> bindTo, Func<int, T> toSource, Func<T, int> toControl)
            => control.Value(x => x.value, bindTo, toSource, toControl);
    }
}