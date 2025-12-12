using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class LayoutProps : IDisposable, ILayoutProps
    {
        public ValueObservable<RectOffset> padding { get; private set; }
        public ValueObservable<float> spacing { get; private set; }
        public ValueObservable<TextAnchor> childAlignment { get; private set; }
        public ValueObservable<bool> reverseArrangement { get; private set; }
        public ValueObservable<bool> childForceExpandHeight { get; private set; }
        public ValueObservable<bool> childForceExpandWidth { get; private set; }
        public ValueObservable<bool> childControlWidth { get; private set; }
        public ValueObservable<bool> childControlHeight { get; private set; }
        public ValueObservable<bool> childScaleWidth { get; private set; }
        public ValueObservable<bool> childScaleHeight { get; private set; }

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
            this.padding = padding;
            this.spacing = spacing;
            this.childAlignment = childAlignment;
            this.reverseArrangement = reverseArrangement;
            this.childForceExpandHeight = childForceExpandHeight;
            this.childForceExpandWidth = childForceExpandWidth;
            this.childControlWidth = childControlWidth;
            this.childControlHeight = childControlHeight;
            this.childScaleWidth = childScaleWidth;
            this.childScaleHeight = childScaleHeight;
        }

        public void CompleteWith(
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
            this.padding = this.padding ?? padding;
            this.spacing = this.spacing ?? spacing;
            this.childAlignment = this.childAlignment ?? childAlignment;
            this.reverseArrangement = this.reverseArrangement ?? reverseArrangement;
            this.childForceExpandHeight = this.childForceExpandHeight ?? childForceExpandHeight;
            this.childForceExpandWidth = this.childForceExpandWidth ?? childForceExpandWidth;
            this.childControlWidth = this.childControlWidth ?? childControlWidth;
            this.childControlHeight = this.childControlHeight ?? childControlHeight;
            this.childScaleWidth = this.childScaleWidth ?? childScaleWidth;
            this.childScaleHeight = this.childScaleHeight ?? childScaleHeight;
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

        protected override void SetupInternal()
        {
            _layout = GetComponent<HorizontalOrVerticalLayoutGroup>();

            props.CompleteWith(
                Props.From(_layout.padding),
                Props.From(_layout.spacing),
                Props.From(_layout.childAlignment),
                Props.From(_layout.reverseArrangement),
                Props.From(_layout.childForceExpandHeight),
                Props.From(_layout.childForceExpandWidth),
                Props.From(_layout.childControlWidth),
                Props.From(_layout.childControlHeight),
                Props.From(_layout.childScaleWidth),
                Props.From(_layout.childScaleHeight)
            );

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