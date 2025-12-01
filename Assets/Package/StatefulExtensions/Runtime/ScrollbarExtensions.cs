using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class ScrollbarExtensions
    {
        public static T Value<T>(this T control, ObservablePrimitive<float> bindTo)
            where T : IControl<ScrollbarProps> => control.Value(x => x.props.value, bindTo);


        public static TControl Value<TControl, TValue>(this TControl control, ObservablePrimitive<TValue> bindTo, Func<float, TValue> toSource, Func<TValue, float> toControl)
            where TControl : IControl<ScrollbarProps> => control.Value(x => x.props.value, bindTo, toSource, toControl);

    }
}