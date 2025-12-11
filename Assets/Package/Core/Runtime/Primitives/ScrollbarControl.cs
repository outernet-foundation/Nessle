using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ScrollbarDirection = UnityEngine.UI.Scrollbar.Direction;

namespace Nessle
{
    public class ScrollbarProps : IDisposable, IValueProps<float>, IInteractableProps
    {
        public ValueObservable<float> value { get; private set; }
        public ValueObservable<ScrollbarDirection> direction { get; private set; }
        public ValueObservable<float> size { get; private set; }
        public ValueObservable<bool> interactable { get; private set; }

        public ScrollbarProps(
            ValueObservable<float> value = default,
            ValueObservable<ScrollbarDirection> direction = default,
            ValueObservable<float> size = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = value;
            this.direction = direction;
            this.size = size;
            this.interactable = interactable;
        }

        public void CompleteWith(
            ValueObservable<float> value = default,
            ValueObservable<ScrollbarDirection> direction = default,
            ValueObservable<float> size = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = this.value ?? value;
            this.direction = this.direction ?? direction;
            this.size = this.size ?? size;
            this.interactable = this.interactable ?? interactable;
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
            props.CompleteWith(
                Props.From(_scrollbar.value),
                Props.From(_scrollbar.direction),
                Props.From(_scrollbar.size),
                Props.From(_scrollbar.interactable)
            );

            AddBinding(
                props.value.Subscribe(x => _scrollbar.value = x.currentValue),
                props.direction.Subscribe(x => _scrollbar.direction = x.currentValue),
                props.size.Subscribe(x => _scrollbar.size = x.currentValue),
                props.interactable.Subscribe(x => _scrollbar.interactable = x.currentValue)
            );
        }
    }
}