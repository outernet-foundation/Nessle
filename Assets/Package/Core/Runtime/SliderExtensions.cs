using ObserveThing;

namespace Nessle
{
    public static class SliderExtensions
    {
        public static T Value<T>(this T control, float value)
            where T : IControl<UIBuilder.SliderProps>
        {
            control.props.value.From(value);
            return control;
        }

        public static T Value<T>(this T control, IValueObservable<float> value)
            where T : IControl<UIBuilder.SliderProps>
        {
            control.props.value.From(value);
            return control;
        }

        public static T MinValue<T>(this T control, float minValue)
            where T : IControl<UIBuilder.SliderProps>
        {
            control.props.minValue.From(minValue);
            return control;
        }

        public static T MinValue<T>(this T control, IValueObservable<float> minValue)
            where T : IControl<UIBuilder.SliderProps>
        {
            control.props.minValue.From(minValue);
            return control;
        }

        public static T MaxValue<T>(this T control, float maxValue)
            where T : IControl<UIBuilder.SliderProps>
        {
            control.props.minValue.From(maxValue);
            return control;
        }

        public static T MaxValue<T>(this T control, IValueObservable<float> maxValue)
            where T : IControl<UIBuilder.SliderProps>
        {
            control.props.minValue.From(maxValue);
            return control;
        }
    }
}
