using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ToggleProps : IDisposable
    {
        public ValueObservable<bool> value { get; }
        public ValueObservable<bool> interactable { get; }
        public ValueObservable<Action<bool>> onValueChanged { get; }

        public ToggleProps() { }

        public ToggleProps(
            ValueObservable<bool> value = default,
            ValueObservable<bool> interactable = default,
            ValueObservable<Action<bool>> onValueChanged = default
        )
        {
            this.value = value;
            this.interactable = interactable;
            this.onValueChanged = onValueChanged;
        }

        public void Dispose()
        {
            value?.Dispose();
            interactable?.Dispose();
            onValueChanged?.Dispose();
        }
    }

    [RequireComponent(typeof(Toggle))]
    public class ToggleControl : PrimitiveControl<ToggleProps>
    {
        private Toggle _toggle;

        protected override void SetupInternal()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(x => props?.onValueChanged?.value?.Invoke(x));

            AddBinding(
                props.value.Subscribe(x => _toggle.isOn = x.currentValue),
                props.interactable.Subscribe(x => _toggle.interactable = x.currentValue)
            );
        }
    }
}