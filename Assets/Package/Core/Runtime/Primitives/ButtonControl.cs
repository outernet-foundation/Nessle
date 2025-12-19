using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using UnityEngine.Events;

namespace Nessle
{
    public struct ButtonProps
    {
        public ImageProps background;
        public IValueObservable<bool> interactable;
        public UnityAction onClick;
    }

    [RequireComponent(typeof(Button))]
    public class ButtonControl : PrimitiveControl<ButtonProps>
    {
        public PrimitiveControl<ImageProps> background;
        private Button _button;

        protected override void SetupInternal()
        {
            _button = GetComponent<Button>();

            if (props.onClick != null)
                _button.onClick.AddListener(props.onClick);

            background.Setup(props.background);

            AddBinding(
                props.interactable?.Subscribe(x => _button.interactable = x.currentValue),
                background
            );
        }
    }
}