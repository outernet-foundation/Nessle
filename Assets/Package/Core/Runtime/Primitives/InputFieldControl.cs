using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using TMPro;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;
using TMP_LineType = TMPro.TMP_InputField.LineType;

namespace Nessle
{
    public class InputFieldProps : IDisposable, IValueProps<string>, IInteractableProps
    {
        public ValueObservable<string> value => inputText.value;
        public TextProps inputText { get; private set; }
        public TextProps placeholderText { get; private set; }
        public ValueObservable<TMP_ContentType> contentType { get; private set; }
        public ValueObservable<bool> readOnly { get; private set; }
        public ValueObservable<TMP_LineType> lineType { get; private set; }
        public ValueObservable<int> characterLimit { get; private set; }
        public ValueObservable<bool> interactable { get; private set; }
        public ValueObservable<Action<string>> onEndEdit { get; private set; }
        public ImageProps background { get; private set; }

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
            this.inputText = inputText;
            this.placeholderText = placeholderText;
            this.contentType = contentType;
            this.readOnly = readOnly;
            this.lineType = lineType;
            this.characterLimit = characterLimit;
            this.interactable = interactable;
            this.onEndEdit = onEndEdit;
            this.background = background;
        }

        public void CompleteWith(
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
            this.inputText = this.inputText ?? inputText;
            this.placeholderText = this.placeholderText ?? placeholderText;
            this.contentType = this.contentType ?? contentType;
            this.readOnly = this.readOnly ?? readOnly;
            this.lineType = this.lineType ?? lineType;
            this.characterLimit = this.characterLimit ?? characterLimit;
            this.interactable = this.interactable ?? interactable;
            this.onEndEdit = this.onEndEdit ?? onEndEdit;
            this.background = this.background ?? background;
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
        public ValueObservable<T> value { get; private set; }
        public InputFieldProps inputField { get; private set; }
        public ValueObservable<bool> interactable => inputField.interactable;

        public InputFieldProps(
            ValueObservable<T> value = default,
            InputFieldProps inputField = default
        )
        {
            this.value = value;
            this.inputField = inputField;
        }

        public void CompleteWith(
            ValueObservable<T> value = default,
            InputFieldProps inputField = default
        )
        {
            this.value = this.value ?? value;
            this.inputField = this.inputField ?? inputField;
        }

        public void Dispose()
        {
            value.Dispose();
            inputField.Dispose();
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

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onValueChanged.AddListener(x => props.inputText.value.From(x));
            _inputField.onEndEdit.AddListener(x => props.onEndEdit.value?.Invoke(x));

            _inputText = _inputField.textComponent.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>>();

            if (_inputField.placeholder != null && _inputField.placeholder is TMP_Text placeholder)
                _placeholderText = placeholder.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>>();

            if (background != null)
                _background = background.gameObject.GetOrAddComponent<PrimitiveControl<ImageProps>>();
        }

        protected override void SetupInternal()
        {
            props.CompleteWith(
                new TextProps(),
                new TextProps(),
                new ValueObservable<TMP_ContentType>(_inputField.contentType),
                new ValueObservable<bool>(_inputField.readOnly),
                new ValueObservable<TMP_LineType>(_inputField.lineType),
                new ValueObservable<int>(_inputField.characterLimit),
                new ValueObservable<bool>(_inputField.interactable),
                new ValueObservable<Action<string>>(),
                new ImageProps()
            );

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
                props.value.Subscribe(x => _inputField.text = x.currentValue),
                props.contentType.Subscribe(x => _inputField.contentType = x.currentValue),
                props.readOnly.Subscribe(x => _inputField.readOnly = x.currentValue),
                props.lineType.Subscribe(x => _inputField.lineType = x.currentValue),
                props.characterLimit.Subscribe(x => _inputField.characterLimit = x.currentValue),
                props.interactable.Subscribe(x => _inputField.interactable = x.currentValue),
                _inputText
            );
        }
    }
}