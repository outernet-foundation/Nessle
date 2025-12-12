using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ScrollRectProps : IDisposable
    {
        public ValueObservable<Vector2> value { get; }
        public ValueObservable<bool> horizontal { get; }
        public ValueObservable<bool> vertical { get; }
        public ValueObservable<IControl> content { get; }
        public ValueObservable<Action<Vector2>> onValueChanged { get; }

        public ScrollRectProps() { }

        public ScrollRectProps(
            ValueObservable<Vector2> value = default,
            ValueObservable<bool> horizontal = default,
            ValueObservable<bool> vertical = default,
            ValueObservable<IControl> content = default,
            ValueObservable<Action<Vector2>> onValueChanged = default
        )
        {
            this.value = value;
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.content = content;
            this.onValueChanged = onValueChanged;
        }

        public void Dispose()
        {
            value?.Dispose();
            horizontal?.Dispose();
            vertical?.Dispose();
            content?.Dispose();
            onValueChanged?.Dispose();
        }
    }

    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectControl : PrimitiveControl<ScrollRectProps>
    {
        private ScrollRect _scrollRect;

        protected override void SetupInternal()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect.onValueChanged.AddListener(x => props?.onValueChanged?.value?.Invoke(x));

            AddBinding(
                props.value?.Subscribe(x =>
                {
                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.normalizedPosition = x.currentValue;
                }),
                props.horizontal?.Subscribe(x =>
                {
                    if (_scrollRect.content == null)
                        return;

                    _scrollRect.horizontal = x.currentValue;
                }),
                props.vertical?.Subscribe(x =>
                {
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

                    _scrollRect.normalizedPosition = props.value.value;
                    _scrollRect.horizontal = props.horizontal.value;
                    _scrollRect.vertical = props.vertical.value;

                    _childParentOverride = x.currentValue.rectTransform;
                })
            );
        }
    }
}