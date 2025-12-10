using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ButtonProps : IDisposable, IInteractableProps
    {
        public ImageProps background { get; set; }
        public ValueObservable<Action> onClick { get; set; }
        public ValueObservable<bool> interactable { get; set; }

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
            props.interactable = props.interactable ?? new ValueObservable<bool>(_button.interactable);
            props.onClick = props.onClick ?? new ValueObservable<Action>();

            if (background != null)
                background.Setup(props: props.background);

            AddBinding(props.interactable.Subscribe(x => _button.interactable = x.currentValue));
        }

        protected override void DisposeInternal()
        {
            background?.Dispose();
        }
    }
}