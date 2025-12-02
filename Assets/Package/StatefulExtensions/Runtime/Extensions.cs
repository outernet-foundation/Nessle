using System;
using ObserveThing;
using ObserveThing.StatefulExtensions;
using FofX.Stateful;

namespace Nessle.StatefulExtensions
{
    public static class Extensions
    {
        public static TControl Value<TControl, TValue>(this TControl control, ObservablePrimitive<TValue> bindTo)
            where TControl : IControl<IValueProps<TValue>>
        {
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => control.props.value.From(x.currentValue)),
                control.props.value.Subscribe(x => bindTo.ExecuteSetOrDelay(x.currentValue))
            );

            return control;
        }

        public static TControl Value<TControl, TValue, TSource>(this TControl control, ObservablePrimitive<TSource> bindTo, Func<TValue, TSource> toSource, Func<TSource, TValue> toValue)
            where TControl : IControl<IValueProps<TValue>>
        {
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => control.props.value.From(toValue(x.currentValue))),
                control.props.value.Subscribe(x => bindTo.ExecuteSetOrDelay(toSource(x.currentValue)))
            );

            return control;
        }
    }
}