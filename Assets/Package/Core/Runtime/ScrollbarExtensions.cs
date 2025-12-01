using ObserveThing;

namespace Nessle
{
    public static class ScrollbarExtensions
    {
        public static T Value<T>(this T control, float value)
            where T : IControl<UIBuilder.ScrollbarProps>
        {
            control.props.value.From(value);
            return control;
        }

        public static T Value<T>(this T control, IValueObservable<float> value)
            where T : IControl<UIBuilder.ScrollbarProps>
        {
            control.props.value.From(value);
            return control;
        }
    }
}
