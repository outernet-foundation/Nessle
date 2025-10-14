using System;
using ObserveThing;
using UnityEngine;

using static Nessle.UIBuilder;

namespace Nessle
{
    public static class ButtonExtensions
    {
        public static void LabelFrom<T>(this T control, IValueObservable<string> label)
            where T : IControl<ButtonProps>
        {
            control.Children(Text("label").Setup(x => x.props.text.From(label)));
        }

        public static void LabelFrom<T, U>(this T control, IValueObservable<U> label)
            where T : IControl<ButtonProps>
        {
            control.Children(Text("label").Setup(x => x.props.text.From(label)));
        }

        public static void LabelFrom<T>(this T control, string label)
            where T : IControl<ButtonProps>
        {
            control.Children(Text("label").Setup(x => x.props.text.From(label)));
        }

        public static void IconFrom<T>(this T control, Sprite icon)
            where T : IControl<ButtonProps>
        {
            control.Children(Image("icon").Setup(x => x.props.sprite.From(icon)));
        }

        public static void IconFrom<T>(this T control, IValueObservable<Sprite> icon)
            where T : IControl<ButtonProps>
        {
            control.Children(Image("icon").Setup(x => x.props.sprite.From(icon)));
        }
    }
}
