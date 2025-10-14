using System;
using ObserveThing;
using ObserveThing.StatefulExtensions;
using FofX.Stateful;
using static Nessle.UIBuilder;

namespace Nessle
{
    public static class ScrollbarExtensions
    {
        public static T BindValue<T>(this T control, ObservablePrimitive<float> bindTo)
            where T : IControl<ScrollbarProps>
        {
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => control.props.value.From(x.currentValue)),
                control.props.value.Subscribe(x => bindTo.ExecuteSetOrDelay(x.currentValue))
            );

            return control;
        }

        public static TControl BindValue<TControl, TValue>(this TControl control, ObservablePrimitive<TValue> bindTo, Func<float, TValue> toState, Func<TValue, float> toControl)
            where TControl : IControl<ScrollbarProps>
        {
            control.AddBinding(
                bindTo.AsObservable().Subscribe(x => control.props.value.From(toControl(x.currentValue))),
                control.props.value.Subscribe(x => bindTo.ExecuteSetOrDelay(toState(x.currentValue)))
            );

            return control;
        }
    }
}