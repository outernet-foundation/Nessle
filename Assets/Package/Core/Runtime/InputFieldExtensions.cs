using System;
using ObserveThing;

namespace Nessle
{
    public static class InputFieldExtensions
    {
        public static T Value<T>(this T control, string value)
            where T : IControl<UIBuilder.InputFieldProps>
        {
            control.props.inputText.text.From(value);
            return control;
        }

        public static T Value<T>(this T control, IValueObservable<string> value)
            where T : IControl<UIBuilder.InputFieldProps>
        {
            control.props.inputText.text.From(value);
            return control;
        }

        public static T OnEndEdit<T>(this T control, Action<string> onEndEdit)
            where T : IControl<UIBuilder.InputFieldProps>
        {
            control.props.onEndEdit.From(onEndEdit);
            return control;
        }

        public static T OnEndEdit<T>(this T control, IValueObservable<Action<string>> onEndEdit)
            where T : IControl<UIBuilder.InputFieldProps>
        {
            control.props.onEndEdit.From(onEndEdit);
            return control;
        }
    }
}
