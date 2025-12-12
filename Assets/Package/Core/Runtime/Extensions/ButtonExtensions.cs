using System;
using UnityEngine;

using ObserveThing;

using static Nessle.UIBuilder;

namespace Nessle
{
    public static class ButtonExtensions
    {
        public static T WithLabel<T>(this T control, ValueObservable<string> label)
            where T : IControl<ButtonProps>
        {
            return control.WithLabel(
                new TextProps(
                    value: label,
                    style: new TextStyleProps(
                        verticalAlignment: Props.From(TMPro.VerticalAlignmentOptions.Capline),
                        horizontalAlignment: Props.From(TMPro.HorizontalAlignmentOptions.Center)
                    )
                )
            );
        }

        public static T WithLabel<T>(this T control, TextProps label)
            where T : IControl<ButtonProps>
        {
            control.children.Add(Text("label", label));
            return control;
        }

        public static T WithIcon<T>(this T control, ValueObservable<Sprite> icon)
            where T : IControl<ButtonProps>
        {
            control.WithIcon(new ImageProps(sprite: icon));
            return control;
        }

        public static T WithIcon<T>(this T control, ImageProps icon)
            where T : IControl<ButtonProps>
        {
            control.children.Add(Image("icon", icon));
            return control;
        }
    }
}
