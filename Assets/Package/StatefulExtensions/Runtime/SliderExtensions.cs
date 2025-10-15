using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class SliderExtensions
    {
        public static void BindValue(this IControl<SliderProps> control, ObservablePrimitive<float> bindTo)
            => control.BindValue(x => x.value, bindTo);

        public static void BindValue<T>(this IControl<SliderProps> control, ObservablePrimitive<T> bindTo, Func<float, T> toSource, Func<T, float> toControl)
            => control.BindValue(x => x.value, bindTo, toSource, toControl);
    }
}