using System;
using System.Linq;
using UnityEngine;
using ObserveThing;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Nessle
{
    public struct DropdownProps
    {
        public ElementProps element;
        public TransformProps transform;
        public IValueObservable<int> value;
        public IValueObservable<bool> allowMultiselect;
        public IListObservable<string> options;
        public IValueObservable<bool> interactable;

        public TextStyleProps captionTextStyle;
        public TextStyleProps itemTextStyle;

        public UnityAction<int> onValueChanged;
    }

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownControl : Control<DropdownProps>
    {
        private TMP_Dropdown _dropdown;

        private Control<TextProps> _captionText;
        private Control<TextProps> _itemText;

        private List<string> _options = new List<string>();

        protected override void SetupInternal()
        {
            _dropdown = GetComponent<TMP_Dropdown>();

            if (props.onValueChanged != null)
                _dropdown.onValueChanged.AddListener(props.onValueChanged);

            _captionText = _dropdown.captionText.gameObject.GetOrAddComponent<Control<TextProps>, TextControl>();
            _itemText = _dropdown.itemText.gameObject.GetOrAddComponent<Control<TextProps>, TextControl>();

            _captionText.Setup(new TextProps() { style = props.captionTextStyle });
            _itemText.Setup(new TextProps() { style = props.itemTextStyle });

            AddBinding(
                props.element.Subscribe(this),
                props.transform.Subscribe(this),
                props.value?.Subscribe(x => _dropdown.value = x.currentValue),
                props.allowMultiselect?.Subscribe(x => _dropdown.MultiSelect = x.currentValue),
                props.options?.Subscribe(x =>
                {
                    if (x.operationType == OpType.Add)
                    {
                        _options.Add(x.element);
                    }
                    else if (x.operationType == OpType.Remove)
                    {
                        _options.Remove(x.element);
                    }

                    _dropdown.ClearOptions();
                    _dropdown.AddOptions(_options);
                }),
                props.interactable?.Subscribe(x => _dropdown.interactable = x.currentValue),
                _captionText,
                _itemText
            );
        }
    }
}