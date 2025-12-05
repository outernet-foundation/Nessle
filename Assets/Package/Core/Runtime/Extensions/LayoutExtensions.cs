using UnityEngine;

using ObserveThing;
using static Nessle.UIBuilder;

namespace Nessle
{
    public static class LayoutExtensions
    {
        public static T Padding<T>(this T control, ValueObservable<RectOffset> padding)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.padding.From(padding);
            return control;
        }

        public static T Spacing<T>(this T control, ValueObservable<float> spacing)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.spacing.From(spacing);
            return control;
        }

        public static T ChildAlignment<T>(this T control, ValueObservable<TextAnchor> childAlignment)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childAlignment.From(childAlignment);
            return control;
        }

        public static T ReverseArrangement<T>(this T control, ValueObservable<bool> reverseArrangement)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.reverseArrangement.From(reverseArrangement);
            return control;
        }

        public static T ChildForceExpandHeight<T>(this T control, ValueObservable<bool> childForceExpandHeight)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childForceExpandHeight.From(childForceExpandHeight);
            return control;
        }

        public static T ChildForceExpandWidth<T>(this T control, ValueObservable<bool> childForceExpandWidth)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childForceExpandWidth.From(childForceExpandWidth);
            return control;
        }

        public static T ChildControlWidth<T>(this T control, ValueObservable<bool> childControlWidth)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childControlWidth.From(childControlWidth);
            return control;
        }

        public static T ChildControlHeight<T>(this T control, ValueObservable<bool> childControlHeight)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childControlHeight.From(childControlHeight);
            return control;
        }

        public static T ChildScaleWidth<T>(this T control, ValueObservable<bool> childScaleWidth)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childScaleWidth.From(childScaleWidth);
            return control;
        }

        public static T ChildScaleHeight<T>(this T control, ValueObservable<bool> childScaleHeight)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childScaleHeight.From(childScaleHeight);
            return control;
        }

        public static T Padding<T>(this T control, RectOffset padding)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.padding.From(padding);
            return control;
        }

        public static T Spacing<T>(this T control, float spacing)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.spacing.From(spacing);
            return control;
        }

        public static T ChildAlignment<T>(this T control, TextAnchor childAlignment)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childAlignment.From(childAlignment);
            return control;
        }

        public static T ReverseArrangement<T>(this T control, bool reverseArrangement)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.reverseArrangement.From(reverseArrangement);
            return control;
        }

        public static T ChildForceExpandHeight<T>(this T control, bool childForceExpandHeight)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childForceExpandHeight.From(childForceExpandHeight);
            return control;
        }

        public static T ChildForceExpandWidth<T>(this T control, bool childForceExpandWidth)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childForceExpandWidth.From(childForceExpandWidth);
            return control;
        }

        public static T ChildControlWidth<T>(this T control, bool childControlWidth)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childControlWidth.From(childControlWidth);
            return control;
        }

        public static T ChildControlHeight<T>(this T control, bool childControlHeight)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childControlHeight.From(childControlHeight);
            return control;
        }

        public static T ChildScaleWidth<T>(this T control, bool childScaleWidth)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childScaleWidth.From(childScaleWidth);
            return control;
        }

        public static T ChildScaleHeight<T>(this T control, bool childScaleHeight)
            where T : IControl<ILayoutProps>
        {
            control.props.layout.childScaleHeight.From(childScaleHeight);
            return control;
        }
    }
}