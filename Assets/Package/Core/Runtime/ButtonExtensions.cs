using System;
using ObserveThing;

namespace Nessle
{
    public static class ButtonExtensions
    {
        public static T OnClick<T>(this T control, Action onclick)
            where T : IControl<UIBuilder.ButtonProps>
        {
            control.props.onClick.From(onclick);
            return control;
        }

        public static T OnClick<T>(this T control, IValueObservable<Action> onclick)
            where T : IControl<UIBuilder.ButtonProps>
        {
            control.props.onClick.From(onclick);
            return control;
        }
    }
}
