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

        public void PopulateFrom(TMP_Dropdown dropdown)
        {
            value.From(dropdown.value);
            allowMultiselect.From(dropdown.MultiSelect);
            options.From(dropdown.options.Select(x => x.text));
            interactable.From(dropdown.interactable);
            itemTextStyle.PopulateFrom(dropdown.itemText);
            captionTextStyle.PopulateFrom(dropdown.captionText);
        }

        public IDisposable BindTo(TMP_Dropdown dropdown)
        {
            return new ComposedDisposable(
                value.Subscribe(x => dropdown.value = x.currentValue),
                allowMultiselect.Subscribe(x => dropdown.MultiSelect = x.currentValue),
                options.Subscribe(_ => dropdown.options = options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                interactable.Subscribe(x => dropdown.interactable = x.currentValue),
                captionTextStyle.BindTo(dropdown.captionText),
                itemTextStyle.BindTo(dropdown.itemText)
            );
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
            AddBinding(props.BindTo(_dropdown));
        }

        protected override DropdownProps GetDefaultProps()
        {
            var props = new DropdownProps();
            props.PopulateFrom(_dropdown);
            return props;
        }
    }
}