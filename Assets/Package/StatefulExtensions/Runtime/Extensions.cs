using System;
using ObserveThing;
using ObserveThing.StatefulExtensions;
using FofX.Stateful;

namespace Nessle.StatefulExtensions
{
    public static class Extensions
    {
        public static TControl Value<TControl, TValue>(this TControl control, Func<TControl, ValueObservable<TValue>> accessValue, ObservablePrimitive<TValue> bindTo)
            where TControl : IControl
        {
            var value = accessValue(control);
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => value.From(x.currentValue)),
                value.Subscribe(x => bindTo.ExecuteSetOrDelay(x.currentValue))
            );

            return control;
        }

        public static TControl Value<TControl, TValue, TSource>(this TControl control, Func<TControl, ValueObservable<TValue>> accessValue, ObservablePrimitive<TSource> bindTo, Func<TValue, TSource> toSource, Func<TSource, TValue> toValue)
            where TControl : IControl
        {
            var value = accessValue(control);
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => value.From(toValue(x.currentValue))),
                value.Subscribe(x => bindTo.ExecuteSetOrDelay(toSource(x.currentValue)))
            );

            return control;
        }
    }
}