using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ScrollRectProps : IDisposable, IValueProps<Vector2>
    {
        public ValueObservable<Vector2> value { get; }
        public ValueObservable<bool> horizontal { get; }
        public ValueObservable<bool> vertical { get; }
        public ValueObservable<IControl> content { get; }

        public ScrollRectProps(
            ValueObservable<Vector2> value = default,
            ValueObservable<bool> horizontal = default,
            ValueObservable<bool> vertical = default,
            ValueObservable<IControl> content = default
        )
        {
            this.value = value ?? new ValueObservable<Vector2>(new Vector2(0, 1));
            this.horizontal = horizontal ?? new ValueObservable<bool>(true);
            this.vertical = vertical ?? new ValueObservable<bool>(true);
            this.content = content ?? new ValueObservable<IControl>();
        }

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
                x.currentValue.SetPivot(new Vector2(0, 1));
                x.currentValue.AnchorToTop();

                _scrollRect.normalizedPosition = props.value.value;
                _scrollRect.horizontal = props.horizontal.value;
                _scrollRect.vertical = props.vertical.value;
            }));
        }

        public override ScrollRectProps GetInstanceProps()
        {
            return new ScrollRectProps(
                new ValueObservable<Vector2>(_scrollRect.normalizedPosition),
                new ValueObservable<bool>(_scrollRect.horizontal),
                new ValueObservable<bool>(_scrollRect.vertical),
                new ValueObservable<IControl>(_scrollRect.content?.GetComponent<IControl>())
            );
        }
    }
}