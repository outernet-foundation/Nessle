using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public struct LayoutProps
    {
        public ElementProps element;
        public TransformProps transform;
        public IValueObservable<RectOffset> padding;
        public IValueObservable<float> spacing;
        public IValueObservable<TextAnchor> childAlignment;
        public IValueObservable<bool> reverseArrangement;
        public IValueObservable<bool> childForceExpandHeight;
        public IValueObservable<bool> childForceExpandWidth;
        public IValueObservable<bool> childControlWidth;
        public IValueObservable<bool> childControlHeight;
        public IValueObservable<bool> childScaleWidth;
        public IValueObservable<bool> childScaleHeight;
        public IListObservable<IControl> children;
    }

    [RequireComponent(typeof(HorizontalOrVerticalLayoutGroup))]
    public class LayoutControl : Control<LayoutProps>
    {
        private HorizontalOrVerticalLayoutGroup _layout;

        protected override void SetupInternal()
        {
            _layout = GetComponent<HorizontalOrVerticalLayoutGroup>();

            AddBinding(
                props.element.Subscribe(this),
                props.transform.Subscribe(this),
                props.padding?.Subscribe(x => _layout.padding = x.currentValue),
                props.spacing?.Subscribe(x => _layout.spacing = x.currentValue),
                props.childAlignment?.Subscribe(x => _layout.childAlignment = x.currentValue),
                props.reverseArrangement?.Subscribe(x => _layout.reverseArrangement = x.currentValue),
                props.childForceExpandHeight?.Subscribe(x => _layout.childForceExpandHeight = x.currentValue),
                props.childForceExpandWidth?.Subscribe(x => _layout.childForceExpandWidth = x.currentValue),
                props.childControlWidth?.Subscribe(x => _layout.childControlWidth = x.currentValue),
                props.childControlHeight?.Subscribe(x => _layout.childControlHeight = x.currentValue),
                props.childScaleWidth?.Subscribe(x => _layout.childScaleWidth = x.currentValue),
                props.childScaleHeight?.Subscribe(x => _layout.childScaleHeight = x.currentValue),
                props.children?.SubscribeAsChildren(rectTransform)
            );
        }
    }
}