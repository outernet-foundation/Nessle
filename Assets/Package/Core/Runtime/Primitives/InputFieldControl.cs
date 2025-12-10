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
        public TextProps inputText { get; set; }
        public TextProps placeholderText { get; set; }
        public ValueObservable<TMP_ContentType> contentType { get; set; }
        public ValueObservable<bool> readOnly { get; set; }
        public ValueObservable<TMP_LineType> lineType { get; set; }
        public ValueObservable<int> characterLimit { get; set; }
        public ValueObservable<bool> interactable { get; set; }
        public ValueObservable<Action<string>> onEndEdit { get; set; }
        public ImageProps background { get; set; }

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
            props.contentType = props.contentType ?? new ValueObservable<TMP_ContentType>(_inputField.contentType);
            props.readOnly = props.readOnly ?? new ValueObservable<bool>(_inputField.readOnly);
            props.lineType = props.lineType ?? new ValueObservable<TMP_LineType>(_inputField.lineType);
            props.characterLimit = props.characterLimit ?? new ValueObservable<int>(_inputField.characterLimit);
            props.interactable = props.interactable ?? new ValueObservable<bool>(_inputField.interactable);
            props.onEndEdit = props.onEndEdit ?? new ValueObservable<Action<string>>();

            _inputText?.Setup(props.inputText);
            _placeholderText?.Setup(props.placeholderText);
            background?.Setup(props.background);

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
    }
}