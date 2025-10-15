using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class InputFieldExtensions
    {
        public static void BindValue(this IControl<InputFieldProps> control, ObservablePrimitive<string> bindTo)
            => control.BindValue(x => x.inputText.text, bindTo);

        public static void BindValue<T>(this IControl<InputFieldProps> control, ObservablePrimitive<T> bindTo, Func<string, T> toSource, Func<T, string> toControl)
            => control.BindValue(x => x.inputText.text, bindTo, toSource, toControl);
    }
}
