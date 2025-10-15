using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class FloatFieldExtensions
    {
        public static void BindValue(this IControl<FloatFieldProps> control, ObservablePrimitive<float> bindTo)
            => control.BindValue(x => x.value, bindTo);

        public static void BindValue<T>(this IControl<FloatFieldProps> control, ObservablePrimitive<T> bindTo, Func<float, T> toSource, Func<T, float> toControl)
            => control.BindValue(x => x.value, bindTo, toSource, toControl);
    }
}
