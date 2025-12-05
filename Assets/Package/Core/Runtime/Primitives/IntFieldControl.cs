using System;
using UnityEngine;
using TMPro;
using ObserveThing;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;

namespace Nessle
{
    public class IntFieldProps : IDisposable, IValueProps<int>, IInteractableProps
    {
        public ValueObservable<int> value { get; } = new ValueObservable<int>();
        public TextStyleProps inputTextStyle { get; } = new TextStyleProps();
        public TextProps placeholderText { get; } = new TextProps();
        public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

        public void Dispose()
        {
            value.Dispose();
            inputTextStyle.Dispose();
            placeholderText.Dispose();
            readOnly.Dispose();
            interactable.Dispose();
        }
    }

    [RequireComponent(typeof(TMP_InputField))]
    public class IntFieldControl : PrimitiveControl<IntFieldProps>
    {
        private TMP_InputField _inputField;

        protected override void SetupInternal()
        {
            _inputField.enabled = false;
            _inputField.enabled = true;
            _inputField.contentType = TMP_ContentType.DecimalNumber;
            _inputField.onEndEdit.AddListener(x => props.value.From(int.TryParse(x, out var value) ? value : 0));

            AddBinding(
                props.value.Subscribe(x => _inputField.text = x.currentValue.ToString()),
                Utility.BindTextStyle(props.inputTextStyle, _inputField.textComponent, true),
                props.readOnly.Subscribe(x => _inputField.readOnly = x.currentValue),
                props.interactable.Subscribe(x => _inputField.interactable = x.currentValue)
            );

            if (_inputField.placeholder != null && _inputField.placeholder is TMP_Text placeholder)
            {
                AddBinding(
                    props.placeholderText.value.Subscribe(x => placeholder.text = x.currentValue),
                    Utility.BindTextStyle(props.placeholderText.style, placeholder, true)
                );
            }
        }
    }
}