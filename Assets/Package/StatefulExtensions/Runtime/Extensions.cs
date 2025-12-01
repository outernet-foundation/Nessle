using System;
using ObserveThing;
using ObserveThing.StatefulExtensions;
using FofX.Stateful;

namespace Nessle.StatefulExtensions
{
    public static class Extensions
    {
        public static void Value<TProps, TValue>(this IControl<TProps> control, Func<TProps, ValueObservable<TValue>> accessValue, ObservablePrimitive<TValue> bindTo)
        {
            var value = accessValue(control.props);
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => value.From(x.currentValue)),
                value.Subscribe(x => bindTo.ExecuteSetOrDelay(x.currentValue))
            );
        }

        public static void Value<TProps, TValue, TSource>(this IControl<TProps> control, Func<TProps, ValueObservable<TValue>> accessValue, ObservablePrimitive<TSource> bindTo, Func<TValue, TSource> toSource, Func<TSource, TValue> toValue)
        {
            var value = accessValue(control.props);
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => value.From(toValue(x.currentValue))),
                value.Subscribe(x => bindTo.ExecuteSetOrDelay(toSource(x.currentValue)))
            );
        }
    }
}