using System;
using UnityEngine;
using ObserveThing;
using TMPro;

namespace Nessle
{
    public class TextProps : IDisposable, IValueProps<string>, IColorProps
    {
        public ValueObservable<string> value { get; } = new ValueObservable<string>();
        public TextStyleProps style { get; } = new TextStyleProps();

        ValueObservable<Color> IColorProps.color => style.color;

        public void Dispose()
        {
            value.Dispose();
            style.Dispose();
        }

        public void PopulateFrom(TMP_Text text)
        {
            value.From(text.text);
            style.PopulateFrom(text);
        }

        public IDisposable BindTo(TMP_Text text)
        {
            return new ComposedDisposable(
                value.Subscribe(x => text.text = x.currentValue),
                style.BindTo(text)
            );
        }
    }

    public class TextStyleProps : IDisposable
    {
        public ValueObservable<TMP_FontAsset> font { get; } = new ValueObservable<TMP_FontAsset>();
        public ValueObservable<TMP_Style> textStyle { get; } = new ValueObservable<TMP_Style>();
        public ValueObservable<TMP_StyleSheet> styleSheet { get; } = new ValueObservable<TMP_StyleSheet>();
        public ValueObservable<Color> color { get; } = new ValueObservable<Color>();
        public ValueObservable<FontStyles> fontStyle { get; } = new ValueObservable<FontStyles>();
        public ValueObservable<float> fontSize { get; } = new ValueObservable<float>();
        public ValueObservable<FontWeight> fontWeight { get; } = new ValueObservable<FontWeight>();
        public ValueObservable<bool> enableAutoSizing { get; } = new ValueObservable<bool>();
        public ValueObservable<float> fontSizeMin { get; } = new ValueObservable<float>();
        public ValueObservable<float> fontSizeMax { get; } = new ValueObservable<float>();
        public ValueObservable<HorizontalAlignmentOptions> horizontalAlignment { get; } = new ValueObservable<HorizontalAlignmentOptions>();
        public ValueObservable<VerticalAlignmentOptions> verticalAlignment { get; } = new ValueObservable<VerticalAlignmentOptions>();
        public ValueObservable<float> characterSpacing { get; } = new ValueObservable<float>();
        public ValueObservable<float> wordSpacing { get; } = new ValueObservable<float>();
        public ValueObservable<float> lineSpacing { get; } = new ValueObservable<float>();
        public ValueObservable<float> lineSpacingAdjustment { get; } = new ValueObservable<float>();
        public ValueObservable<float> paragraphSpacing { get; } = new ValueObservable<float>();
        public ValueObservable<float> characterWidthAdjustment { get; } = new ValueObservable<float>();
        public ValueObservable<TextWrappingModes> textWrappingMode { get; } = new ValueObservable<TextWrappingModes>();
        public ValueObservable<TextOverflowModes> overflowMode { get; } = new ValueObservable<TextOverflowModes>();
        public ValueObservable<TextureMappingOptions> horizontalMapping { get; } = new ValueObservable<TextureMappingOptions>();
        public ValueObservable<TextureMappingOptions> verticalMapping { get; } = new ValueObservable<TextureMappingOptions>();

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

        public void PopulateFrom(TMP_Text text)
        {
            font.From(text.font);
            textStyle.From(text.textStyle);
            styleSheet.From(text.styleSheet);
            color.From(text.color);
            fontStyle.From(text.fontStyle);
            fontSize.From(text.fontSize);
            fontWeight.From(text.fontWeight);
            enableAutoSizing.From(text.enableAutoSizing);
            fontSizeMin.From(text.fontSizeMin);
            fontSizeMax.From(text.fontSizeMax);
            horizontalAlignment.From(text.horizontalAlignment);
            verticalAlignment.From(text.verticalAlignment);
            characterSpacing.From(text.characterSpacing);
            wordSpacing.From(text.wordSpacing);
            lineSpacing.From(text.lineSpacing);
            lineSpacingAdjustment.From(text.lineSpacingAdjustment);
            paragraphSpacing.From(text.paragraphSpacing);
            characterWidthAdjustment.From(text.characterWidthAdjustment);
            textWrappingMode.From(text.textWrappingMode);
            overflowMode.From(text.overflowMode);
            horizontalMapping.From(text.horizontalMapping);
            verticalMapping.From(text.verticalMapping);
        }

        public IDisposable BindTo(TMP_Text text)
        {
            return new ComposedDisposable(
                font.Subscribe(x => text.font = x.currentValue),
                textStyle.Subscribe(x => text.textStyle = x.currentValue),
                styleSheet.Subscribe(x => text.styleSheet = x.currentValue),
                color.Subscribe(x => text.color = x.currentValue),
                fontStyle.Subscribe(x => text.fontStyle = x.currentValue),
                fontSize.Subscribe(x => text.fontSize = x.currentValue),
                fontWeight.Subscribe(x => text.fontWeight = x.currentValue),
                enableAutoSizing.Subscribe(x => text.enableAutoSizing = x.currentValue),
                fontSizeMin.Subscribe(x => text.fontSizeMin = x.currentValue),
                fontSizeMax.Subscribe(x => text.fontSizeMax = x.currentValue),
                horizontalAlignment.Subscribe(x => text.horizontalAlignment = x.currentValue),
                verticalAlignment.Subscribe(x => text.verticalAlignment = x.currentValue),
                characterSpacing.Subscribe(x => text.characterSpacing = x.currentValue),
                wordSpacing.Subscribe(x => text.wordSpacing = x.currentValue),
                lineSpacing.Subscribe(x => text.lineSpacing = x.currentValue),
                lineSpacingAdjustment.Subscribe(x => text.lineSpacingAdjustment = x.currentValue),
                paragraphSpacing.Subscribe(x => text.paragraphSpacing = x.currentValue),
                characterWidthAdjustment.Subscribe(x => text.characterWidthAdjustment = x.currentValue),
                textWrappingMode.Subscribe(x => text.textWrappingMode = x.currentValue),
                overflowMode.Subscribe(x => text.overflowMode = x.currentValue),
                horizontalMapping.Subscribe(x => text.horizontalMapping = x.currentValue),
                verticalMapping.Subscribe(x => text.verticalMapping = x.currentValue)
            );
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
            AddBinding(props.BindTo(_text));
        }

        protected override TextProps GetDefaultProps()
        {
            var props = new TextProps();
            props.PopulateFrom(_text);
            return props;
        }
    }
}