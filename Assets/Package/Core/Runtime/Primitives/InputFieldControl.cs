using System;
using UnityEngine;
using ObserveThing;
using TMPro;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;
using TMP_LineType = TMPro.TMP_InputField.LineType;

namespace Nessle
{
    public class InputFieldProps : IDisposable, IValueProps<string>, IInteractableProps
    {
        public TextProps inputText { get; } = new TextProps();
        public TextProps placeholderText { get; } = new TextProps();
        public ValueObservable<TMP_ContentType> contentType { get; } = new ValueObservable<TMP_ContentType>();
        public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
        public ValueObservable<TMP_LineType> lineType { get; } = new ValueObservable<TMP_LineType>();
        public ValueObservable<int> characterLimit { get; } = new ValueObservable<int>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);
        public ValueObservable<Action<string>> onEndEdit { get; } = new ValueObservable<Action<string>>();

        ValueObservable<string> IValueProps<string>.value => inputText.value;

        public void Dispose()
        {
            inputText.Dispose();
            placeholderText.Dispose();
            contentType.Dispose();
            readOnly.Dispose();
            lineType.Dispose();
            characterLimit.Dispose();
            interactable.Dispose();
            onEndEdit.Dispose();
        }
    }

    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldControl : PrimitiveControl<InputFieldProps>
    {
        private TMP_InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
        }

        protected override void SetupInternal()
        {
            _inputField.enabled = false;
            _inputField.enabled = true;

            _inputField.onValueChanged.AddListener(x => props.inputText.value.From(x));
            _inputField.onEndEdit.AddListener(x => props.onEndEdit.value?.Invoke(x));

            AddBinding(
                props.inputText.value.Subscribe(x => _inputField.text = x.currentValue),
                Utility.BindTextStyle(props.inputText.style, _inputField.textComponent, true),
                props.contentType.Subscribe(x => _inputField.contentType = x.currentValue),
                props.readOnly.Subscribe(x => _inputField.readOnly = x.currentValue),
                props.lineType.Subscribe(x => _inputField.lineType = x.currentValue),
                props.characterLimit.Subscribe(x => _inputField.characterLimit = x.currentValue),
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