using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using UnityEngine.Events;

namespace Nessle
{
    public struct ScrollRectProps
    {
        public ElementProps element;
        public LayoutProps layout;
        public IValueObservable<Vector2> value;
        public IValueObservable<bool> horizontal;
        public IValueObservable<bool> vertical;
        public IValueObservable<ScrollRect.MovementType> movementType;
        public IValueObservable<float> elasticity;
        public IValueObservable<bool> inertia;
        public IValueObservable<float> decelerationRate;
        public IValueObservable<float> scrollSensitivity;
        public IValueObservable<ScrollRect.ScrollbarVisibility> verticalScrollbarVisibility;
        public IValueObservable<ScrollRect.ScrollbarVisibility> horizontalScrollbarVisibility;
        public IValueObservable<IControl> content;
        public UnityAction<Vector2> onValueChanged;
    }

    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectControl : Control<ScrollRectProps>
    {
        private ScrollRect _scrollRect;

        private Vector2 _value;
        private bool _horizontal;
        private bool _vertical;

        protected override void SetupInternal()
        {
            _scrollRect = GetComponent<ScrollRect>();

            if (props.onValueChanged != null)
                _scrollRect.onValueChanged.AddListener(props.onValueChanged);

            AddBinding(
                props.element.Subscribe(this),
                props.layout.Subscribe(this),
                props.movementType?.Subscribe(x => _scrollRect.movementType = x),
                props.elasticity?.Subscribe(x => _scrollRect.elasticity = x),
                props.inertia?.Subscribe(x => _scrollRect.inertia = x),
                props.decelerationRate?.Subscribe(x => _scrollRect.decelerationRate = x),
                props.scrollSensitivity?.Subscribe(x => _scrollRect.scrollSensitivity = x),
                props.verticalScrollbarVisibility?.Subscribe(x => _scrollRect.verticalScrollbarVisibility = x),
                props.horizontalScrollbarVisibility?.Subscribe(x => _scrollRect.horizontalScrollbarVisibility = x),
                props.value?.Subscribe(x =>
                {
                    _value = x;

                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.normalizedPosition = x;
                }),
                props.horizontal?.Subscribe(x =>
                {
                    _horizontal = x;

                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.horizontal = x;
                }),
                props.vertical?.Subscribe(x =>
                {
                    _vertical = x;

                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.vertical = x;
                }),
                props.content?
                    .ObservableWithPrevious()
                    .Subscribe(x =>
                    {
                        if (x.previous != null)
                            x.previous.rectTransform.parent = null;

                        if (x.current == null)
                            return;

                        x.current.rectTransform.SetParent(_scrollRect.viewport, false);
                        _scrollRect.content = x.current.rectTransform;

                        _scrollRect.normalizedPosition = _value;
                        _scrollRect.horizontal = _horizontal;
                        _scrollRect.vertical = _vertical;
                    })
            );
        }
    }
}