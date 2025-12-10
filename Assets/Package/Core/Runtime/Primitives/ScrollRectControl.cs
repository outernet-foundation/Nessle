using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ScrollRectProps : IDisposable, IValueProps<Vector2>
    {
        public ValueObservable<Vector2> value { get; set; }
        public ValueObservable<bool> horizontal { get; set; }
        public ValueObservable<bool> vertical { get; set; }
        public ValueObservable<IControl> content { get; set; }

        public void Dispose()
        {
            value.Dispose();
            horizontal.Dispose();
            vertical.Dispose();
            content.Dispose();
        }
    }

    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectControl : PrimitiveControl<ScrollRectProps>
    {
        private ScrollRect _scrollRect;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect.onValueChanged.AddListener(x => props?.value.From(x));
            _childParentOverride = _scrollRect.viewport;
        }

        protected override void SetupInternal()
        {
            var content = _scrollRect.content;
            var contentControl = default(IControl);

            if (content != null)
                contentControl = content.GetComponent<IControl>();

            props.value = props.value ?? new ValueObservable<Vector2>(_scrollRect.normalizedPosition);
            props.horizontal = props.horizontal ?? new ValueObservable<bool>(_scrollRect.horizontal);
            props.vertical = props.vertical ?? new ValueObservable<bool>(_scrollRect.vertical);
            props.content = props.content ?? new ValueObservable<IControl>(contentControl);

            AddBinding(props.value.Subscribe(x =>
            {
                if (_scrollRect.content == null)
                    return;

                _scrollRect.normalizedPosition = x.currentValue;
            }));

            AddBinding(props.horizontal.Subscribe(x =>
            {
                if (_scrollRect.content == null)
                    return;

                _scrollRect.horizontal = x.currentValue;
            }));

            AddBinding(props.vertical.Subscribe(x =>
            {
                if (_scrollRect.content == null)
                    return;

                _scrollRect.vertical = x.currentValue;
            }));

            AddBinding(props.content.Subscribe(x =>
            {
                x.previousValue?.parent.From(default(IControl));

                if (x.currentValue == null)
                    return;

                x.currentValue.parent.From(this);
                _scrollRect.content = x.currentValue.rectTransform;

                x.currentValue.rectTransform.pivot = new Vector2(0, 1);
                x.currentValue.rectTransform.anchorMin = new Vector2(x.currentValue.rectTransform.anchorMin.x, 1);
                x.currentValue.rectTransform.anchorMax = new Vector2(x.currentValue.rectTransform.anchorMax.x, 1);

                _scrollRect.normalizedPosition = props.value.value;
                _scrollRect.horizontal = props.horizontal.value;
                _scrollRect.vertical = props.vertical.value;
            }));
        }
    }
}