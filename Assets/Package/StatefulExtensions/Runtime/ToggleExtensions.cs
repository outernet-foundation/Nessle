using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class ToggleExtensions
    {
        public static void BindValue(this IControl<ToggleProps> control, ObservablePrimitive<bool> bindTo)
            => control.BindValue(x => x.isOn, bindTo);

        public static void BindValue<T>(this IControl<ToggleProps> control, ObservablePrimitive<T> bindTo, Func<bool, T> toSource, Func<T, bool> toControl)
            => control.BindValue(x => x.isOn, bindTo, toSource, toControl);
    }
}