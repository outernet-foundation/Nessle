using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class LayoutProps : IDisposable, ILayoutProps
    {
        public ValueObservable<RectOffset> padding { get; }
        public ValueObservable<float> spacing { get; }
        public ValueObservable<TextAnchor> childAlignment { get; }
        public ValueObservable<bool> reverseArrangement { get; }
        public ValueObservable<bool> childForceExpandHeight { get; }
        public ValueObservable<bool> childForceExpandWidth { get; }
        public ValueObservable<bool> childControlWidth { get; }
        public ValueObservable<bool> childControlHeight { get; }
        public ValueObservable<bool> childScaleWidth { get; }
        public ValueObservable<bool> childScaleHeight { get; }

        LayoutProps ILayoutProps.layout => this;

        public LayoutProps(
            ValueObservable<RectOffset> padding = default,
            ValueObservable<float> spacing = default,
            ValueObservable<TextAnchor> childAlignment = default,
            ValueObservable<bool> reverseArrangement = default,
            ValueObservable<bool> childForceExpandHeight = default,
            ValueObservable<bool> childForceExpandWidth = default,
            ValueObservable<bool> childControlWidth = default,
            ValueObservable<bool> childControlHeight = default,
            ValueObservable<bool> childScaleWidth = default,
            ValueObservable<bool> childScaleHeight = default
        )
        {
            this.padding = padding ?? new ValueObservable<RectOffset>();
            this.spacing = spacing ?? new ValueObservable<float>();
            this.childAlignment = childAlignment ?? new ValueObservable<TextAnchor>();
            this.reverseArrangement = reverseArrangement ?? new ValueObservable<bool>();
            this.childForceExpandHeight = childForceExpandHeight ?? new ValueObservable<bool>();
            this.childForceExpandWidth = childForceExpandWidth ?? new ValueObservable<bool>();
            this.childControlWidth = childControlWidth ?? new ValueObservable<bool>();
            this.childControlHeight = childControlHeight ?? new ValueObservable<bool>();
            this.childScaleWidth = childScaleWidth ?? new ValueObservable<bool>();
            this.childScaleHeight = childScaleHeight ?? new ValueObservable<bool>();
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
            AddBinding(
                props.padding.Subscribe(x => _layout.padding = x.currentValue),
                props.spacing.Subscribe(x => _layout.spacing = x.currentValue),
                props.childAlignment.Subscribe(x => _layout.childAlignment = x.currentValue),
                props.reverseArrangement.Subscribe(x => _layout.reverseArrangement = x.currentValue),
                props.childForceExpandHeight.Subscribe(x => _layout.childForceExpandHeight = x.currentValue),
                props.childForceExpandWidth.Subscribe(x => _layout.childForceExpandWidth = x.currentValue),
                props.childControlWidth.Subscribe(x => _layout.childControlWidth = x.currentValue),
                props.childControlHeight.Subscribe(x => _layout.childControlHeight = x.currentValue),
                props.childScaleWidth.Subscribe(x => _layout.childScaleWidth = x.currentValue),
                props.childScaleHeight.Subscribe(x => _layout.childScaleHeight = x.currentValue)
            );
        }

        public override LayoutProps GetInstanceProps()
        {
            return new LayoutProps(
                new ValueObservable<RectOffset>(_layout.padding),
                new ValueObservable<float>(_layout.spacing),
                new ValueObservable<TextAnchor>(_layout.childAlignment),
                new ValueObservable<bool>(_layout.reverseArrangement),
                new ValueObservable<bool>(_layout.childForceExpandHeight),
                new ValueObservable<bool>(_layout.childForceExpandWidth),
                new ValueObservable<bool>(_layout.childControlWidth),
                new ValueObservable<bool>(_layout.childControlHeight),
                new ValueObservable<bool>(_layout.childScaleWidth),
                new ValueObservable<bool>(_layout.childScaleHeight)
            );
        }
    }
}