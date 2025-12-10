using System;
using System.Linq;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class DropdownProps : IDisposable, IValueProps<int>, IInteractableProps
    {
        public ValueObservable<int> value { get; set; }
        public ValueObservable<bool> allowMultiselect { get; set; }
        public ListObservable<string> options { get; set; }
        public ValueObservable<bool> interactable { get; set; }

        public TextStyleProps captionTextStyle { get; set; }
        public TextStyleProps itemTextStyle { get; set; }

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
            props.value = props.value ?? new ValueObservable<int>(_dropdown.value);
            props.allowMultiselect = props.allowMultiselect ?? new ValueObservable<bool>(_dropdown.MultiSelect);
            props.options = props.options ?? new ListObservable<string>(_dropdown.options.Select(x => x.text));
            props.interactable = props.interactable ?? new ValueObservable<bool>(_dropdown.interactable);

            Utility.CompleteProps(_dropdown.captionText, props.captionTextStyle);
            Utility.CompleteProps(_dropdown.itemText, props.itemTextStyle);

            AddBinding(
                props.value.Subscribe(x => _dropdown.value = x.currentValue),
                props.allowMultiselect.Subscribe(x => _dropdown.MultiSelect = x.currentValue),
                props.options.Subscribe(_ => _dropdown.options = props.options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                props.interactable.Subscribe(x => _dropdown.interactable = x.currentValue),
                Utility.BindTextStyle(props.captionTextStyle, _dropdown.captionText),
                Utility.BindTextStyle(props.itemTextStyle, _dropdown.itemText)
            );
        }
    }
}