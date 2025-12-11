using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ButtonProps : IDisposable, IInteractableProps
    {
        public ImageProps background { get; private set; }
        public ValueObservable<Action> onClick { get; private set; }
        public ValueObservable<bool> interactable { get; private set; }

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

        public void CompleteWith(
            ImageProps background = default,
            ValueObservable<Action> onClick = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.background = this.background ?? background;
            this.onClick = this.onClick ?? onClick;
            this.interactable = this.interactable ?? interactable;
        }

        public void Dispose()
        {
            background.Dispose();
            onClick.Dispose();
            interactable.Dispose();
        }
    }

    [RequireComponent(typeof(Button))]
    public class ButtonControl : PrimitiveControl<ButtonProps>
    {
        public PrimitiveControl<ImageProps> background;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => props?.onClick.value?.Invoke());
        }

        protected override void SetupInternal()
        {
            props.CompleteWith(
                new ImageProps(),
                new ValueObservable<Action>(),
                Props.From(_button.interactable)
            );

            background.Setup(props.background);

            AddBinding(
                props.interactable.Subscribe(x => _button.interactable = x.currentValue),
                background
            );
        }
    }
}