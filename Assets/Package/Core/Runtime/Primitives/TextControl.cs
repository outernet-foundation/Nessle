using System;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public struct TextProps
    {
        public IValueObservable<string> value;
        public TextStyleProps style;
    }

    public struct TextStyleProps
    {
        public IValueObservable<TMP_FontAsset> font;
        public IValueObservable<TMP_Style> textStyle;
        public IValueObservable<TMP_StyleSheet> styleSheet;
        public IValueObservable<Color> color;
        public IValueObservable<FontStyles> fontStyle;
        public IValueObservable<float> fontSize;
        public IValueObservable<FontWeight> fontWeight;
        public IValueObservable<bool> enableAutoSizing;
        public IValueObservable<float> fontSizeMin;
        public IValueObservable<float> fontSizeMax;
        public IValueObservable<HorizontalAlignmentOptions> horizontalAlignment;
        public IValueObservable<VerticalAlignmentOptions> verticalAlignment;
        public IValueObservable<float> characterSpacing;
        public IValueObservable<float> wordSpacing;
        public IValueObservable<float> lineSpacing;
        public IValueObservable<float> lineSpacingAdjustment;
        public IValueObservable<float> paragraphSpacing;
        public IValueObservable<float> characterWidthAdjustment;
        public IValueObservable<TextWrappingModes> textWrappingMode;
        public IValueObservable<TextOverflowModes> overflowMode;
        public IValueObservable<TextureMappingOptions> horizontalMapping;
        public IValueObservable<TextureMappingOptions> verticalMapping;
        public IValueObservable<Color> outlineColor;
        public IValueObservable<float> outlineWidth;
    }

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextControl : PrimitiveControl<TextProps>
    {
        private TextMeshProUGUI _text;

        protected override void SetupInternal()
        {
            _text = GetComponent<TextMeshProUGUI>();

            AddBinding(
                props.value?.Subscribe(x => _text.text = x.currentValue),
                props.style.font?.Subscribe(x => _text.font = x.currentValue),
                props.style.textStyle?.Subscribe(x => _text.textStyle = x.currentValue),
                props.style.styleSheet?.Subscribe(x => _text.styleSheet = x.currentValue),
                props.style.color?.Subscribe(x => _text.color = x.currentValue),
                props.style.fontStyle?.Subscribe(x => _text.fontStyle = x.currentValue),
                props.style.fontSize?.Subscribe(x => _text.fontSize = x.currentValue),
                props.style.fontWeight?.Subscribe(x => _text.fontWeight = x.currentValue),
                props.style.enableAutoSizing?.Subscribe(x => _text.enableAutoSizing = x.currentValue),
                props.style.fontSizeMin?.Subscribe(x => _text.fontSizeMin = x.currentValue),
                props.style.fontSizeMax?.Subscribe(x => _text.fontSizeMax = x.currentValue),
                props.style.horizontalAlignment?.Subscribe(x => _text.horizontalAlignment = x.currentValue),
                props.style.verticalAlignment?.Subscribe(x => _text.verticalAlignment = x.currentValue),
                props.style.characterSpacing?.Subscribe(x => _text.characterSpacing = x.currentValue),
                props.style.wordSpacing?.Subscribe(x => _text.wordSpacing = x.currentValue),
                props.style.lineSpacing?.Subscribe(x => _text.lineSpacing = x.currentValue),
                props.style.lineSpacingAdjustment?.Subscribe(x => _text.lineSpacingAdjustment = x.currentValue),
                props.style.paragraphSpacing?.Subscribe(x => _text.paragraphSpacing = x.currentValue),
                props.style.characterWidthAdjustment?.Subscribe(x => _text.characterWidthAdjustment = x.currentValue),
                props.style.textWrappingMode?.Subscribe(x => _text.textWrappingMode = x.currentValue),
                props.style.overflowMode?.Subscribe(x => _text.overflowMode = x.currentValue),
                props.style.horizontalMapping?.Subscribe(x => _text.horizontalMapping = x.currentValue),
                props.style.verticalMapping?.Subscribe(x => _text.verticalMapping = x.currentValue),
                props.style.outlineColor?.Subscribe(x => _text.outlineColor = x.currentValue),
                props.style.outlineWidth?.Subscribe(x => _text.outlineWidth = x.currentValue)
            );
        }
    }
}