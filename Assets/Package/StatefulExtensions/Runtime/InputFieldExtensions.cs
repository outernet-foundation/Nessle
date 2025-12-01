using System;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle.StatefulExtensions
{
    public static class InputFieldExtensions
    {
        public static T Value<T>(this T control, ObservablePrimitive<string> bindTo)
            where T : IControl<InputFieldProps> => control.Value(x => x.props.inputText.text, bindTo);

        public static TControl Value<TControl, TValue>(this TControl control, ObservablePrimitive<TValue> bindTo, Func<string, TValue> toSource, Func<TValue, string> toControl)
            where TControl : IControl<InputFieldProps> => control.Value(x => x.props.inputText.text, bindTo, toSource, toControl);
    }
}
