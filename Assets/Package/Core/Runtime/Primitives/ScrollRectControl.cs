using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ScrollRectProps : IDisposable, IValueProps<Vector2>
    {
        public ValueObservable<Vector2> value { get; } = new ValueObservable<Vector2>(new Vector2(0, 1));
        public ValueObservable<bool> horizontal { get; } = new ValueObservable<bool>(true);
        public ValueObservable<bool> vertical { get; } = new ValueObservable<bool>(true);
        public ValueObservable<IControl> content { get; } = new ValueObservable<IControl>();

        public void PopulateFrom(ScrollRect scrollRect)
        {
            var contentObj = scrollRect.content;
            var contentControl = default(IControl);

            if (content != null)
            {
                contentControl = contentObj.GetComponent<IControl>();

                if (contentControl == null)
                    contentControl = UIBuilder.Control(contentObj.name, content);
            }

            value.From(scrollRect.normalizedPosition);
            horizontal.From(scrollRect.horizontal);
            vertical.From(scrollRect.vertical);
            content.From(contentControl);
        }

        public IDisposable BindTo(ScrollRect scrollRect, IControl containingControl)
        {
            return new ComposedDisposable(
                value.Subscribe(x =>
                {
                    if (scrollRect.content == null)
                        return;

                    scrollRect.normalizedPosition = x.currentValue;
                }),

                horizontal.Subscribe(x =>
                {
                    if (scrollRect.content == null)
                        return;

                    scrollRect.horizontal = x.currentValue;
                }),

                vertical.Subscribe(x =>
                {
                    if (scrollRect.content == null)
                        return;

                    scrollRect.vertical = x.currentValue;
                }),

                content.Subscribe(x =>
                {
                    x.previousValue?.parent.From(default(IControl));

                    if (x.currentValue == null)
                        return;

                    x.currentValue.parent.From(containingControl);
                    scrollRect.content = x.currentValue.rectTransform;
                    x.currentValue.SetPivot(new Vector2(0, 1));
                    x.currentValue.AnchorToTop();

                    scrollRect.normalizedPosition = value.value;
                    scrollRect.horizontal = horizontal.value;
                    scrollRect.vertical = vertical.value;
                })
            );
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
            AddBinding(props.BindTo(_scrollRect, this));
        }

        protected override ScrollRectProps GetDefaultProps()
        {
            var props = new ScrollRectProps();
            props.PopulateFrom(_scrollRect);
            return props;
        }
    }
}