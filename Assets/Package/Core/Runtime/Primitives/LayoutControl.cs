using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class LayoutProps : IDisposable, ILayoutProps
    {
        public ValueObservable<RectOffset> padding { get; } = new ValueObservable<RectOffset>(new RectOffset());
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
            AddBinding(Utility.BindLayout(props, _layout, true));
        }
    }
}