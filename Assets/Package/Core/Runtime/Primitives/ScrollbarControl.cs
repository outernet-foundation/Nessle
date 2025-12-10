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

        public void PopulateFrom(Scrollbar scrollbar)
        {
            value.From(scrollbar.value);
            direction.From(scrollbar.direction);
            size.From(scrollbar.size);
            interactable.From(scrollbar.interactable);
        }

        public IDisposable BindTo(Scrollbar scrollbar)
        {
            return new ComposedDisposable(
                value.Subscribe(x => scrollbar.value = x.currentValue),
                direction.Subscribe(x => scrollbar.direction = x.currentValue),
                size.Subscribe(x => scrollbar.size = x.currentValue),
                interactable.Subscribe(x => scrollbar.interactable = x.currentValue)
            );
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
            AddBinding(props.BindTo(_scrollbar));
        }

        protected override ScrollbarProps GetDefaultProps()
        {
            var props = new ScrollbarProps();
            props.PopulateFrom(_scrollbar);
            return props;
        }
    }
}