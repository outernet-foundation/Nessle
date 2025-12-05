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
            props.value.From(_dropdown.value);
            props.allowMultiselect.From(_dropdown.MultiSelect);
            props.options.From(_dropdown.options.Select(x => x.text));
            props.interactable.From(_dropdown.interactable);
            Utility.CopyFromText(props.captionTextStyle, _dropdown.captionText);
            Utility.CopyFromText(props.itemTextStyle, _dropdown.itemText);

            AddBinding(
                props.value.Subscribe(x => _dropdown.value = x.currentValue),
                props.allowMultiselect.Subscribe(x => _dropdown.MultiSelect = x.currentValue),
                props.options.Subscribe(_ => _dropdown.options = props.options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                props.interactable.Subscribe(x => _dropdown.interactable = x.currentValue),
                Utility.BindTextStyle(props.captionTextStyle, _dropdown.captionText, true),
                Utility.BindTextStyle(props.itemTextStyle, _dropdown.itemText, true)
            );
        }
    }
}