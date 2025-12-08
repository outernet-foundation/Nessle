using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ButtonProps : IDisposable, IInteractableProps
    {
        public ImageProps background { get; } = new ImageProps();
        public ValueObservable<Action> onClick { get; } = new ValueObservable<Action>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>();

        public ButtonProps(
            ImageProps background = default,
            ValueObservable<Action> onClick = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.background = background ?? new ImageProps();
            this.onClick = onClick ?? new ValueObservable<Action>();
            this.interactable = interactable ?? new ValueObservable<bool>();
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
            if (background != null)
                background.Setup(props: props.background);

            AddBinding(props.interactable.Subscribe(x => _button.interactable = x.currentValue));
        }

        protected override void DisposeInternal()
        {
            background?.Dispose();
        }

        public override ButtonProps GetInstanceProps()
        {
            return new ButtonProps(
                background?.GetInstanceProps(),
                interactable: new ValueObservable<bool>(_button.interactable)
            );
        }
    }
}