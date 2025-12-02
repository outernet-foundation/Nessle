using System.Collections.Generic;
using ObserveThing;

namespace Nessle
{
    public static class DropdownExtensions
    {
        public static T Options<T>(this T control, params string[] options)
            where T : IControl<UIBuilder.DropdownProps> => control.Options((IEnumerable<string>)options);

        public static T Options<T>(this T control, IEnumerable<string> options)
            where T : IControl<UIBuilder.DropdownProps>
        {
            control.props.options.From(options);
            return control;
        }

        public static T Options<T>(this T control, IListObservable<string> options)
            where T : IControl<UIBuilder.DropdownProps>
        {
            control.props.options.From(options);
            return control;
        }
    }
}
