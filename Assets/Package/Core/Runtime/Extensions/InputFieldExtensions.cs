using System;
using ObserveThing;

namespace Nessle
{
    public static class InputFieldExtensions
    {
        public static T OnEndEdit<T>(this T control, Action<string> onEndEdit)
            where T : IControl<InputFieldProps>
        {
            control.props.onEndEdit.From(onEndEdit);
            return control;
        }

        public static T OnEndEdit<T>(this T control, IValueObservable<Action<string>> onEndEdit)
            where T : IControl<InputFieldProps>
        {
            control.props.onEndEdit.From(onEndEdit);
            return control;
        }
    }
}
