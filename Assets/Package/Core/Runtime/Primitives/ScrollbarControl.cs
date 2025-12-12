using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ScrollbarDirection = UnityEngine.UI.Scrollbar.Direction;

namespace Nessle
{
    public class ScrollbarProps : IDisposable
    {
        public ValueObservable<float> value { get; }
        public ValueObservable<ScrollbarDirection> direction { get; }
        public ValueObservable<float> size { get; }
        public ValueObservable<bool> interactable { get; }
        public ValueObservable<Action<float>> onValueChanged { get; }

        public ScrollbarProps() { }

        public ScrollbarProps(
            ValueObservable<float> value = default,
            ValueObservable<ScrollbarDirection> direction = default,
            ValueObservable<float> size = default,
            ValueObservable<bool> interactable = default,
            ValueObservable<Action<float>> onValueChanged = default
        )
        {
            this.value = value;
            this.direction = direction;
            this.size = size;
            this.interactable = interactable;
            this.onValueChanged = onValueChanged;
        }

        public void Dispose()
        {
            value?.Dispose();
            direction?.Dispose();
            size?.Dispose();
            interactable?.Dispose();
            onValueChanged?.Dispose();
        }
    }

    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarControl : PrimitiveControl<ScrollbarProps>
    {
        private Scrollbar _scrollbar;

        protected override void SetupInternal()
        {
            _scrollbar = GetComponent<Scrollbar>();
            _scrollbar.onValueChanged.AddListener(x => props?.onValueChanged?.value?.Invoke(x));

            AddBinding(
                props.value.Subscribe(x => _scrollbar.value = x.currentValue),
                props.direction.Subscribe(x => _scrollbar.direction = x.currentValue),
                props.size.Subscribe(x => _scrollbar.size = x.currentValue),
                props.interactable.Subscribe(x => _scrollbar.interactable = x.currentValue)
            );
        }
    }
}