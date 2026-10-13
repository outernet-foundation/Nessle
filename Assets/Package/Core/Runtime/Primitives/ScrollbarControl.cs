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
        public IValueObservable<float> value;
        public IValueObservable<ScrollbarDirection> direction;
        public IValueObservable<float> size;
        public IValueObservable<bool> interactable;
        public UnityAction<float> onValueChanged;

        public ImageProps handle;
        public ImageProps background;
    }

    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarControl : Control<ScrollbarProps>
    {
        public ImageControl background;
        public ImageControl handle;

        private Scrollbar _scrollbar;

        protected override void SetupInternal()
        {
            _scrollbar = GetComponent<Scrollbar>();

            if (props.onValueChanged != null)
                _scrollbar.onValueChanged.AddListener(props.onValueChanged);

            if (background != null)
                background.Setup(props.background);

            if (handle != null)
                handle.Setup(props.handle);

            AddBinding(
                background,
                handle,
                props.value?.Subscribe(x => _scrollbar.value = x),
                props.direction?.Subscribe(x => _scrollbar.direction = x),
                props.size?.Subscribe(x => _scrollbar.size = x),
                props.interactable?.Subscribe(x => _scrollbar.interactable = x)
            );
        }
    }
}