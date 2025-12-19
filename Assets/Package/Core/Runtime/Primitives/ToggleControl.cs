using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using UnityEngine.Events;

namespace Nessle
{
    public struct ToggleProps
    {
        public IValueObservable<bool> value;
        public IValueObservable<bool> interactable;
        public UnityAction<bool> onValueChanged;
    }

    [RequireComponent(typeof(Toggle))]
    public class ToggleControl : PrimitiveControl<ToggleProps>
    {
        private Toggle _toggle;

        protected override void SetupInternal()
        {
            _toggle = GetComponent<Toggle>();

            if (props.onValueChanged != null)
                _toggle.onValueChanged.AddListener(props.onValueChanged);

            AddBinding(
                props.value?.Subscribe(x => _toggle.isOn = x.currentValue),
                props.interactable?.Subscribe(x => _toggle.interactable = x.currentValue)
            );
        }
    }
}