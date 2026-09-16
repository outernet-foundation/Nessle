using UnityEngine;
using ObserveThing;
using TMPro;
using UnityEngine.TextCore;

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
        public IValueObservable<Vector4> margin;
        public IValueObservable<VertexSortingOrder> geometrySorting;
        public IValueObservable<bool> isScaleStatic;
        public IValueObservable<bool> richText;
        public IValueObservable<bool> raycastTarget;
        public IValueObservable<bool> maskable;
        public IValueObservable<bool> parseEscapeCharacters;
        public IValueObservable<bool> visibleDescender;
        public IValueObservable<bool> emojiFallbackSupport;
        public IValueObservable<TMP_SpriteAsset> spriteAsset;
        public IValueObservable<TMP_StyleSheet> styleAsset;
        public IValueObservable<OTL_FeatureTag> fontFeatures;
        public IValueObservable<bool> extraPadding;
    }

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextControl : Control<TextProps>
    {
        private TextMeshProUGUI _text;

        protected override void SetupInternal()
        {
            _text = GetComponent<TextMeshProUGUI>();

            AddBinding(
                props.value?.Subscribe(x => _text.text = x),
                props.style.font?.Subscribe(x => _text.font = x),
                props.style.textStyle?.Subscribe(x => _text.textStyle = x),
                props.style.styleSheet?.Subscribe(x => _text.styleSheet = x),
                props.style.color?.Subscribe(x => _text.color = x),
                props.style.fontStyle?.Subscribe(x => _text.fontStyle = x),
                props.style.fontSize?.Subscribe(x => _text.fontSize = x),
                props.style.fontWeight?.Subscribe(x => _text.fontWeight = x),
                props.style.enableAutoSizing?.Subscribe(x => _text.enableAutoSizing = x),
                props.style.fontSizeMin?.Subscribe(x => _text.fontSizeMin = x),
                props.style.fontSizeMax?.Subscribe(x => _text.fontSizeMax = x),
                props.style.horizontalAlignment?.Subscribe(x => _text.horizontalAlignment = x),
                props.style.verticalAlignment?.Subscribe(x => _text.verticalAlignment = x),
                props.style.characterSpacing?.Subscribe(x => _text.characterSpacing = x),
                props.style.wordSpacing?.Subscribe(x => _text.wordSpacing = x),
                props.style.lineSpacing?.Subscribe(x => _text.lineSpacing = x),
                props.style.lineSpacingAdjustment?.Subscribe(x => _text.lineSpacingAdjustment = x),
                props.style.paragraphSpacing?.Subscribe(x => _text.paragraphSpacing = x),
                props.style.characterWidthAdjustment?.Subscribe(x => _text.characterWidthAdjustment = x),
                props.style.textWrappingMode?.Subscribe(x => _text.textWrappingMode = x),
                props.style.overflowMode?.Subscribe(x => _text.overflowMode = x),
                props.style.horizontalMapping?.Subscribe(x => _text.horizontalMapping = x),
                props.style.verticalMapping?.Subscribe(x => _text.verticalMapping = x),
                props.style.outlineColor?.Subscribe(x => _text.outlineColor = x),
                props.style.outlineWidth?.Subscribe(x => _text.outlineWidth = x),
                props.style.margin?.Subscribe(x => _text.margin = x),
                props.style.geometrySorting?.Subscribe(x => _text.geometrySortingOrder = x),
                props.style.isScaleStatic?.Subscribe(x => _text.isTextObjectScaleStatic = x),
                props.style.richText?.Subscribe(x => _text.richText = x),
                props.style.raycastTarget?.Subscribe(x => _text.raycastTarget = x),
                props.style.maskable?.Subscribe(x => _text.maskable = x),
                props.style.parseEscapeCharacters?.Subscribe(x => _text.parseCtrlCharacters = x),
                props.style.visibleDescender?.Subscribe(x => _text.useMaxVisibleDescender = x),
                props.style.emojiFallbackSupport?.Subscribe(x => _text.emojiFallbackSupport = x),
                props.style.spriteAsset?.Subscribe(x => _text.spriteAsset = x),
                props.style.styleAsset?.Subscribe(x => _text.styleSheet = x),
                props.style.fontFeatures?.Subscribe(x => _text.fontFeatures = new() { x }),
                props.style.extraPadding?.Subscribe(x => _text.extraPadding = x)
            );
        }
    }
}