using System;
using UnityEngine;

using ObserveThing;

using static Nessle.UIBuilder;

namespace Nessle
{
    public static class ButtonExtensions
    {
        public static T OnClick<T>(this T control, Action onclick)
            where T : IControl<ButtonProps>
        {
            control.props.onClick.From(onclick);
            return control;
        }

        public static T OnClick<T>(this T control, IValueObservable<Action> onclick)
            where T : IControl<ButtonProps>
        {
            control.props.onClick.From(onclick);
            return control;
        }

        public static T Label<T>(this T control, IValueObservable<string> label)
            where T : IControl<ButtonProps>
        {
            control.children.Add(
                Text("label")
                    .Value(label)
                    .Style(x =>
                    {
                        x.props.style.verticalAlignment.From(TMPro.VerticalAlignmentOptions.Capline);
                        x.props.style.horizontalAlignment.From(TMPro.HorizontalAlignmentOptions.Center);
                    })
            );

            return control;
        }

        public static T Label<T, U>(this T control, IValueObservable<U> label)
            where T : IControl<ButtonProps>
        {
            control.children.Add(
                Text("label")
                    .Value(label.SelectDynamic(x => x.ToString()))
                    .Style(x =>
                    {
                        x.props.style.verticalAlignment.From(TMPro.VerticalAlignmentOptions.Capline);
                        x.props.style.horizontalAlignment.From(TMPro.HorizontalAlignmentOptions.Center);
                    })
            );

            return control;
        }

        public static T Label<T>(this T control, string label)
            where T : IControl<ButtonProps>
        {
            control.children.Add(
                Text("label")
                    .Value(label)
                    .Style(x =>
                    {
                        x.props.style.verticalAlignment.From(TMPro.VerticalAlignmentOptions.Capline);
                        x.props.style.horizontalAlignment.From(TMPro.HorizontalAlignmentOptions.Center);
                    })
            );

            return control;
        }

        public static T Icon<T>(this T control, Sprite icon)
            where T : IControl<ButtonProps>
        {
            control.children.Add(Image("icon").Sprite(icon));
            return control;
        }

        public static T Icon<T>(this T control, IValueObservable<Sprite> icon)
            where T : IControl<ButtonProps>
        {
            control.children.Add(Image("icon").Sprite(icon));
            return control;
        }
    }
}
