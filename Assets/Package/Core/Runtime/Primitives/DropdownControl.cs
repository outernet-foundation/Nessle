using System;
using System.Linq;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class DropdownProps : IDisposable
    {
        public ValueObservable<int> value { get; }
        public ValueObservable<bool> allowMultiselect { get; }
        public ListObservable<string> options { get; }
        public ValueObservable<bool> interactable { get; }

        public TextStyleProps captionTextStyle { get; }
        public TextStyleProps itemTextStyle { get; }

        public ValueObservable<Action<int>> onValueChanged { get; }

        public DropdownProps() { }

        public DropdownProps(
            ValueObservable<int> value = default,
            ValueObservable<bool> allowMultiselect = default,
            ListObservable<string> options = default,
            ValueObservable<bool> interactable = default,
            TextStyleProps captionTextStyle = default,
            TextStyleProps itemTextStyle = default,
            ValueObservable<Action<int>> onValueChanged = default
        )
        {
            this.value = value;
            this.allowMultiselect = allowMultiselect;
            this.options = options;
            this.interactable = interactable;
            this.captionTextStyle = captionTextStyle;
            this.itemTextStyle = itemTextStyle;
            this.onValueChanged = onValueChanged;
        }

        public void Dispose()
        {
            value?.Dispose();
            allowMultiselect?.Dispose();
            options?.Dispose();
            interactable?.Dispose();
            captionTextStyle?.Dispose();
            itemTextStyle?.Dispose();
            onValueChanged?.Dispose();
        }
    }

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownControl : PrimitiveControl<DropdownProps>
    {
        private TMP_Dropdown _dropdown;

        private PrimitiveControl<TextProps> _captionText;
        private PrimitiveControl<TextProps> _itemText;

        protected override void SetupInternal()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.onValueChanged.AddListener(x => props?.onValueChanged?.value?.Invoke(x));

            _captionText = _dropdown.captionText.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>, TextControl>();
            _itemText = _dropdown.itemText.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>, TextControl>();

            _captionText.Setup(new TextProps(style: props.captionTextStyle));
            _itemText.Setup(new TextProps(style: props.itemTextStyle));

            AddBinding(
                props.value?.Subscribe(x => _dropdown.value = x.currentValue),
                props.allowMultiselect?.Subscribe(x => _dropdown.MultiSelect = x.currentValue),
                props.options?.Subscribe(_ => _dropdown.options = props.options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                props.interactable?.Subscribe(x => _dropdown.interactable = x.currentValue),
                _captionText,
                _itemText
            );
        }
    }
}