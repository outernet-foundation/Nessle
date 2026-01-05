using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using UnityEngine.Events;

namespace Nessle
{
    public struct ButtonProps
    {
        public ElementProps element;
        public LayoutProps layout;
        public ImageProps background;
        public IValueObservable<bool> interactable;
        public UnityAction onClick;
        public IListObservable<IControl> content;
    }

    [RequireComponent(typeof(Button))]
    public class ButtonControl : Control<ButtonProps>
    {
        public Control<ImageProps> background;
        public RectTransform childParent;
        private Button _button;

        protected override void SetupInternal()
        {
            _button = GetComponent<Button>();

            if (props.onClick != null)
                _button.onClick.AddListener(props.onClick);

            background.Setup(props.background);

            AddBinding(
                props.element.Subscribe(this),
                props.layout.Subscribe(this),
                props.interactable?.Subscribe(x => _button.interactable = x.currentValue),
                background,
                props.content?.SubscribeAsChildren(childParent)
            );
        }
    }
}