using ObserveThing;

namespace Nessle
{
    public static class TextExtensions
    {
        public static T Value<T>(this T control, string value)
            where T : IControl<UIBuilder.TextProps>
        {
            control.props.text.From(value);
            return control;
        }

        public static T Value<T>(this T control, IValueObservable<string> value)
            where T : IControl<UIBuilder.TextProps>
        {
            control.props.text.From(value);
            return control;
        }
    }
}
