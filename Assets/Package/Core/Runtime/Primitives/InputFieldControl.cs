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
        public ValueObservable<string> value => inputText.value;
        public TextProps inputText { get; }
        public TextProps placeholderText { get; }
        public ValueObservable<TMP_ContentType> contentType { get; }
        public ValueObservable<bool> readOnly { get; }
        public ValueObservable<TMP_LineType> lineType { get; }
        public ValueObservable<int> characterLimit { get; }
        public ValueObservable<bool> interactable { get; }
        public ValueObservable<Action<string>> onEndEdit { get; }
        public ImageProps background { get; }

        public InputFieldProps(
            TextProps inputText = default,
            TextProps placeholderText = default,
            ValueObservable<TMP_ContentType> contentType = default,
            ValueObservable<bool> readOnly = default,
            ValueObservable<TMP_LineType> lineType = default,
            ValueObservable<int> characterLimit = default,
            ValueObservable<bool> interactable = default,
            ValueObservable<Action<string>> onEndEdit = default,
            ImageProps background = default
        )
        {
            this.inputText = inputText ?? new TextProps();
            this.placeholderText = placeholderText ?? new TextProps();
            this.contentType = contentType ?? new ValueObservable<TMP_ContentType>();
            this.readOnly = readOnly ?? new ValueObservable<bool>();
            this.lineType = lineType ?? new ValueObservable<TMP_LineType>();
            this.characterLimit = characterLimit ?? new ValueObservable<int>();
            this.interactable = interactable ?? new ValueObservable<bool>();
            this.onEndEdit = onEndEdit ?? new ValueObservable<Action<string>>();
            this.background = background ?? new ImageProps();
        }

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
            background.Dispose();
        }
    }

    public class InputFieldProps<T> : IDisposable, IValueProps<T>, IInteractableProps
    {
        public ValueObservable<T> value { get; } = new ValueObservable<T>();
        public InputFieldProps inputField { get; } = new InputFieldProps();
        public ValueObservable<bool> interactable => inputField.interactable;

        public void Dispose()
        {
            value.Dispose();
            inputField.Dispose();
        }
    }

    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldControl : PrimitiveControl<InputFieldProps>
    {
        public PrimitiveControl<ImageProps> background;

        private TMP_InputField _inputField;
        private PrimitiveControl<TextProps> _inputText;
        private PrimitiveControl<TextProps> _placeholderText;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputText = _inputField.textComponent.gameObject.GetComponent<PrimitiveControl<TextProps>>();
            _placeholderText = _inputField.placeholder.gameObject.GetComponent<PrimitiveControl<TextProps>>();
            _inputField.onValueChanged.AddListener(x => props.inputText.value.From(x));
            _inputField.onEndEdit.AddListener(x => props.onEndEdit.value?.Invoke(x));
        }

        protected override void SetupInternal()
        {
            AddBinding(
                props.value.Subscribe(x => _inputField.text = x.currentValue),
                props.contentType.Subscribe(x => _inputField.contentType = x.currentValue),
                props.readOnly.Subscribe(x => _inputField.readOnly = x.currentValue),
                props.lineType.Subscribe(x => _inputField.lineType = x.currentValue),
                props.characterLimit.Subscribe(x => _inputField.characterLimit = x.currentValue),
                props.interactable.Subscribe(x => _inputField.interactable = x.currentValue)
            );
        }

        protected override void DisposeInternal()
        {
            _inputText?.Dispose();
            _placeholderText?.Dispose();
            background?.Dispose();
        }

        public override InputFieldProps GetInstanceProps()
        {
            return new InputFieldProps(
                _inputText?.GetInstanceProps(),
                _placeholderText?.GetInstanceProps(),
                new ValueObservable<TMP_ContentType>(_inputField.contentType),
                new ValueObservable<bool>(_inputField.readOnly),
                new ValueObservable<TMP_LineType>(_inputField.lineType),
                new ValueObservable<int>(_inputField.characterLimit),
                new ValueObservable<bool>(_inputField.interactable),
                new ValueObservable<Action<string>>(),
                background?.GetInstanceProps()
            );
        }
    }
}