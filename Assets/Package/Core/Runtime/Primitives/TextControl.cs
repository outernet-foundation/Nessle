using System;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class TextProps : IDisposable, IValueProps<string>, IColorProps
    {
        public ValueObservable<string> value { get; }
        public TextStyleProps style { get; }

        ValueObservable<Color> IColorProps.color => style.color;

        public TextProps(ValueObservable<string> value = default, TextStyleProps style = default)
        {
            this.value = value ?? new ValueObservable<string>();
            this.style = style ?? new TextStyleProps();
        }

        public void Dispose()
        {
            value.Dispose();
            style.Dispose();
        }
    }

    public class TextStyleProps : IDisposable
    {
        public ValueObservable<TMP_FontAsset> font { get; }
        public ValueObservable<TMP_Style> textStyle { get; }
        public ValueObservable<TMP_StyleSheet> styleSheet { get; }
        public ValueObservable<Color> color { get; }
        public ValueObservable<FontStyles> fontStyle { get; }
        public ValueObservable<float> fontSize { get; }
        public ValueObservable<FontWeight> fontWeight { get; }
        public ValueObservable<bool> enableAutoSizing { get; }
        public ValueObservable<float> fontSizeMin { get; }
        public ValueObservable<float> fontSizeMax { get; }
        public ValueObservable<HorizontalAlignmentOptions> horizontalAlignment { get; }
        public ValueObservable<VerticalAlignmentOptions> verticalAlignment { get; }
        public ValueObservable<float> characterSpacing { get; }
        public ValueObservable<float> wordSpacing { get; }
        public ValueObservable<float> lineSpacing { get; }
        public ValueObservable<float> lineSpacingAdjustment { get; }
        public ValueObservable<float> paragraphSpacing { get; }
        public ValueObservable<float> characterWidthAdjustment { get; }
        public ValueObservable<TextWrappingModes> textWrappingMode { get; }
        public ValueObservable<TextOverflowModes> overflowMode { get; }
        public ValueObservable<TextureMappingOptions> horizontalMapping { get; }
        public ValueObservable<TextureMappingOptions> verticalMapping { get; }

        public TextStyleProps(
            ValueObservable<TMP_FontAsset> font = default,
            ValueObservable<TMP_Style> textStyle = default,
            ValueObservable<TMP_StyleSheet> styleSheet = default,
            ValueObservable<Color> color = default,
            ValueObservable<FontStyles> fontStyle = default,
            ValueObservable<float> fontSize = default,
            ValueObservable<FontWeight> fontWeight = default,
            ValueObservable<bool> enableAutoSizing = default,
            ValueObservable<float> fontSizeMin = default,
            ValueObservable<float> fontSizeMax = default,
            ValueObservable<HorizontalAlignmentOptions> horizontalAlignment = default,
            ValueObservable<VerticalAlignmentOptions> verticalAlignment = default,
            ValueObservable<float> characterSpacing = default,
            ValueObservable<float> wordSpacing = default,
            ValueObservable<float> lineSpacing = default,
            ValueObservable<float> lineSpacingAdjustment = default,
            ValueObservable<float> paragraphSpacing = default,
            ValueObservable<float> characterWidthAdjustment = default,
            ValueObservable<TextWrappingModes> textWrappingMode = default,
            ValueObservable<TextOverflowModes> overflowMode = default,
            ValueObservable<TextureMappingOptions> horizontalMapping = default,
            ValueObservable<TextureMappingOptions> verticalMapping = default
        )
        {
            this.font = font ?? new ValueObservable<TMP_FontAsset>();
            this.textStyle = textStyle ?? new ValueObservable<TMP_Style>();
            this.styleSheet = styleSheet ?? new ValueObservable<TMP_StyleSheet>();
            this.color = color ?? new ValueObservable<Color>();
            this.fontStyle = fontStyle ?? new ValueObservable<FontStyles>();
            this.fontSize = fontSize ?? new ValueObservable<float>();
            this.fontWeight = fontWeight ?? new ValueObservable<FontWeight>();
            this.enableAutoSizing = enableAutoSizing ?? new ValueObservable<bool>();
            this.fontSizeMin = fontSizeMin ?? new ValueObservable<float>();
            this.fontSizeMax = fontSizeMax ?? new ValueObservable<float>();
            this.horizontalAlignment = horizontalAlignment ?? new ValueObservable<HorizontalAlignmentOptions>();
            this.verticalAlignment = verticalAlignment ?? new ValueObservable<VerticalAlignmentOptions>();
            this.characterSpacing = characterSpacing ?? new ValueObservable<float>();
            this.wordSpacing = wordSpacing ?? new ValueObservable<float>();
            this.lineSpacing = lineSpacing ?? new ValueObservable<float>();
            this.lineSpacingAdjustment = lineSpacingAdjustment ?? new ValueObservable<float>();
            this.paragraphSpacing = paragraphSpacing ?? new ValueObservable<float>();
            this.characterWidthAdjustment = characterWidthAdjustment ?? new ValueObservable<float>();
            this.textWrappingMode = textWrappingMode ?? new ValueObservable<TextWrappingModes>();
            this.overflowMode = overflowMode ?? new ValueObservable<TextOverflowModes>();
            this.horizontalMapping = horizontalMapping ?? new ValueObservable<TextureMappingOptions>();
            this.verticalMapping = verticalMapping ?? new ValueObservable<TextureMappingOptions>();
        }

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
            AddBinding(
                Utility.BindTextStyle(props.style, _text),
                props.value.Subscribe(x => _text.text = x.currentValue)
            );
        }

        public override TextProps GetInstanceProps()
        {
            return new TextProps(
                new ValueObservable<string>(_text.text),
                Utility.StylePropsFromText(_text)
            );
        }
    }
}