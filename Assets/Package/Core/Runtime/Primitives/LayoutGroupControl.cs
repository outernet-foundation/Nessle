using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public struct LayoutGroupProps
    {
        public ElementProps element;
        public LayoutProps layout;
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
    public class LayoutGroupControl : Control<LayoutGroupProps>
    {
        private HorizontalOrVerticalLayoutGroup _layout;

        protected override void SetupInternal()
        {
            _layout = GetComponent<HorizontalOrVerticalLayoutGroup>();

            AddBinding(
                props.element.Subscribe(this),
                props.layout.Subscribe(this),
                props.padding?.Subscribe(x => _layout.padding = x),
                props.spacing?.Subscribe(x => _layout.spacing = x),
                props.childAlignment?.Subscribe(x => _layout.childAlignment = x),
                props.reverseArrangement?.Subscribe(x => _layout.reverseArrangement = x),
                props.childForceExpandHeight?.Subscribe(x => _layout.childForceExpandHeight = x),
                props.childForceExpandWidth?.Subscribe(x => _layout.childForceExpandWidth = x),
                props.childControlWidth?.Subscribe(x => _layout.childControlWidth = x),
                props.childControlHeight?.Subscribe(x => _layout.childControlHeight = x),
                props.childScaleWidth?.Subscribe(x => _layout.childScaleWidth = x),
                props.childScaleHeight?.Subscribe(x => _layout.childScaleHeight = x),
                props.children?.SubscribeAsChildren(rectTransform)
            );
        }
    }
}