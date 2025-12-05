using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ScrollbarDirection = UnityEngine.UI.Scrollbar.Direction;

namespace Nessle
{
    public class ScrollbarProps : IDisposable, IValueProps<float>, IInteractableProps
    {
        public ValueObservable<float> value { get; } = new ValueObservable<float>();
        public ValueObservable<ScrollbarDirection> direction { get; } = new ValueObservable<ScrollbarDirection>();
        public ValueObservable<float> size { get; } = new ValueObservable<float>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

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
    }
}