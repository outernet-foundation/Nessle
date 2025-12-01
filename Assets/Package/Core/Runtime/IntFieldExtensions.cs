using ObserveThing;

namespace Nessle
{
    public static class IntFieldExtensions
    {
        public static T Value<T>(this T control, int value)
            where T : IControl<UIBuilder.IntFieldProps>
        {
            control.props.value.From(value);
            return control;
        }

        public static T Value<T>(this T control, IValueObservable<int> value)
            where T : IControl<UIBuilder.IntFieldProps>
        {
            control.props.value.From(value);
            return control;
        }
    }
}
