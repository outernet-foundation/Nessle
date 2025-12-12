using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using TMPro;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;
using TMP_LineType = TMPro.TMP_InputField.LineType;

namespace Nessle
{
    public class InputFieldProps : IDisposable
    {
        public ValueObservable<string> value => inputText.value;
        public TextProps inputText { get; }
        public TextProps placeholderText { get; }
        public ValueObservable<TMP_ContentType> contentType { get; }
        public ValueObservable<bool> readOnly { get; }
        public ValueObservable<TMP_LineType> lineType { get; }
        public ValueObservable<int> characterLimit { get; }
        public ValueObservable<bool> interactable { get; }
        public ValueObservable<Action<string>> onValueChanged { get; }
        public ValueObservable<Action<string>> onEndEdit { get; }
        public ImageProps background { get; }

        public InputFieldProps() { }

        public InputFieldProps(
            TextProps inputText = default,
            TextProps placeholderText = default,
            ValueObservable<TMP_ContentType> contentType = default,
            ValueObservable<bool> readOnly = default,
            ValueObservable<TMP_LineType> lineType = default,
            ValueObservable<int> characterLimit = default,
            ValueObservable<bool> interactable = default,
            ValueObservable<Action<string>> onValueChanged = default,
            ValueObservable<Action<string>> onEndEdit = default,
            ImageProps background = default
        )
        {
            this.inputText = inputText;
            this.placeholderText = placeholderText;
            this.contentType = contentType;
            this.readOnly = readOnly;
            this.lineType = lineType;
            this.characterLimit = characterLimit;
            this.interactable = interactable;
            this.onValueChanged = onValueChanged;
            this.onEndEdit = onEndEdit;
            this.background = background;
        }

        public void Dispose()
        {
            inputText?.Dispose();
            placeholderText?.Dispose();
            contentType?.Dispose();
            readOnly?.Dispose();
            lineType?.Dispose();
            characterLimit?.Dispose();
            interactable?.Dispose();
            onEndEdit?.Dispose();
            onValueChanged?.Dispose();
            background?.Dispose();
        }
    }

    public class InputFieldProps<T> : IDisposable
    {
        public ValueObservable<T> value { get; }
        public InputFieldProps inputField { get; }

        public InputFieldProps() { }

        public InputFieldProps(
            ValueObservable<T> value = default,
            InputFieldProps inputField = default
        )
        {
            this.value = value;
            this.inputField = inputField;
        }

        public void Dispose()
        {
            value?.Dispose();
            inputField?.Dispose();
        }
    }

    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldControl : PrimitiveControl<InputFieldProps>
    {
        public Image background;
        private TMP_InputField _inputField;

        private PrimitiveControl<TextProps> _inputText;
        private PrimitiveControl<TextProps> _placeholderText;
        private PrimitiveControl<ImageProps> _background;

        protected override void SetupInternal()
        {
            _inputField = GetComponent<TMP_InputField>();

            _inputField.onValueChanged.AddListener(x => props?.onValueChanged?.value?.Invoke(x));
            _inputField.onEndEdit.AddListener(x => props?.onEndEdit?.value?.Invoke(x));

            _inputText = _inputField.textComponent.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>, TextControl>();

            if (_inputField.placeholder != null && _inputField.placeholder is TMP_Text placeholder)
                _placeholderText = placeholder.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>, TextControl>();

            _background = background.gameObject.GetOrAddComponent<PrimitiveControl<ImageProps>, ImageControl>();

            _inputText.Setup(props.inputText);

            if (_placeholderText != null)
            {
                _placeholderText.Setup(props.placeholderText);
                AddBinding(_placeholderText);
            }

            if (_background != null)
            {
                _background.Setup(props.background);
                AddBinding(_background);
            }

            AddBinding(
                props.value?.Subscribe(x => _inputField.text = x.currentValue),
                props.contentType?.Subscribe(x => _inputField.contentType = x.currentValue),
                props.readOnly?.Subscribe(x => _inputField.readOnly = x.currentValue),
                props.lineType?.Subscribe(x => _inputField.lineType = x.currentValue),
                props.characterLimit?.Subscribe(x => _inputField.characterLimit = x.currentValue),
                props.interactable?.Subscribe(x => _inputField.interactable = x.currentValue),
                _inputText
            );
        }
    }
}