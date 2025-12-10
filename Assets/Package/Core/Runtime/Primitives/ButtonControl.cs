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

        public void PopulateFrom(Button button)
        {
            interactable.From(button.interactable);
        }

        public IDisposable BindTo(Button button)
        {
            return interactable.Subscribe(x => button.interactable = x.currentValue);
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
        public Image background;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => props?.onClick.value?.Invoke());
        }

        protected override void SetupInternal()
        {
            AddBinding(props.BindTo(_button));

            if (background != null)
                AddBinding(props.background.BindTo(background));
        }

        protected override ButtonProps GetDefaultProps()
        {
            var props = new ButtonProps();
            props.PopulateFrom(_button);

            if (background != null)
                props.background.PopulateFrom(background);

            return props;
        }
    }
}