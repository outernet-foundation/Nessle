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
            Utility.CopyFromText(props.style, _text);
            props.value.From(_text.text);

            AddBinding(
                Utility.BindTextStyle(props.style, _text),
                props.value.Subscribe(x => _text.text = x.currentValue)
            );
        }
    }
}