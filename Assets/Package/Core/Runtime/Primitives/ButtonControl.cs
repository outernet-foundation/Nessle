using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ButtonProps : IDisposable
    {
        public ImageProps background { get; }
        public ValueObservable<Action> onClick { get; }
        public ValueObservable<bool> interactable { get; }

        public ButtonProps() { }

        public ButtonProps(
            ImageProps background = default,
            ValueObservable<Action> onClick = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.background = background;
            this.onClick = onClick;
            this.interactable = interactable;
        }

        public void Dispose()
        {
            background?.Dispose();
            onClick?.Dispose();
            interactable?.Dispose();
        }
    }

    [RequireComponent(typeof(Button))]
    public class ButtonControl : PrimitiveControl<ButtonProps>
    {
        public PrimitiveControl<ImageProps> background;
        private Button _button;

        protected override void SetupInternal()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => props?.onClick?.value?.Invoke());

            background.Setup(props.background);

            AddBinding(
                props.interactable?.Subscribe(x => _button.interactable = x.currentValue),
                background
            );
        }
    }
}