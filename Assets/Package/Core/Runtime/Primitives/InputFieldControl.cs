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
        public TextProps inputText { get; } = new TextProps();
        public TextProps placeholderText { get; } = new TextProps();
        public ValueObservable<TMP_ContentType> contentType { get; } = new ValueObservable<TMP_ContentType>();
        public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
        public ValueObservable<TMP_LineType> lineType { get; } = new ValueObservable<TMP_LineType>();
        public ValueObservable<int> characterLimit { get; } = new ValueObservable<int>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);
        public ValueObservable<Action<string>> onEndEdit { get; } = new ValueObservable<Action<string>>();
        public ImageProps background { get; } = new ImageProps();

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
        }

        protected override void SetupInternal()
        {
            _inputText?.Setup(props.inputText);
            _placeholderText?.Setup(props.placeholderText);
            background?.Setup(props.background);

            _inputField.enabled = false;
            _inputField.enabled = true;

            _inputField.onValueChanged.AddListener(x => props.inputText.value.From(x));
            _inputField.onEndEdit.AddListener(x => props.onEndEdit.value?.Invoke(x));

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