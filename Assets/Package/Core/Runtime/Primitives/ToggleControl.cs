using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ToggleProps : IDisposable, IValueProps<bool>, IInteractableProps
    {
        public ValueObservable<bool> value { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>();

        public void Dispose()
        {
            value.Dispose();
            interactable.Dispose();
        }

        public IDisposable BindTo(Toggle toggle)
        {
            return new ComposedDisposable(
                value.Subscribe(x => toggle.isOn = x.currentValue),
                interactable.Subscribe(x => toggle.interactable = x.currentValue)
            );
        }

        public void PopulateFrom(Toggle toggle)
        {
            value.From(toggle.isOn);
            interactable.From(toggle.interactable);
        }
    }

    [RequireComponent(typeof(Toggle))]
    public class ToggleControl : PrimitiveControl<ToggleProps>
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(x => props?.value.From(x));
        }

        protected override void SetupInternal()
        {
            AddBinding(props.BindTo(_toggle));
        }

        protected override ToggleProps GetDefaultProps()
        {
            var props = new ToggleProps();
            props.PopulateFrom(_toggle);
            return props;
        }
    }
}