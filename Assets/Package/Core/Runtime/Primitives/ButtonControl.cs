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

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => props?.onClick.value?.Invoke());
        }

        protected override void SetupInternal()
        {
            AddBinding(props.interactable.Subscribe(x => _button.interactable = x.currentValue));
        }
    }
}