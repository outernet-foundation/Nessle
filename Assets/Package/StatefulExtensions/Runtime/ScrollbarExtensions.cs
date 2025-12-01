using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class ScrollbarExtensions
    {
        public static void Value(this IControl<ScrollbarProps> control, ObservablePrimitive<float> bindTo)
            => control.Value(x => x.value, bindTo);

        public static void Value<T>(this IControl<ScrollbarProps> control, ObservablePrimitive<T> bindTo, Func<float, T> toSource, Func<T, float> toControl)
            => control.Value(x => x.value, bindTo, toSource, toControl);
    }
}