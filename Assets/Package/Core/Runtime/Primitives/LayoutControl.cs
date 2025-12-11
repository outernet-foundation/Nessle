using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class LayoutProps : IDisposable, ILayoutProps
    {
        public ValueObservable<RectOffset> padding { get; } = new ValueObservable<RectOffset>();
        public ValueObservable<float> spacing { get; } = new ValueObservable<float>();
        public ValueObservable<TextAnchor> childAlignment { get; } = new ValueObservable<TextAnchor>();
        public ValueObservable<bool> reverseArrangement { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> childForceExpandHeight { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> childForceExpandWidth { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> childControlWidth { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> childControlHeight { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> childScaleWidth { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> childScaleHeight { get; } = new ValueObservable<bool>();

        LayoutProps ILayoutProps.layout => this;

        public void PopulateFrom(HorizontalOrVerticalLayoutGroup layout)
        {
            padding.From(layout.padding);
            spacing.From(layout.spacing);
            childAlignment.From(layout.childAlignment);
            reverseArrangement.From(layout.reverseArrangement);
            childForceExpandHeight.From(layout.childForceExpandHeight);
            childForceExpandWidth.From(layout.childForceExpandWidth);
            childControlWidth.From(layout.childControlWidth);
            childControlHeight.From(layout.childControlHeight);
            childScaleWidth.From(layout.childScaleWidth);
            childScaleHeight.From(layout.childScaleHeight);
        }

        public IDisposable BindTo(HorizontalOrVerticalLayoutGroup layout)
        {
            return new ComposedDisposable(
                padding.Subscribe(x => layout.padding = x.currentValue),
                spacing.Subscribe(x => layout.spacing = x.currentValue),
                childAlignment.Subscribe(x => layout.childAlignment = x.currentValue),
                reverseArrangement.Subscribe(x => layout.reverseArrangement = x.currentValue),
                childForceExpandHeight.Subscribe(x => layout.childForceExpandHeight = x.currentValue),
                childForceExpandWidth.Subscribe(x => layout.childForceExpandWidth = x.currentValue),
                childControlWidth.Subscribe(x => layout.childControlWidth = x.currentValue),
                childControlHeight.Subscribe(x => layout.childControlHeight = x.currentValue),
                childScaleWidth.Subscribe(x => layout.childScaleWidth = x.currentValue),
                childScaleHeight.Subscribe(x => layout.childScaleHeight = x.currentValue)
            );
        }

        public void Dispose()
        {
            padding.Dispose();
            spacing.Dispose();
            childAlignment.Dispose();
            reverseArrangement.Dispose();
            childForceExpandHeight.Dispose();
            childForceExpandWidth.Dispose();
            childControlWidth.Dispose();
            childControlHeight.Dispose();
            childScaleWidth.Dispose();
            childScaleHeight.Dispose();
        }
    }

    [RequireComponent(typeof(HorizontalOrVerticalLayoutGroup))]
    public class LayoutControl : PrimitiveControl<LayoutProps>
    {
        private HorizontalOrVerticalLayoutGroup _layout;

        private void Awake()
        {
            _layout = GetComponent<HorizontalOrVerticalLayoutGroup>();
        }

        protected override void SetupInternal()
        {
            AddBinding(props.BindTo(_layout));
        }

        protected override LayoutProps CompleteProps()
        {
            var props = new LayoutProps();
            props.PopulateFrom(_layout);
            return props;
        }
    }
}