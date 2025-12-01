using ObserveThing;

namespace Nessle
{
    public static class ToggleExtensions
    {
        public static T Value<T>(this T control, bool value)
            where T : IControl<UIBuilder.ToggleProps>
        {
            control.props.value.From(value);
            return control;
        }

        public static T Value<T>(this T control, IValueObservable<bool> value)
            where T : IControl<UIBuilder.ToggleProps>
        {
            control.props.value.From(value);
            return control;
        }
    }
}
