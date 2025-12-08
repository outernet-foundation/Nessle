using System;
using System.Linq;
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

        public DropdownProps(
            ValueObservable<int> value = default,
            ValueObservable<bool> allowMultiselect = default,
            ListObservable<string> options = default,
            ValueObservable<bool> interactable = default,
            TextStyleProps captionTextStyle = default,
            TextStyleProps itemTextStyle = default
        )
        {
            this.value = value ?? new ValueObservable<int>();
            this.allowMultiselect = allowMultiselect ?? new ValueObservable<bool>();
            this.options = options ?? new ListObservable<string>();
            this.interactable = interactable ?? new ValueObservable<bool>();
            this.captionTextStyle = captionTextStyle ?? new TextStyleProps();
            this.itemTextStyle = itemTextStyle ?? new TextStyleProps();
        }

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
            AddBinding(
                props.value.Subscribe(x => _dropdown.value = x.currentValue),
                props.allowMultiselect.Subscribe(x => _dropdown.MultiSelect = x.currentValue),
                props.options.Subscribe(_ => _dropdown.options = props.options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                props.interactable.Subscribe(x => _dropdown.interactable = x.currentValue),
                Utility.BindTextStyle(props.captionTextStyle, _dropdown.captionText),
                Utility.BindTextStyle(props.itemTextStyle, _dropdown.itemText)
            );
        }

        public override DropdownProps GetInstanceProps()
        {
            return new DropdownProps(
                new ValueObservable<int>(_dropdown.value),
                new ValueObservable<bool>(_dropdown.MultiSelect),
                new ListObservable<string>(_dropdown.options.Select(x => x.text)),
                new ValueObservable<bool>(_dropdown.interactable),
                Utility.StylePropsFromText(_dropdown.captionText),
                Utility.StylePropsFromText(_dropdown.itemText)
            );
        }
    }
}