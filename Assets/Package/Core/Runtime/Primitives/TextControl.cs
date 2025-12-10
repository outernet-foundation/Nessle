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
            Utility.CompleteProps(_text, props.style);
            props.value = props.value ?? new ValueObservable<string>(_text.text);

            AddBinding(
                Utility.BindTextStyle(props.style, _text),
                props.value.Subscribe(x => _text.text = x.currentValue)
            );
        }
    }
}