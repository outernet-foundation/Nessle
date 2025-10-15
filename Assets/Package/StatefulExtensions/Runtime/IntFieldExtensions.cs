using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class IntFieldExtensions
    {
        public static void BindValue(this IControl<IntFieldProps> control, ObservablePrimitive<int> bindTo)
            => control.BindValue(x => x.value, bindTo);

        public static void BindValue<T>(this IControl<IntFieldProps> control, ObservablePrimitive<T> bindTo, Func<int, T> toSource, Func<T, int> toControl)
            => control.BindValue(x => x.value, bindTo, toSource, toControl);
    }
}
