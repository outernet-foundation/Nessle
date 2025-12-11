using System;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class TextProps : IDisposable, IValueProps<string>, IColorProps
    {
        public ValueObservable<string> value { get; set; }
        public TextStyleProps style { get; set; }

        ValueObservable<Color> IColorProps.color => style.color;

        public void Dispose()
        {
            value.Dispose();
            style.Dispose();
        }
    }

    public class TextStyleProps : IDisposable
    {
        public ValueObservable<TMP_FontAsset> font { get; set; }
        public ValueObservable<TMP_Style> textStyle { get; set; }
        public ValueObservable<TMP_StyleSheet> styleSheet { get; set; }
        public ValueObservable<Color> color { get; set; }
        public ValueObservable<FontStyles> fontStyle { get; set; }
        public ValueObservable<float> fontSize { get; set; }
        public ValueObservable<FontWeight> fontWeight { get; set; }
        public ValueObservable<bool> enableAutoSizing { get; set; }
        public ValueObservable<float> fontSizeMin { get; set; }
        public ValueObservable<float> fontSizeMax { get; set; }
        public ValueObservable<HorizontalAlignmentOptions> horizontalAlignment { get; set; }
        public ValueObservable<VerticalAlignmentOptions> verticalAlignment { get; set; }
        public ValueObservable<float> characterSpacing { get; set; }
        public ValueObservable<float> wordSpacing { get; set; }
        public ValueObservable<float> lineSpacing { get; set; }
        public ValueObservable<float> lineSpacingAdjustment { get; set; }
        public ValueObservable<float> paragraphSpacing { get; set; }
        public ValueObservable<float> characterWidthAdjustment { get; set; }
        public ValueObservable<TextWrappingModes> textWrappingMode { get; set; }
        public ValueObservable<TextOverflowModes> overflowMode { get; set; }
        public ValueObservable<TextureMappingOptions> horizontalMapping { get; set; }
        public ValueObservable<TextureMappingOptions> verticalMapping { get; set; }

        public void Dispose()
        {
            font.Dispose();
            textStyle.Dispose();
            styleSheet.Dispose();
            color.Dispose();
            fontStyle.Dispose();
            fontSize.Dispose();
            fontWeight.Dispose();
            enableAutoSizing.Dispose();
            fontSizeMin.Dispose();
            fontSizeMax.Dispose();
            horizontalAlignment.Dispose();
            verticalAlignment.Dispose();
            characterSpacing.Dispose();
            wordSpacing.Dispose();
            lineSpacing.Dispose();
            lineSpacingAdjustment.Dispose();
            paragraphSpacing.Dispose();
            characterWidthAdjustment.Dispose();
            textWrappingMode.Dispose();
            overflowMode.Dispose();
            horizontalMapping.Dispose();
            verticalMapping.Dispose();
        }
    }

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextControl : PrimitiveControl<TextProps>
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void SetupInternal()
        {
            props.value = props.value ?? new ValueObservable<string>(_text.text);
            props.style = props.style ?? new TextStyleProps();
            props.style.font = props.style.font ?? new ValueObservable<TMP_FontAsset>(_text.font);
            props.style.textStyle = props.style.textStyle ?? new ValueObservable<TMP_Style>(_text.textStyle);
            props.style.styleSheet = props.style.styleSheet ?? new ValueObservable<TMP_StyleSheet>(_text.styleSheet);
            props.style.color = props.style.color ?? new ValueObservable<Color>(_text.color);
            props.style.fontStyle = props.style.fontStyle ?? new ValueObservable<FontStyles>(_text.fontStyle);
            props.style.fontSize = props.style.fontSize ?? new ValueObservable<float>(_text.fontSize);
            props.style.fontWeight = props.style.fontWeight ?? new ValueObservable<FontWeight>(_text.fontWeight);
            props.style.enableAutoSizing = props.style.enableAutoSizing ?? new ValueObservable<bool>(_text.enableAutoSizing);
            props.style.fontSizeMin = props.style.fontSizeMin ?? new ValueObservable<float>(_text.fontSizeMin);
            props.style.fontSizeMax = props.style.fontSizeMax ?? new ValueObservable<float>(_text.fontSizeMax);
            props.style.horizontalAlignment = props.style.horizontalAlignment ?? new ValueObservable<HorizontalAlignmentOptions>(_text.horizontalAlignment);
            props.style.verticalAlignment = props.style.verticalAlignment ?? new ValueObservable<VerticalAlignmentOptions>(_text.verticalAlignment);
            props.style.characterSpacing = props.style.characterSpacing ?? new ValueObservable<float>(_text.characterSpacing);
            props.style.wordSpacing = props.style.wordSpacing ?? new ValueObservable<float>(_text.wordSpacing);
            props.style.lineSpacing = props.style.lineSpacing ?? new ValueObservable<float>(_text.lineSpacing);
            props.style.lineSpacingAdjustment = props.style.lineSpacingAdjustment ?? new ValueObservable<float>(_text.lineSpacingAdjustment);
            props.style.paragraphSpacing = props.style.paragraphSpacing ?? new ValueObservable<float>(_text.paragraphSpacing);
            props.style.characterWidthAdjustment = props.style.characterWidthAdjustment ?? new ValueObservable<float>(_text.characterWidthAdjustment);
            props.style.textWrappingMode = props.style.textWrappingMode ?? new ValueObservable<TextWrappingModes>(_text.textWrappingMode);
            props.style.overflowMode = props.style.overflowMode ?? new ValueObservable<TextOverflowModes>(_text.overflowMode);
            props.style.horizontalMapping = props.style.horizontalMapping ?? new ValueObservable<TextureMappingOptions>(_text.horizontalMapping);
            props.style.verticalMapping = props.style.verticalMapping ?? new ValueObservable<TextureMappingOptions>(_text.verticalMapping);

            AddBinding(
                props.value.Subscribe(x => _text.text = x.currentValue),
                props.style.font.Subscribe(x => _text.font = x.currentValue),
                props.style.textStyle.Subscribe(x => _text.textStyle = x.currentValue),
                props.style.styleSheet.Subscribe(x => _text.styleSheet = x.currentValue),
                props.style.color.Subscribe(x => _text.color = x.currentValue),
                props.style.fontStyle.Subscribe(x => _text.fontStyle = x.currentValue),
                props.style.fontSize.Subscribe(x => _text.fontSize = x.currentValue),
                props.style.fontWeight.Subscribe(x => _text.fontWeight = x.currentValue),
                props.style.enableAutoSizing.Subscribe(x => _text.enableAutoSizing = x.currentValue),
                props.style.fontSizeMin.Subscribe(x => _text.fontSizeMin = x.currentValue),
                props.style.fontSizeMax.Subscribe(x => _text.fontSizeMax = x.currentValue),
                props.style.horizontalAlignment.Subscribe(x => _text.horizontalAlignment = x.currentValue),
                props.style.verticalAlignment.Subscribe(x => _text.verticalAlignment = x.currentValue),
                props.style.characterSpacing.Subscribe(x => _text.characterSpacing = x.currentValue),
                props.style.wordSpacing.Subscribe(x => _text.wordSpacing = x.currentValue),
                props.style.lineSpacing.Subscribe(x => _text.lineSpacing = x.currentValue),
                props.style.lineSpacingAdjustment.Subscribe(x => _text.lineSpacingAdjustment = x.currentValue),
                props.style.paragraphSpacing.Subscribe(x => _text.paragraphSpacing = x.currentValue),
                props.style.characterWidthAdjustment.Subscribe(x => _text.characterWidthAdjustment = x.currentValue),
                props.style.textWrappingMode.Subscribe(x => _text.textWrappingMode = x.currentValue),
                props.style.overflowMode.Subscribe(x => _text.overflowMode = x.currentValue),
                props.style.horizontalMapping.Subscribe(x => _text.horizontalMapping = x.currentValue),
                props.style.verticalMapping.Subscribe(x => _text.verticalMapping = x.currentValue)
            );
        }
    }
}