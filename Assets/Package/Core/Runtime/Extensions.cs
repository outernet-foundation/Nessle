using System;
using ObserveThing;
using UnityEngine;

using static Nessle.UIBuilder;

namespace Nessle
{
    public static class Extensions
    {
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

        public static TControl Value<TControl, TProps, TValue>(this TControl control, Func<TProps, ValueObservable<TValue>> accessValue, ValueObservable<TValue> bindTo)
            where TControl : IControl<TProps>
        {
            var value = accessValue(control.props);
            control.AddBinding(
                bindTo.Subscribe(x => value.From(x.currentValue)),
                value.Subscribe(x => bindTo.From(x.currentValue))
            );
            return control;
        }

        public static TControl Value<TControl, TProps, TValue, TSource>(this TControl control, Func<TProps, ValueObservable<TValue>> accessValue, ValueObservable<TSource> bindTo, Func<TValue, TSource> toSource, Func<TSource, TValue> toValue)
            where TControl : IControl<TProps>
        {
            var value = accessValue(control.props);
            control.AddBinding(
                bindTo.Subscribe(x => value.From(toValue(x.currentValue))),
                value.Subscribe(x => bindTo.From(toSource(x.currentValue)))
            );
            return control;
        }
    }
}
