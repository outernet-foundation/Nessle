using ObserveThing;

namespace Nessle
{
    public static class SliderExtensions
    {
        public static T MinValue<T>(this T control, float minValue)
            where T : IControl<SliderProps>
        {
            control.props.minValue.From(minValue);
            return control;
        }

        public static T MinValue<T>(this T control, IValueObservable<float> minValue)
            where T : IControl<SliderProps>
        {
            control.props.minValue.From(minValue);
            return control;
        }

        public static T MaxValue<T>(this T control, float maxValue)
            where T : IControl<SliderProps>
        {
            control.props.minValue.From(maxValue);
            return control;
        }

        public static T MaxValue<T>(this T control, IValueObservable<float> maxValue)
            where T : IControl<SliderProps>
        {
            control.props.minValue.From(maxValue);
            return control;
        }
    }
}
