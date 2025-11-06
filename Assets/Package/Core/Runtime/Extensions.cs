using System;
using ObserveThing;
using UnityEngine;

using static Nessle.UIBuilder;

namespace Nessle
{
    public interface IValueProps<T>
    {
        ValueObservable<T> value { get; }
    }

    public static class Extensions
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

        public static void BindValue<TProps, TValue>(this IControl<TProps> control, Func<TProps, ValueObservable<TValue>> accessValue, ValueObservable<TValue> bindTo)
        {
            var value = accessValue(control.props);
            control.AddBinding(
                bindTo.Subscribe(x => value.From(x.currentValue)),
                value.Subscribe(x => bindTo.From(x.currentValue))
            );
        }

        public static void BindValue<TProps, TValue, TSource>(this IControl<TProps> control, Func<TProps, ValueObservable<TValue>> accessValue, ValueObservable<TSource> bindTo, Func<TValue, TSource> toSource, Func<TSource, TValue> toValue)
        {
            var value = accessValue(control.props);
            control.AddBinding(
                bindTo.Subscribe(x => value.From(toValue(x.currentValue))),
                value.Subscribe(x => bindTo.From(toSource(x.currentValue)))
            );
        }

        public static void BindValue<TProps, TValue>(this IControl<TProps> control, ValueObservable<TValue> bindTo)
            where TProps : IValueProps<TValue>
        {
            control.AddBinding(
                bindTo.Subscribe(x => control.props.value.From(x.currentValue)),
                control.props.value.Subscribe(x => bindTo.From(x.currentValue))
            );
        }

        public static void BindValue<TProps, TValue, TSource>(this IControl<TProps> control, ValueObservable<TSource> bindTo, Func<TValue, TSource> toSource, Func<TSource, TValue> toValue)
            where TProps : IValueProps<TValue>
        {
            control.AddBinding(
                bindTo.Subscribe(x => control.props.value.From(toValue(x.currentValue))),
                control.props.value.Subscribe(x => bindTo.From(toSource(x.currentValue)))
            );
        }
    }
}
