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
        public TextProps inputText { get; } = new TextProps();
        public TextProps placeholderText { get; } = new TextProps();
        public ValueObservable<TMP_ContentType> contentType { get; } = new ValueObservable<TMP_ContentType>();
        public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
        public ValueObservable<TMP_LineType> lineType { get; } = new ValueObservable<TMP_LineType>();
        public ValueObservable<int> characterLimit { get; } = new ValueObservable<int>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>();
        public ValueObservable<Action<string>> onEndEdit { get; } = new ValueObservable<Action<string>>();
        public ImageProps background { get; } = new ImageProps();

        public void PopulateFrom(TMP_InputField inputField)
        {
            inputText.PopulateFrom(inputField.textComponent);

            if (inputField.placeholder != null && inputField.placeholder is TMP_Text placeholder)
                placeholderText.PopulateFrom(placeholder);

            contentType.From(inputField.contentType);
            readOnly.From(inputField.readOnly);
            lineType.From(inputField.lineType);
            characterLimit.From(inputField.characterLimit);
            interactable.From(inputField.interactable);
        }

        public IDisposable BindTo(TMP_InputField inputField)
        {
            return new ComposedDisposable(
                value.Subscribe(x => inputField.text = x.currentValue),
                contentType.Subscribe(x => inputField.contentType = x.currentValue),
                readOnly.Subscribe(x => inputField.readOnly = x.currentValue),
                lineType.Subscribe(x => inputField.lineType = x.currentValue),
                characterLimit.Subscribe(x => inputField.characterLimit = x.currentValue),
                interactable.Subscribe(x => inputField.interactable = x.currentValue)
            );
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
        public Image background;

        private TMP_InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onValueChanged.AddListener(x => props.inputText.value.From(x));
            _inputField.onEndEdit.AddListener(x => props.onEndEdit.value?.Invoke(x));
        }

        protected override void SetupInternal()
        {
            AddBinding(props.BindTo(_inputField));

            if (background != null)
                AddBinding(props.background.BindTo(background));
        }

        protected override InputFieldProps GetDefaultProps()
        {
            var props = new InputFieldProps();
            props.PopulateFrom(_inputField);

            if (background != null)
                props.background.PopulateFrom(background);

            return props;
        }
    }
}