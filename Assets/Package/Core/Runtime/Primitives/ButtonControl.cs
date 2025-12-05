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
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

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
        private Button _button;
        public PrimitiveControl<ImageProps> background;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => props?.onClick.value?.Invoke());
        }

        protected override void SetupInternal()
        {
            props.interactable.From(_button.interactable);

            if (background != null)
                background.Setup(props: props.background);

            AddBinding(props.interactable.Subscribe(x => _button.interactable = x.currentValue));
        }

        protected override void DisposeInternal()
        {
            background.Dispose();
        }
    }
}