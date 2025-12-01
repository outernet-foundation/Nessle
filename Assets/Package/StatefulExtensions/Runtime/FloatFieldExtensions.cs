using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class FloatFieldExtensions
    {
        public static void Value(this IControl<FloatFieldProps> control, ObservablePrimitive<float> bindTo)
            => control.Value(x => x.value, bindTo);

        public static void Value<T>(this IControl<FloatFieldProps> control, ObservablePrimitive<T> bindTo, Func<float, T> toSource, Func<T, float> toControl)
            => control.Value(x => x.value, bindTo, toSource, toControl);
    }
}
