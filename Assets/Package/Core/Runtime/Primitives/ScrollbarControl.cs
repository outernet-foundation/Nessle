using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ScrollbarDirection = UnityEngine.UI.Scrollbar.Direction;

namespace Nessle
{
    public class ScrollbarProps : IDisposable, IValueProps<float>, IInteractableProps
    {
        public ValueObservable<float> value { get; }
        public ValueObservable<ScrollbarDirection> direction { get; }
        public ValueObservable<float> size { get; }
        public ValueObservable<bool> interactable { get; }

        public ScrollbarProps(
            ValueObservable<float> value = default,
            ValueObservable<ScrollbarDirection> direction = default,
            ValueObservable<float> size = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = value ?? new ValueObservable<float>();
            this.direction = direction ?? new ValueObservable<ScrollbarDirection>();
            this.size = size ?? new ValueObservable<float>();
            this.interactable = interactable ?? new ValueObservable<bool>(true);
        }

        public void Dispose()
        {
            value.Dispose();
            direction.Dispose();
            size.Dispose();
            interactable.Dispose();
        }
    }

    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarControl : PrimitiveControl<ScrollbarProps>
    {
        private Scrollbar _scrollbar;

        private void Awake()
        {
            _scrollbar = GetComponent<Scrollbar>();
            _scrollbar.onValueChanged.AddListener(x => props?.value.From(x));
        }

        protected override void SetupInternal()
        {
            AddBinding(
                props.value.Subscribe(x => _scrollbar.value = x.currentValue),
                props.direction.Subscribe(x => _scrollbar.direction = x.currentValue),
                props.size.Subscribe(x => _scrollbar.size = x.currentValue),
                props.interactable.Subscribe(x => _scrollbar.interactable = x.currentValue)
            );
        }

        public override ScrollbarProps GetInstanceProps()
        {
            return new ScrollbarProps(
                new ValueObservable<float>(_scrollbar.value),
                new ValueObservable<ScrollbarDirection>(_scrollbar.direction),
                new ValueObservable<float>(_scrollbar.size),
                new ValueObservable<bool>(_scrollbar.interactable)
            );
        }
    }
}