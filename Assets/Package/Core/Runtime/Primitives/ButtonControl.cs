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
        public IValueObservable<IControl> content;
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
                props.interactable?.Subscribe(x => _button.interactable = x),
                background,
                props.content?.ObservableWithPrevious().Subscribe(x =>
                {
                    if (x.previous != null)
                        x.previous.transform.SetParent(null);

                    if (x.current != null)
                        x.current.transform.SetParent(transform, false);
                })
            );
        }
    }
}