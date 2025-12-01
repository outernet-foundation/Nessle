using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class IntFieldExtensions
    {
        public static void Value(this IControl<IntFieldProps> control, ObservablePrimitive<int> bindTo)
            => control.Value(x => x.value, bindTo);

        public static void Value<T>(this IControl<IntFieldProps> control, ObservablePrimitive<T> bindTo, Func<int, T> toSource, Func<T, int> toControl)
            => control.Value(x => x.value, bindTo, toSource, toControl);
    }
}
