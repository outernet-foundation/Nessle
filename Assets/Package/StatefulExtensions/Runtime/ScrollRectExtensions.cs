using System;
using FofX.Stateful;
using UnityEngine;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class ScrollRectExtensions
    {
        public static T Value<T>(this T control, ObservablePrimitive<Vector2> bindTo)
            where T : IControl<ScrollRectProps> => control.Value(x => x.props.value, bindTo);

        public static TControl Value<TControl, TValue>(this TControl control, ObservablePrimitive<TValue> bindTo, Func<Vector2, TValue> toSource, Func<TValue, Vector2> toControl)
            where TControl : IControl<ScrollRectProps> => control.Value(x => x.props.value, bindTo, toSource, toControl);
    }
}