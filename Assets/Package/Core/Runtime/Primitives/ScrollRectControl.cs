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
        public TransformProps transform;
        public IValueObservable<Vector2> value;
        public IValueObservable<bool> horizontal;
        public IValueObservable<bool> vertical;
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
                props.transform.Subscribe(this),
                props.value?.Subscribe(x =>
                {
                    _value = x.currentValue;

                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.normalizedPosition = x.currentValue;
                }),
                props.horizontal?.Subscribe(x =>
                {
                    _horizontal = x.currentValue;

                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.horizontal = x.currentValue;
                }),
                props.vertical?.Subscribe(x =>
                {
                    _vertical = x.currentValue;

                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.vertical = x.currentValue;
                }),
                props.content?.Subscribe(x =>
                {
                    if (x.previousValue != null)
                        x.previousValue.rectTransform.parent = null;

                    if (x.currentValue == null)
                        return;

                    x.currentValue.rectTransform.SetParent(_scrollRect.viewport, false);
                    _scrollRect.content = x.currentValue.rectTransform;

                    _scrollRect.normalizedPosition = _value;
                    _scrollRect.horizontal = _horizontal;
                    _scrollRect.vertical = _vertical;
                })
            );
        }
    }
}