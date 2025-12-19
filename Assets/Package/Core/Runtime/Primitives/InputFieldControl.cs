using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using TMPro;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;
using TMP_LineType = TMPro.TMP_InputField.LineType;
using UnityEngine.Events;

namespace Nessle
{
    public struct InputFieldProps
    {
        public IValueObservable<string> value;
        public IValueObservable<string> placeholderValue;
        public TextStyleProps inputTextStyle;
        public TextStyleProps placeholderTextStyle;
        public IValueObservable<TMP_ContentType> contentType;
        public IValueObservable<bool> readOnly;
        public IValueObservable<TMP_LineType> lineType;
        public IValueObservable<int> characterLimit;
        public IValueObservable<bool> interactable;
        public UnityAction<string> onValueChanged;
        public UnityAction<string> onEndEdit;
        public ImageProps background;
    }

    public struct InputFieldProps<T>
    {
        public IValueObservable<T> value;
        public IValueObservable<string> placeholderValue;
        public TextStyleProps inputTextStyle;
        public TextStyleProps placeholderTextStyle;
        public IValueObservable<bool> readOnly;
        public IValueObservable<TMP_LineType> lineType;
        public IValueObservable<int> characterLimit;
        public IValueObservable<bool> interactable;
        public ImageProps background;
        public Action<T> onValueChanged;
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
            _inputText = _inputField.textComponent.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>, TextControl>();

            if (_inputField.placeholder != null && _inputField.placeholder is TMP_Text placeholder)
                _placeholderText = placeholder.gameObject.GetOrAddComponent<PrimitiveControl<TextProps>, TextControl>();

            if (background != null)
                _background = background.gameObject.GetOrAddComponent<PrimitiveControl<ImageProps>, ImageControl>();

            _inputText.Setup(new TextProps() { style = props.inputTextStyle });

            if (_placeholderText != null)
            {
                _placeholderText.Setup(new TextProps() { style = props.placeholderTextStyle });
                AddBinding(_placeholderText);
            }

            if (_background != null)
            {
                _background.Setup(props.background);
                AddBinding(_background);
            }

            if (props.onValueChanged != null)
                _inputField.onValueChanged.AddListener(props.onValueChanged);

            if (props.onEndEdit != null)
                _inputField.onEndEdit.AddListener(props.onEndEdit);

            AddBinding(
                _inputText,
                props.value?.Subscribe(x => _inputField.text = x.currentValue),
                props.contentType?.Subscribe(x => _inputField.contentType = x.currentValue),
                props.readOnly?.Subscribe(x => _inputField.readOnly = x.currentValue),
                props.lineType?.Subscribe(x => _inputField.lineType = x.currentValue),
                props.characterLimit?.Subscribe(x => _inputField.characterLimit = x.currentValue),
                props.interactable?.Subscribe(x => _inputField.interactable = x.currentValue)
            );
        }
    }
}