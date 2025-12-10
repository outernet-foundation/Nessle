using ObserveThing;

namespace Nessle
{
    public static class ScrollRectExtensions
    {
        public static T Content<T>(this T control, IControl content)
            where T : IControl<ScrollRectProps>
        {
            control.props.content.From(content);
            return control;
        }

        public static T Content<T>(this T control, IValueObservable<IControl> content)
            where T : IControl<ScrollRectProps>
        {
            control.props.content.From(content);
            return control;
        }
    }
}