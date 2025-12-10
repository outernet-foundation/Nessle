using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class LayoutProps : IDisposable, ILayoutProps
    {
        public ValueObservable<RectOffset> padding { get; set; }
        public ValueObservable<float> spacing { get; set; }
        public ValueObservable<TextAnchor> childAlignment { get; set; }
        public ValueObservable<bool> reverseArrangement { get; set; }
        public ValueObservable<bool> childForceExpandHeight { get; set; }
        public ValueObservable<bool> childForceExpandWidth { get; set; }
        public ValueObservable<bool> childControlWidth { get; set; }
        public ValueObservable<bool> childControlHeight { get; set; }
        public ValueObservable<bool> childScaleWidth { get; set; }
        public ValueObservable<bool> childScaleHeight { get; set; }

        LayoutProps ILayoutProps.layout => this;

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
            props.padding = props.padding ?? new ValueObservable<RectOffset>(_layout.padding);
            props.spacing = props.spacing ?? new ValueObservable<float>(_layout.spacing);
            props.childAlignment = props.childAlignment ?? new ValueObservable<TextAnchor>(_layout.childAlignment);
            props.reverseArrangement = props.reverseArrangement ?? new ValueObservable<bool>(_layout.reverseArrangement);
            props.childForceExpandHeight = props.childForceExpandHeight ?? new ValueObservable<bool>(_layout.childForceExpandHeight);
            props.childForceExpandWidth = props.childForceExpandWidth ?? new ValueObservable<bool>(_layout.childForceExpandWidth);
            props.childControlWidth = props.childControlWidth ?? new ValueObservable<bool>(_layout.childControlWidth);
            props.childControlHeight = props.childControlHeight ?? new ValueObservable<bool>(_layout.childControlHeight);
            props.childScaleWidth = props.childScaleWidth ?? new ValueObservable<bool>(_layout.childScaleWidth);
            props.childScaleHeight = props.childScaleHeight ?? new ValueObservable<bool>(_layout.childScaleHeight);

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
    }
}