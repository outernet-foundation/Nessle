using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;

namespace Nessle
{
    public class ToggleProps : IDisposable, IValueProps<bool>, IInteractableProps
    {
        public ValueObservable<bool> value { get; private set; }
        public ValueObservable<bool> interactable { get; private set; }

        public ToggleProps(
            ValueObservable<bool> value = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = value;
            this.interactable = interactable;
        }

        public void CompleteWith(
            ValueObservable<bool> value = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = this.value ?? value;
            this.interactable = this.interactable ?? interactable;
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

        protected override void SetupInternal()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(x => props?.value.From(x));

            props.CompleteWith(
                Props.From(_toggle.isOn),
                Props.From(_toggle.interactable)
            );

            AddBinding(
                props.value.Subscribe(x => _toggle.isOn = x.currentValue),
                props.interactable.Subscribe(x => _toggle.interactable = x.currentValue)
            );
        }
    }
}