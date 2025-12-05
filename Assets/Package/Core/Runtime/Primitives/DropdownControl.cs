using System;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class DropdownProps : IDisposable, IValueProps<int>, IInteractableProps
    {
        public ValueObservable<int> value { get; } = new ValueObservable<int>();
        public ValueObservable<bool> allowMultiselect { get; } = new ValueObservable<bool>();
        public ListObservable<string> options { get; } = new ListObservable<string>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

        public TextStyleProps captionTextStyle { get; } = new TextStyleProps();
        public TextStyleProps itemTextStyle { get; } = new TextStyleProps();

        public void Dispose()
        {
            value.Dispose();
            allowMultiselect.Dispose();
            options.Dispose();
            interactable.Dispose();
            captionTextStyle.Dispose();
            itemTextStyle.Dispose();
        }
    }

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownControl : PrimitiveControl<DropdownProps>
    {
        private TMP_Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.onValueChanged.AddListener(x => props?.value.From(x));
        }

        protected override void SetupInternal()
        {
            AddBinding(Utility.BindDropdown(props, _dropdown, true));
        }
    }
}