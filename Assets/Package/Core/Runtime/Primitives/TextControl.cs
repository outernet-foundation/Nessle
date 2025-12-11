using System;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class TextProps : IDisposable, IValueProps<string>, IColorProps
    {
        public ValueObservable<string> value { get; private set; }
        public TextStyleProps style { get; private set; }

        ValueObservable<Color> IColorProps.color => style.color;

        public TextProps(
            ValueObservable<string> value = default,
            TextStyleProps style = default
        )
        {
            this.value = value;
            this.style = style;
        }

        public void CompleteWith(
            ValueObservable<string> value = default,
            TextStyleProps style = default
        )
        {
            this.value = this.value ?? value;
            this.style = this.style ?? style;
        }

        public void Dispose()
        {
            value.Dispose();
            style.Dispose();
        }
    }

    public class TextStyleProps : IDisposable
    {
        public ValueObservable<TMP_FontAsset> font { get; private set; }
        public ValueObservable<TMP_Style> textStyle { get; private set; }
        public ValueObservable<TMP_StyleSheet> styleSheet { get; private set; }
        public ValueObservable<Color> color { get; private set; }
        public ValueObservable<FontStyles> fontStyle { get; private set; }
        public ValueObservable<float> fontSize { get; private set; }
        public ValueObservable<FontWeight> fontWeight { get; private set; }
        public ValueObservable<bool> enableAutoSizing { get; private set; }
        public ValueObservable<float> fontSizeMin { get; private set; }
        public ValueObservable<float> fontSizeMax { get; private set; }
        public ValueObservable<HorizontalAlignmentOptions> horizontalAlignment { get; private set; }
        public ValueObservable<VerticalAlignmentOptions> verticalAlignment { get; private set; }
        public ValueObservable<float> characterSpacing { get; private set; }
        public ValueObservable<float> wordSpacing { get; private set; }
        public ValueObservable<float> lineSpacing { get; private set; }
        public ValueObservable<float> lineSpacingAdjustment { get; private set; }
        public ValueObservable<float> paragraphSpacing { get; private set; }
        public ValueObservable<float> characterWidthAdjustment { get; private set; }
        public ValueObservable<TextWrappingModes> textWrappingMode { get; private set; }
        public ValueObservable<TextOverflowModes> overflowMode { get; private set; }
        public ValueObservable<TextureMappingOptions> horizontalMapping { get; private set; }
        public ValueObservable<TextureMappingOptions> verticalMapping { get; private set; }

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
            this.font = font;
            this.textStyle = textStyle;
            this.styleSheet = styleSheet;
            this.color = color;
            this.fontStyle = fontStyle;
            this.fontSize = fontSize;
            this.fontWeight = fontWeight;
            this.enableAutoSizing = enableAutoSizing;
            this.fontSizeMin = fontSizeMin;
            this.fontSizeMax = fontSizeMax;
            this.horizontalAlignment = horizontalAlignment;
            this.verticalAlignment = verticalAlignment;
            this.characterSpacing = characterSpacing;
            this.wordSpacing = wordSpacing;
            this.lineSpacing = lineSpacing;
            this.lineSpacingAdjustment = lineSpacingAdjustment;
            this.paragraphSpacing = paragraphSpacing;
            this.characterWidthAdjustment = characterWidthAdjustment;
            this.textWrappingMode = textWrappingMode;
            this.overflowMode = overflowMode;
            this.horizontalMapping = horizontalMapping;
            this.verticalMapping = verticalMapping;
        }

        public void CompleteWith(
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
            this.font = this.font ?? font;
            this.textStyle = this.textStyle ?? textStyle;
            this.styleSheet = this.styleSheet ?? styleSheet;
            this.color = this.color ?? color;
            this.fontStyle = this.fontStyle ?? fontStyle;
            this.fontSize = this.fontSize ?? fontSize;
            this.fontWeight = this.fontWeight ?? fontWeight;
            this.enableAutoSizing = this.enableAutoSizing ?? enableAutoSizing;
            this.fontSizeMin = this.fontSizeMin ?? fontSizeMin;
            this.fontSizeMax = this.fontSizeMax ?? fontSizeMax;
            this.horizontalAlignment = this.horizontalAlignment ?? horizontalAlignment;
            this.verticalAlignment = this.verticalAlignment ?? verticalAlignment;
            this.characterSpacing = this.characterSpacing ?? characterSpacing;
            this.wordSpacing = this.wordSpacing ?? wordSpacing;
            this.lineSpacing = this.lineSpacing ?? lineSpacing;
            this.lineSpacingAdjustment = this.lineSpacingAdjustment ?? lineSpacingAdjustment;
            this.paragraphSpacing = this.paragraphSpacing ?? paragraphSpacing;
            this.characterWidthAdjustment = this.characterWidthAdjustment ?? characterWidthAdjustment;
            this.textWrappingMode = this.textWrappingMode ?? textWrappingMode;
            this.overflowMode = this.overflowMode ?? overflowMode;
            this.horizontalMapping = this.horizontalMapping ?? horizontalMapping;
            this.verticalMapping = this.verticalMapping ?? verticalMapping;
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
            props.CompleteWith(
                Props.From(_text.text),
                new TextStyleProps()
            );

            props.style.CompleteWith(
                Props.From(_text.font),
                Props.From(_text.textStyle),
                Props.From(_text.styleSheet),
                Props.From(_text.color),
                Props.From(_text.fontStyle),
                Props.From(_text.fontSize),
                Props.From(_text.fontWeight),
                Props.From(_text.enableAutoSizing),
                Props.From(_text.fontSizeMin),
                Props.From(_text.fontSizeMax),
                Props.From(_text.horizontalAlignment),
                Props.From(_text.verticalAlignment),
                Props.From(_text.characterSpacing),
                Props.From(_text.wordSpacing),
                Props.From(_text.lineSpacing),
                Props.From(_text.lineSpacingAdjustment),
                Props.From(_text.paragraphSpacing),
                Props.From(_text.characterWidthAdjustment),
                Props.From(_text.textWrappingMode),
                Props.From(_text.overflowMode),
                Props.From(_text.horizontalMapping),
                Props.From(_text.verticalMapping)
            );

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