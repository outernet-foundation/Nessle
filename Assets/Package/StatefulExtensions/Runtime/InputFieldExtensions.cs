using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class InputFieldExtensions
    {
        public static void Value(this IControl<InputFieldProps> control, ObservablePrimitive<string> bindTo)
            => control.Value(x => x.inputText.text, bindTo);

        public static void Value<T>(this IControl<InputFieldProps> control, ObservablePrimitive<T> bindTo, Func<string, T> toSource, Func<T, string> toControl)
            => control.Value(x => x.inputText.text, bindTo, toSource, toControl);
    }
}
