using System;
using FofX.Stateful;
using UnityEngine;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class ScrollRectExtensions
    {
        public static void BindValue(this IControl<ScrollRectProps> control, ObservablePrimitive<Vector2> bindTo)
            => control.BindValue(x => x.value, bindTo);

        public static void BindValue<T>(this IControl<ScrollRectProps> control, ObservablePrimitive<T> bindTo, Func<Vector2, T> toSource, Func<T, Vector2> toControl)
            => control.BindValue(x => x.value, bindTo, toSource, toControl);
    }
}