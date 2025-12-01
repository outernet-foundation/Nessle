using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public static class ScrollRectExtensions
    {
        public static T Value<T>(this T control, Vector2 value)
            where T : IControl<UIBuilder.ScrollRectProps>
        {
            control.props.value.From(value);
            return control;
        }

        public static T Value<T>(this T control, IValueObservable<Vector2> value)
            where T : IControl<UIBuilder.ScrollRectProps>
        {
            control.props.value.From(value);
            return control;
        }
    }
}
