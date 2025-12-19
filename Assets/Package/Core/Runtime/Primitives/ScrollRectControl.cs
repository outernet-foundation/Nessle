using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using UnityEngine.Events;

namespace Nessle
{
    public struct ScrollRectProps
    {
        public IValueObservable<Vector2> value;
        public IValueObservable<bool> horizontal;
        public IValueObservable<bool> vertical;
        public IValueObservable<IControl> content;
        public UnityAction<Vector2> onValueChanged;
    }

    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectControl : PrimitiveControl<ScrollRectProps>
    {
        private ScrollRect _scrollRect;

        private Vector2 _value;
        private bool _horizontal;
        private bool _vertical;

        protected override void SetupInternal()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _childParentOverride = _scrollRect.viewport;

            if (props.onValueChanged != null)
                _scrollRect.onValueChanged.AddListener(props.onValueChanged);

            AddBinding(
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
                    x.previousValue?.parent.From(default(IControl));

                    if (x.currentValue == null)
                        return;

                    x.currentValue.parent.From(this);
                    _scrollRect.content = x.currentValue.rectTransform;
                    x.currentValue.SetPivot(new Vector2(0, 1));
                    x.currentValue.AnchorToTop();

                    _scrollRect.normalizedPosition = _value;
                    _scrollRect.horizontal = _horizontal;
                    _scrollRect.vertical = _vertical;
                })
            );
        }
    }
}