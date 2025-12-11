using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ScrollRectProps : IDisposable, IValueProps<Vector2>
    {
        public ValueObservable<Vector2> value { get; private set; }
        public ValueObservable<bool> horizontal { get; private set; }
        public ValueObservable<bool> vertical { get; private set; }
        public ValueObservable<IControl> content { get; private set; }

        public ScrollRectProps(
            ValueObservable<Vector2> value = default,
            ValueObservable<bool> horizontal = default,
            ValueObservable<bool> vertical = default,
            ValueObservable<IControl> content = default
        )
        {
            this.value = value;
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.content = content;
        }

        public void CompleteWith(
            ValueObservable<Vector2> value = default,
            ValueObservable<bool> horizontal = default,
            ValueObservable<bool> vertical = default,
            ValueObservable<IControl> content = default
        )
        {
            this.value = this.value ?? value;
            this.horizontal = this.horizontal ?? horizontal;
            this.vertical = this.vertical ?? vertical;
            this.content = this.content ?? content;
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
            var content = _scrollRect.content;
            var contentControl = default(IControl);

            if (content != null)
                contentControl = content.GetComponent<IControl>();

            props.CompleteWith(
                Props.From(_scrollRect.normalizedPosition),
                Props.From(_scrollRect.horizontal),
                Props.From(_scrollRect.vertical),
                Props.From(contentControl)
            );

            AddBinding(
                props.value.Subscribe(x =>
                {
                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.normalizedPosition = x.currentValue;
                }),
                props.horizontal.Subscribe(x =>
                {
                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.horizontal = x.currentValue;
                }),
                props.vertical.Subscribe(x =>
                {
                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.vertical = x.currentValue;
                }),
                props.content.Subscribe(x =>
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
                })
            );
        }
    }
}