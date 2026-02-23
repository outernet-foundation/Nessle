using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ScrollbarDirection = UnityEngine.UI.Scrollbar.Direction;
using UnityEngine.Events;

namespace Nessle
{
    public struct ScrollbarProps
    {
        public ElementProps element;
        public LayoutProps layout;
        public IValueObservable<float> value;
        public IValueObservable<ScrollbarDirection> direction;
        public IValueObservable<float> size;
        public IValueObservable<bool> interactable;
        public UnityAction<float> onValueChanged;
    }

    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarControl : Control<ScrollbarProps>
    {
        private Scrollbar _scrollbar;

        protected override void SetupInternal()
        {
            _scrollbar = GetComponent<Scrollbar>();

            if (props.onValueChanged != null)
                _scrollbar.onValueChanged.AddListener(props.onValueChanged);

            AddBinding(
                props.element.Subscribe(this),
                props.layout.Subscribe(this),
                props.value?.Subscribe(x => _scrollbar.value = x),
                props.direction?.Subscribe(x => _scrollbar.direction = x),
                props.size?.Subscribe(x => _scrollbar.size = x),
                props.interactable?.Subscribe(x => _scrollbar.interactable = x)
            );
        }
    }
}