using System;
using System.Linq;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class DropdownProps : IDisposable, IValueProps<int>, IInteractableProps
    {
        public ValueObservable<int> value { get; private set; }
        public ValueObservable<bool> allowMultiselect { get; private set; }
        public ListObservable<string> options { get; private set; }
        public ValueObservable<bool> interactable { get; private set; }

        public TextStyleProps captionTextStyle { get; private set; }
        public TextStyleProps itemTextStyle { get; private set; }

        public DropdownProps(
            ValueObservable<int> value = default,
            ValueObservable<bool> allowMultiselect = default,
            ListObservable<string> options = default,
            ValueObservable<bool> interactable = default,
            TextStyleProps captionTextStyle = default,
            TextStyleProps itemTextStyle = default
        )
        {
            this.value = value;
            this.allowMultiselect = allowMultiselect;
            this.options = options;
            this.interactable = interactable;
            this.captionTextStyle = captionTextStyle;
            this.itemTextStyle = itemTextStyle;
        }

        public void CompleteWith(
            ValueObservable<int> value = default,
            ValueObservable<bool> allowMultiselect = default,
            ListObservable<string> options = default,
            ValueObservable<bool> interactable = default,
            TextStyleProps captionTextStyle = default,
            TextStyleProps itemTextStyle = default
        )
        {
            this.value = this.value ?? value;
            this.allowMultiselect = this.allowMultiselect ?? allowMultiselect;
            this.options = this.options ?? options;
            this.interactable = this.interactable ?? interactable;
            this.captionTextStyle = this.captionTextStyle ?? captionTextStyle;
            this.itemTextStyle = this.itemTextStyle ?? itemTextStyle;
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
            props.CompleteWith(
                Props.From(_dropdown.value),
                Props.From(_dropdown.MultiSelect),
                Props.From(_dropdown.options.Select(x => x.text)),
                Props.From(_dropdown.interactable),
                new TextStyleProps(),
                new TextStyleProps()
            );

            _captionText.Setup(new TextProps(style: props.captionTextStyle));
            _itemText.Setup(new TextProps(style: props.itemTextStyle));

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