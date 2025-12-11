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
        private PrimitiveControl<TextProps> _captionText;
        private PrimitiveControl<TextProps> _itemText;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.onValueChanged.AddListener(x => props?.value.From(x));

            _captionText = _dropdown.captionText.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>>();
            _itemText = _dropdown.itemText.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>>();
        }

        protected override void SetupInternal()
        {
            props.value = props.value ?? new ValueObservable<int>(_dropdown.value);
            props.allowMultiselect = props.allowMultiselect ?? new ValueObservable<bool>(_dropdown.MultiSelect);
            props.options = props.options ?? new ListObservable<string>(_dropdown.options.Select(x => x.text));
            props.interactable = props.interactable ?? new ValueObservable<bool>(_dropdown.interactable);
            props.captionTextStyle = props.captionTextStyle ?? new TextStyleProps();
            props.itemTextStyle = props.itemTextStyle ?? new TextStyleProps();

            _captionText.Setup(new TextProps() { style = props.captionTextStyle });
            _itemText.Setup(new TextProps() { style = props.itemTextStyle });

            AddBinding(
                props.value.Subscribe(x => _dropdown.value = x.currentValue),
                props.allowMultiselect.Subscribe(x => _dropdown.MultiSelect = x.currentValue),
                props.options.Subscribe(_ => _dropdown.options = props.options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                props.interactable.Subscribe(x => _dropdown.interactable = x.currentValue),
                _captionText,
                _itemText
            );
        }
    }
}