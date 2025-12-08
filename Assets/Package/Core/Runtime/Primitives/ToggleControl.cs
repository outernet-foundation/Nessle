using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ToggleProps : IDisposable, IValueProps<bool>, IInteractableProps
    {
        public ValueObservable<bool> value { get; }
        public ValueObservable<bool> interactable { get; }

        public ToggleProps(
            ValueObservable<bool> value = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = value ?? new ValueObservable<bool>();
            this.interactable = interactable ?? new ValueObservable<bool>();
        }

        public void Dispose()
        {
            value.Dispose();
            interactable.Dispose();
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
            props.value.From(_toggle.isOn);
            props.interactable.From(_toggle.interactable);

            AddBinding(
                props.value.Subscribe(x => _toggle.isOn = x.currentValue),
                props.interactable.Subscribe(x => _toggle.interactable = x.currentValue)
            );
        }

        public override ToggleProps GetInstanceProps()
        {
            return new ToggleProps(
                new ValueObservable<bool>(_toggle.isOn),
                new ValueObservable<bool>(_toggle.interactable)
            );
        }
    }
}