using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ToggleProps : IDisposable, IValueProps<bool>, IInteractableProps
    {
        public ValueObservable<bool> value { get; set; }
        public ValueObservable<bool> interactable { get; set; }

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
            props.value = props.value ?? new ValueObservable<bool>(_toggle.isOn);
            props.interactable = props.interactable ?? new ValueObservable<bool>(_toggle.interactable);

            AddBinding(
                props.value.Subscribe(x => _toggle.isOn = x.currentValue),
                props.interactable.Subscribe(x => _toggle.interactable = x.currentValue)
            );
        }
    }
}