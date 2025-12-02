using System;
using UnityEngine;
using ObserveThing;

namespace Nessle
{
    public interface IValueProps<T>
    {
        ValueObservable<T> value { get; }
    }

    public interface IInteractableProps
    {
        ValueObservable<bool> interactable { get; }
    }

    public interface IColorProps
    {
        ValueObservable<Color> color { get; }
    }

    public static class InterfaceExtensions
    {
        public static TControl Value<TControl, TValue>(this TControl control, TValue value)
            where TControl : IControl<IValueProps<TValue>>
        {
            control.props.value.From(value);
            return control;
        }

        public static TControl Value<TControl, TValue>(this TControl control, IValueObservable<TValue> value)
            where TControl : IControl<IValueProps<TValue>>
        {
            control.props.value.From(value);
            return control;
        }

        public static TControl Value<TControl, TValue>(this TControl control, ValueObservable<TValue> bindTo)
            where TControl : IControl<IValueProps<TValue>>
        {
            control.AddBinding(
                bindTo.Subscribe(x => control.props.value.From(x.currentValue)),
                control.props.value.Subscribe(x => bindTo.From(x.currentValue))
            );

            return control;
        }

        public static TControl Value<TControl, TValue, TSource>(this TControl control, ValueObservable<TSource> bindTo, Func<TValue, TSource> toSource, Func<TSource, TValue> toValue)
            where TControl : IControl<IValueProps<TValue>>
        {
            control.AddBinding(
                bindTo.Subscribe(x => control.props.value.From(toValue(x.currentValue))),
                control.props.value.Subscribe(x => bindTo.From(toSource(x.currentValue)))
            );

            return control;
        }

        public static T Interactable<T>(this T control, bool interactable)
            where T : IControl<IInteractableProps>
        {
            control.props.interactable.From(interactable);
            return control;
        }

        public static T Interactable<T>(this T control, IValueObservable<bool> interactable)
            where T : IControl<IInteractableProps>
        {
            control.props.interactable.From(interactable);
            return control;
        }

        public static T Color<T>(this T control, Color color)
            where T : IControl<IColorProps>
        {
            control.props.color.From(color);
            return control;
        }

        public static T Color<T>(this T control, IValueObservable<Color> color)
            where T : IControl<IColorProps>
        {
            control.props.color.From(color);
            return control;
        }
    }
}
