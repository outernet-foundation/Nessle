using System;
using System.Linq;
using ObserveThing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using TMP_ContentType = TMPro.TMP_InputField.ContentType;
using TMP_LineType = TMPro.TMP_InputField.LineType;
using ScrollbarDirection = UnityEngine.UI.Scrollbar.Direction;
using SliderDirection = UnityEngine.UI.Slider.Direction;
using ImageType = UnityEngine.UI.Image.Type;
using ImageFillMethod = UnityEngine.UI.Image.FillMethod;

namespace Nessle
{
    public static partial class UIBuilder
    {
        public static UIPrimitiveSet primitives { get; set; }

        public static Control Control(string identifier, params Type[] components)
            => new Control(identifier, components);

        public static Control<T> Control<T>(string identifier, T props, params Type[] components)
            => new Control<T>(identifier, props, components);

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

        private static void CopyFromText(TextStyleProps props, TMP_Text text)
        {
            props.font.From(text.font);
            props.textStyle.From(text.textStyle);
            props.styleSheet.From(text.styleSheet);
            props.color.From(text.color);
            props.fontStyle.From(text.fontStyle);
            props.fontSize.From(text.fontSize);
            props.fontWeight.From(text.fontWeight);
            props.enableAutoSizing.From(text.enableAutoSizing);
            props.fontSizeMin.From(text.fontSizeMin);
            props.fontSizeMax.From(text.fontSizeMax);
            props.horizontalAlignment.From(text.horizontalAlignment);
            props.verticalAlignment.From(text.verticalAlignment);
            props.characterSpacing.From(text.characterSpacing);
            props.wordSpacing.From(text.wordSpacing);
            props.lineSpacing.From(text.lineSpacing);
            props.lineSpacingAdjustment.From(text.lineSpacingAdjustment);
            props.paragraphSpacing.From(text.paragraphSpacing);
            props.characterWidthAdjustment.From(text.characterWidthAdjustment);
            props.textWrappingMode.From(text.textWrappingMode);
            props.overflowMode.From(text.overflowMode);
            props.horizontalMapping.From(text.horizontalMapping);
            props.verticalMapping.From(text.verticalMapping);
        }

        private static IDisposable BindTextStyle(TextStyleProps props, TMP_Text text, bool copyFromText = false)
        {
            if (copyFromText)
                CopyFromText(props, text);

            return new ComposedDisposable(
                props.font.Subscribe(x => text.font = x.currentValue),
                props.textStyle.Subscribe(x => text.textStyle = x.currentValue),
                props.styleSheet.Subscribe(x => text.styleSheet = x.currentValue),
                props.color.Subscribe(x => text.color = x.currentValue),
                props.fontStyle.Subscribe(x => text.fontStyle = x.currentValue),
                props.fontSize.Subscribe(x => text.fontSize = x.currentValue),
                props.fontWeight.Subscribe(x => text.fontWeight = x.currentValue),
                props.enableAutoSizing.Subscribe(x => text.enableAutoSizing = x.currentValue),
                props.fontSizeMin.Subscribe(x => text.fontSizeMin = x.currentValue),
                props.fontSizeMax.Subscribe(x => text.fontSizeMax = x.currentValue),
                props.horizontalAlignment.Subscribe(x => text.horizontalAlignment = x.currentValue),
                props.verticalAlignment.Subscribe(x => text.verticalAlignment = x.currentValue),
                props.characterSpacing.Subscribe(x => text.characterSpacing = x.currentValue),
                props.wordSpacing.Subscribe(x => text.wordSpacing = x.currentValue),
                props.lineSpacing.Subscribe(x => text.lineSpacing = x.currentValue),
                props.lineSpacingAdjustment.Subscribe(x => text.lineSpacingAdjustment = x.currentValue),
                props.paragraphSpacing.Subscribe(x => text.paragraphSpacing = x.currentValue),
                props.characterWidthAdjustment.Subscribe(x => text.characterWidthAdjustment = x.currentValue),
                props.textWrappingMode.Subscribe(x => text.textWrappingMode = x.currentValue),
                props.overflowMode.Subscribe(x => text.overflowMode = x.currentValue),
                props.horizontalMapping.Subscribe(x => text.horizontalMapping = x.currentValue),
                props.verticalMapping.Subscribe(x => text.verticalMapping = x.currentValue)
            );
        }

        public static Control<TextProps> Text(string identifier = "text", TextProps props = default, TextMeshProUGUI prefab = default, Action<Control<TextProps>> setup = default)
        {
            var text = UnityEngine.Object.Instantiate(prefab == null ? primitives.text : prefab);
            var control = new Control<TextProps>(identifier, props ?? new TextProps(), text.gameObject);

            control.AddBinding(
                control.props.value.Subscribe(x => text.text = x.currentValue),
                BindTextStyle(control.props.style, text, true)
            );

            setup?.Invoke(control);

            return control;
        }

        public class ImageProps : IDisposable, IColorProps
        {
            public ValueObservable<Sprite> sprite { get; } = new ValueObservable<Sprite>();
            public ValueObservable<Color> color { get; } = new ValueObservable<Color>();
            public ValueObservable<ImageType> imageType { get; } = new ValueObservable<ImageType>();
            public ValueObservable<bool> fillCenter { get; } = new ValueObservable<bool>(true);
            public ValueObservable<float> pixelsPerUnitMultiplier { get; } = new ValueObservable<float>(1);
            public ValueObservable<bool> raycastTarget { get; } = new ValueObservable<bool>(true);
            public ValueObservable<Vector4> raycastPadding { get; } = new ValueObservable<Vector4>();
            public ValueObservable<bool> useSpriteMesh { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> preserveAspect { get; } = new ValueObservable<bool>();
            public ValueObservable<int> fillOrigin { get; } = new ValueObservable<int>();
            public ValueObservable<ImageFillMethod> fillMethod { get; } = new ValueObservable<ImageFillMethod>();
            public ValueObservable<float> fillAmount { get; } = new ValueObservable<float>();

            public void Dispose()
            {
                sprite.Dispose();
                color.Dispose();
                imageType.Dispose();
                fillCenter.Dispose();
                pixelsPerUnitMultiplier.Dispose();
                raycastTarget.Dispose();
                raycastPadding.Dispose();
                useSpriteMesh.Dispose();
                preserveAspect.Dispose();
                fillOrigin.Dispose();
                fillMethod.Dispose();
                fillAmount.Dispose();
            }
        }

        public static Control<ImageProps> Image(string identifier = "image", ImageProps props = default, Image prefab = default)
        {
            var image = prefab == null ? new GameObject(identifier, typeof(Image)).GetComponent<Image>() : UnityEngine.Object.Instantiate(prefab);
            var control = new Control<ImageProps>(identifier, props ?? new ImageProps(), image.gameObject);

            control.AddBinding(
                control.props.sprite.Subscribe(x => image.sprite = x.currentValue),
                control.props.color.Subscribe(x => image.color = x.currentValue),
                control.props.imageType.Subscribe(x => image.type = x.currentValue),
                control.props.fillCenter.Subscribe(x => image.fillCenter = x.currentValue),
                control.props.pixelsPerUnitMultiplier.Subscribe(x => image.pixelsPerUnitMultiplier = x.currentValue),
                control.props.raycastTarget.Subscribe(x => image.raycastTarget = x.currentValue),
                control.props.raycastPadding.Subscribe(x => image.raycastPadding = x.currentValue),
                control.props.useSpriteMesh.Subscribe(x => image.useSpriteMesh = x.currentValue),
                control.props.preserveAspect.Subscribe(x => image.preserveAspect = x.currentValue),
                control.props.fillOrigin.Subscribe(x => image.fillOrigin = x.currentValue),
                control.props.fillMethod.Subscribe(x => image.fillMethod = x.currentValue),
                control.props.fillAmount.Subscribe(x => image.fillAmount = x.currentValue)
            );

            return control;
        }

        public class ButtonProps : IDisposable, IInteractableProps
        {
            public ValueObservable<Action> onClick { get; } = new ValueObservable<Action>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

            public void Dispose()
            {
                onClick.Dispose();
                interactable.Dispose();
            }
        }

        public static Control<ButtonProps> Button(string identifier = "button", ButtonProps props = default, Button prefab = default, Action<IControl<ButtonProps>> setup = default)
        {
            var button = UnityEngine.Object.Instantiate(prefab == null ? primitives.button : prefab);
            var content = (RectTransform)button.transform.Find("content");
            var control = new Control<ButtonProps>(identifier, props ?? new ButtonProps(), button.gameObject);

            button.onClick.AddListener(() => control.props.onClick.value?.Invoke());

            control.ChildParentOverride(content);
            control.AddBinding(control.props.interactable.Subscribe(x => button.interactable = x.currentValue));

            return control;
        }

        public class LayoutProps : IDisposable
        {
            public ValueObservable<RectOffset> padding { get; } = new ValueObservable<RectOffset>(new RectOffset());
            public ValueObservable<float> spacing { get; } = new ValueObservable<float>();
            public ValueObservable<TextAnchor> childAlignment { get; } = new ValueObservable<TextAnchor>();
            public ValueObservable<bool> reverseArrangement { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> childForceExpandHeight { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> childForceExpandWidth { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> childControlWidth { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> childControlHeight { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> childScaleWidth { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> childScaleHeight { get; } = new ValueObservable<bool>();

            public void Dispose()
            {
                padding.Dispose();
                spacing.Dispose();
                childAlignment.Dispose();
                reverseArrangement.Dispose();
                childForceExpandHeight.Dispose();
                childForceExpandWidth.Dispose();
                childControlWidth.Dispose();
                childControlHeight.Dispose();
                childScaleWidth.Dispose();
                childScaleHeight.Dispose();
            }
        }

        public static Control<LayoutProps> HorizontalLayout(string identifier = "horizontalLayout.layout", LayoutProps props = default, HorizontalLayoutGroup prefab = default)
        {
            var layout = UnityEngine.Object.Instantiate(prefab == null ? primitives.horizontalLayout : prefab);
            var control = new Control<LayoutProps>(identifier, props ?? new LayoutProps(), layout.gameObject);

            control.AddBinding(
                control.props.padding.Subscribe(x => layout.padding = x.currentValue),
                control.props.spacing.Subscribe(x => layout.spacing = x.currentValue),
                control.props.childAlignment.Subscribe(x => layout.childAlignment = x.currentValue),
                control.props.reverseArrangement.Subscribe(x => layout.reverseArrangement = x.currentValue),
                control.props.childForceExpandHeight.Subscribe(x => layout.childForceExpandHeight = x.currentValue),
                control.props.childForceExpandWidth.Subscribe(x => layout.childForceExpandWidth = x.currentValue),
                control.props.childControlWidth.Subscribe(x => layout.childControlWidth = x.currentValue),
                control.props.childControlHeight.Subscribe(x => layout.childControlHeight = x.currentValue),
                control.props.childScaleWidth.Subscribe(x => layout.childScaleWidth = x.currentValue),
                control.props.childScaleHeight.Subscribe(x => layout.childScaleHeight = x.currentValue)
            );

            return control;
        }

        public static Control<LayoutProps> VerticalLayout(string identifier = "verticalLayout.layout", LayoutProps props = default, VerticalLayoutGroup prefab = default)
        {
            var layout = UnityEngine.Object.Instantiate(prefab == null ? primitives.verticalLayout : prefab);
            var control = new Control<LayoutProps>(identifier, props ?? new LayoutProps(), layout.gameObject);

            control.AddBinding(
                control.props.padding.Subscribe(x => layout.padding = x.currentValue),
                control.props.spacing.Subscribe(x => layout.spacing = x.currentValue),
                control.props.childAlignment.Subscribe(x => layout.childAlignment = x.currentValue),
                control.props.reverseArrangement.Subscribe(x => layout.reverseArrangement = x.currentValue),
                control.props.childForceExpandHeight.Subscribe(x => layout.childForceExpandHeight = x.currentValue),
                control.props.childForceExpandWidth.Subscribe(x => layout.childForceExpandWidth = x.currentValue),
                control.props.childControlWidth.Subscribe(x => layout.childControlWidth = x.currentValue),
                control.props.childControlHeight.Subscribe(x => layout.childControlHeight = x.currentValue),
                control.props.childScaleWidth.Subscribe(x => layout.childScaleWidth = x.currentValue),
                control.props.childScaleHeight.Subscribe(x => layout.childScaleHeight = x.currentValue)
            );

            return control;
        }

        public class InputFieldProps : IDisposable, IValueProps<string>, IInteractableProps
        {
            public TextProps inputText { get; } = new TextProps();
            public TextProps placeholderText { get; } = new TextProps();
            public ValueObservable<TMP_ContentType> contentType { get; } = new ValueObservable<TMP_ContentType>();
            public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
            public ValueObservable<TMP_LineType> lineType { get; } = new ValueObservable<TMP_LineType>();
            public ValueObservable<int> characterLimit { get; } = new ValueObservable<int>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);
            public ValueObservable<Action<string>> onEndEdit { get; } = new ValueObservable<Action<string>>();

            ValueObservable<string> IValueProps<string>.value => inputText.value;

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
            }
        }

        public static Control<InputFieldProps> InputField(string identifier = "inputField", InputFieldProps props = default, TMP_InputField prefab = default)
        {
            var inputField = UnityEngine.Object.Instantiate(prefab == null ? primitives.inputField : prefab);
            var control = new Control<InputFieldProps>(identifier, props ?? new InputFieldProps(), inputField.gameObject);

            inputField.enabled = false;
            inputField.enabled = true;

            inputField.onValueChanged.AddListener(x => control.props.inputText.value.From(x));
            inputField.onEndEdit.AddListener(x => control.props.onEndEdit.value?.Invoke(x));

            control.AddBinding(
                control.props.inputText.value.Subscribe(x => inputField.text = x.currentValue),
                BindTextStyle(control.props.inputText.style, inputField.textComponent, true),
                control.props.contentType.Subscribe(x => inputField.contentType = x.currentValue),
                control.props.readOnly.Subscribe(x => inputField.readOnly = x.currentValue),
                control.props.lineType.Subscribe(x => inputField.lineType = x.currentValue),
                control.props.characterLimit.Subscribe(x => inputField.characterLimit = x.currentValue),
                control.props.interactable.Subscribe(x => inputField.interactable = x.currentValue)
            );

            if (inputField.placeholder != null && inputField.placeholder is TMP_Text placeholder)
            {
                control.AddBinding(
                    control.props.placeholderText.value.Subscribe(x => placeholder.text = x.currentValue),
                    BindTextStyle(control.props.placeholderText.style, placeholder, true)
                );
            }

            return control;
        }

        public class FloatFieldProps : IDisposable, IValueProps<float>, IInteractableProps
        {
            public ValueObservable<float> value { get; } = new ValueObservable<float>();
            public TextStyleProps inputTextStyle { get; } = new TextStyleProps();
            public TextProps placeholderText { get; } = new TextProps();
            public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

            public void Dispose()
            {
                value.Dispose();
                inputTextStyle.Dispose();
                placeholderText.Dispose();
                readOnly.Dispose();
                interactable.Dispose();
            }
        }

        public static Control<FloatFieldProps> FloatField(string identifier = "floatField", FloatFieldProps props = default, TMP_InputField prefab = default)
        {
            var inputField = UnityEngine.Object.Instantiate(prefab == null ? primitives.inputField : prefab);
            var control = new Control<FloatFieldProps>(identifier, props ?? new FloatFieldProps(), inputField.gameObject);

            inputField.enabled = false;
            inputField.enabled = true;
            inputField.contentType = TMP_ContentType.DecimalNumber;
            inputField.onEndEdit.AddListener(x => control.props.value.From(float.TryParse(x, out var value) ? value : 0));

            control.AddBinding(
                control.props.value.Subscribe(x => inputField.text = x.currentValue.ToString()),
                BindTextStyle(control.props.inputTextStyle, inputField.textComponent, true),
                control.props.readOnly.Subscribe(x => inputField.readOnly = x.currentValue),
                control.props.interactable.Subscribe(x => inputField.interactable = x.currentValue)
            );

            if (inputField.placeholder != null && inputField.placeholder is TMP_Text placeholder)
            {
                control.AddBinding(
                    control.props.placeholderText.value.Subscribe(x => placeholder.text = x.currentValue),
                    BindTextStyle(control.props.placeholderText.style, placeholder, true)
                );
            }

            return control;
        }

        public class IntFieldProps : IDisposable, IValueProps<int>, IInteractableProps
        {
            public ValueObservable<int> value { get; } = new ValueObservable<int>();
            public TextStyleProps inputTextStyle { get; } = new TextStyleProps();
            public TextProps placeholderText { get; } = new TextProps();
            public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

            public void Dispose()
            {
                value.Dispose();
                inputTextStyle.Dispose();
                placeholderText.Dispose();
                readOnly.Dispose();
                interactable.Dispose();
            }
        }

        public static Control<IntFieldProps> IntField(string identifier = "intField", IntFieldProps props = default, TMP_InputField prefab = default)
        {
            var inputField = UnityEngine.Object.Instantiate(prefab == null ? primitives.inputField : prefab);
            var control = new Control<IntFieldProps>(identifier, props ?? new IntFieldProps(), inputField.gameObject);

            inputField.enabled = false;
            inputField.enabled = true;
            inputField.contentType = TMP_ContentType.IntegerNumber;
            inputField.onEndEdit.AddListener(x => control.props.value.From(int.TryParse(x, out var value) ? value : 0));

            control.AddBinding(
                control.props.value.Subscribe(x => inputField.text = x.currentValue.ToString()),
                BindTextStyle(control.props.inputTextStyle, inputField.textComponent, true),
                control.props.readOnly.Subscribe(x => inputField.readOnly = x.currentValue),
                control.props.interactable.Subscribe(x => inputField.interactable = x.currentValue)
            );

            if (inputField.placeholder != null && inputField.placeholder is TMP_Text placeholder)
            {
                control.AddBinding(
                    control.props.placeholderText.value.Subscribe(x => placeholder.text = x.currentValue),
                    BindTextStyle(control.props.placeholderText.style, placeholder, true)
                );
            }

            return control;
        }

        public class DoubleFieldProps : IDisposable, IValueProps<double>, IInteractableProps
        {
            public ValueObservable<double> value { get; } = new ValueObservable<double>();
            public TextStyleProps inputTextStyle { get; } = new TextStyleProps();
            public TextProps placeholderText { get; } = new TextProps();
            public ValueObservable<bool> readOnly { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

            public void Dispose()
            {
                value.Dispose();
                inputTextStyle.Dispose();
                placeholderText.Dispose();
                readOnly.Dispose();
                interactable.Dispose();
            }
        }

        public static Control<DoubleFieldProps> DoubleField(string identifier = "doubleField", DoubleFieldProps props = default, TMP_InputField prefab = default)
        {
            var inputField = UnityEngine.Object.Instantiate(prefab == null ? primitives.inputField : prefab);
            var control = new Control<DoubleFieldProps>(identifier, props ?? new DoubleFieldProps(), inputField.gameObject);

            inputField.enabled = false;
            inputField.enabled = true;
            inputField.contentType = TMP_ContentType.DecimalNumber;
            inputField.onEndEdit.AddListener(x => control.props.value.From(double.TryParse(x, out var value) ? value : 0));

            control.AddBinding(
                control.props.value.Subscribe(x => inputField.text = x.currentValue.ToString()),
                BindTextStyle(control.props.inputTextStyle, inputField.textComponent, true),
                control.props.readOnly.Subscribe(x => inputField.readOnly = x.currentValue),
                control.props.interactable.Subscribe(x => inputField.interactable = x.currentValue)
            );

            if (inputField.placeholder != null && inputField.placeholder is TMP_Text placeholder)
            {
                control.AddBinding(
                    control.props.placeholderText.value.Subscribe(x => placeholder.text = x.currentValue),
                    BindTextStyle(control.props.placeholderText.style, placeholder, true)
                );
            }

            return control;
        }

        public static Control Space(string identifier = "space")
            => new Control(identifier);

        public class ScrollbarProps : IDisposable, IValueProps<float>, IInteractableProps
        {
            public ValueObservable<float> value { get; } = new ValueObservable<float>();
            public ValueObservable<ScrollbarDirection> direction { get; } = new ValueObservable<ScrollbarDirection>();
            public ValueObservable<float> size { get; } = new ValueObservable<float>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

            public void Dispose()
            {
                value.Dispose();
                direction.Dispose();
                size.Dispose();
                interactable.Dispose();
            }
        }

        public static Control<ScrollbarProps> Scrollbar(string identifier = "scrollbar", ScrollbarProps props = default, Scrollbar prefab = default)
        {
            var scrollbar = UnityEngine.Object.Instantiate(prefab == null ? primitives.scrollbar : prefab);
            var control = new Control<ScrollbarProps>(identifier, props ?? new ScrollbarProps(), scrollbar.gameObject);

            scrollbar.onValueChanged.AddListener(x => control.props.value.From(x));

            control.AddBinding(
                control.props.value.Subscribe(x => scrollbar.value = x.currentValue),
                control.props.direction.Subscribe(x => scrollbar.direction = x.currentValue),
                control.props.size.Subscribe(x => scrollbar.size = x.currentValue),
                control.props.interactable.Subscribe(x => scrollbar.interactable = x.currentValue)
            );

            return control;
        }

        public class ScrollRectProps : IDisposable, IValueProps<Vector2>
        {
            public ValueObservable<Vector2> value { get; } = new ValueObservable<Vector2>(new Vector2(0, 1));
            public ValueObservable<bool> horizontal { get; } = new ValueObservable<bool>(true);
            public ValueObservable<bool> vertical { get; } = new ValueObservable<bool>(true);
            public ValueObservable<IControl> content { get; } = new ValueObservable<IControl>();

            public void Dispose()
            {
                value.Dispose();
                horizontal.Dispose();
                vertical.Dispose();
                content.Dispose();
            }
        }

        public static IControl<ScrollRectProps> ScrollRect(Action<IControl<ScrollRectProps>> setup)
            => ScrollRect(props: default, setup: setup);

        public static Control<ScrollRectProps> ScrollRect(string identifier = "scrollRect", ScrollRectProps props = default, ScrollRect prefab = default, Action<IControl<ScrollRectProps>> setup = default)
        {
            var scrollRect = UnityEngine.Object.Instantiate(prefab == null ? primitives.scrollRect : prefab);
            var control = new Control<ScrollRectProps>(identifier, props ?? new ScrollRectProps(), scrollRect.gameObject);

            scrollRect.onValueChanged.AddListener(x => control.props.value.From(x));

            control.AddBinding(control.props.value.Subscribe(x =>
            {
                if (scrollRect.content == null)
                    return;

                scrollRect.normalizedPosition = x.currentValue;
            }));

            control.AddBinding(control.props.horizontal.Subscribe(x =>
            {
                if (scrollRect.content == null)
                    return;

                scrollRect.horizontal = x.currentValue;
            }));

            control.AddBinding(control.props.vertical.Subscribe(x =>
            {
                if (scrollRect.content == null)
                    return;

                scrollRect.vertical = x.currentValue;
            }));

            control.AddBinding(control.props.content.Subscribe(x =>
            {
                x.previousValue?.parent.From(default(IControl));

                if (x.currentValue == null)
                    return;

                x.currentValue.parent.From(control);
                scrollRect.content = x.currentValue.transform;
                x.currentValue.SetPivot(new Vector2(0, 1));
                x.currentValue.AnchorToTop();

                scrollRect.normalizedPosition = control.props.value.value;
                scrollRect.horizontal = control.props.horizontal.value;
                scrollRect.vertical = control.props.vertical.value;
            }));

            control.ChildParentOverride(scrollRect.viewport);

            return control;
        }

        public class DropdownProps : IDisposable, IValueProps<int>, IInteractableProps
        {
            public ValueObservable<int> value { get; } = new ValueObservable<int>();
            public ValueObservable<bool> allowMultiselect { get; } = new ValueObservable<bool>();
            public ListObservable<string> options { get; } = new ListObservable<string>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>(true);

            public TextStyleProps captionTextStyle { get; } = new TextStyleProps();
            public TextStyleProps itemTextStyle { get; } = new TextStyleProps();

            public void Dispose()
            {
                value.Dispose();
                allowMultiselect.Dispose();
                options.Dispose();
                interactable.Dispose();
                captionTextStyle.Dispose();
                itemTextStyle.Dispose();
            }
        }

        public static Control<DropdownProps> Dropdown(string identifier = "dropdown", DropdownProps props = default, TMP_Dropdown prefab = default)
        {
            var dropdown = UnityEngine.Object.Instantiate(prefab == null ? primitives.dropdown : prefab);
            var control = new Control<DropdownProps>(identifier, props ?? new DropdownProps(), dropdown.gameObject);

            dropdown.onValueChanged.AddListener(x => control.props.value.From(x));

            control.AddBinding(
                control.props.value.Subscribe(x => dropdown.value = x.currentValue),
                control.props.allowMultiselect.Subscribe(x => dropdown.MultiSelect = x.currentValue),
                control.props.options.Subscribe(_ => dropdown.options = control.props.options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                control.props.interactable.Subscribe(x => dropdown.interactable = x.currentValue),
                BindTextStyle(control.props.captionTextStyle, dropdown.captionText, true),
                BindTextStyle(control.props.itemTextStyle, dropdown.itemText, true)
            );

            return control;
        }

        public class ToggleProps : IDisposable, IValueProps<bool>, IInteractableProps
        {
            public ValueObservable<bool> value { get; } = new ValueObservable<bool>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>();

            public void Dispose()
            {
                value.Dispose();
                interactable.Dispose();
            }
        }

        public static Control<ToggleProps> Toggle(string identifier = "toggle", ToggleProps props = default, Toggle prefab = default)
        {
            var toggle = UnityEngine.Object.Instantiate(prefab == null ? primitives.toggle : prefab);
            var control = new Control<ToggleProps>(identifier, props ?? new ToggleProps(), toggle.gameObject);

            toggle.onValueChanged.AddListener(x => control.props.value.From(x));

            control.props.value.From(toggle.isOn);
            control.props.interactable.From(toggle.interactable);

            control.AddBinding(
                control.props.value.Subscribe(x => toggle.isOn = x.currentValue),
                control.props.interactable.Subscribe(x => toggle.interactable = x.currentValue)
            );

            return control;
        }

        public class SliderProps : IDisposable, IValueProps<float>, IInteractableProps
        {
            public ValueObservable<float> value { get; } = new ValueObservable<float>();
            public ValueObservable<float> minValue { get; } = new ValueObservable<float>();
            public ValueObservable<float> maxValue { get; } = new ValueObservable<float>();
            public ValueObservable<bool> wholeNumbers { get; } = new ValueObservable<bool>();
            public ValueObservable<SliderDirection> direction { get; } = new ValueObservable<SliderDirection>();
            public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>();

            public void Dispose()
            {
                value.Dispose();
                minValue.Dispose();
                maxValue.Dispose();
                wholeNumbers.Dispose();
                direction.Dispose();
                interactable.Dispose();
            }
        }

        public static Control<SliderProps> Slider(string identifier = "slider", SliderProps props = default, Slider prefab = default)
        {
            var slider = UnityEngine.Object.Instantiate(prefab == null ? primitives.slider : prefab);
            var control = new Control<SliderProps>(identifier, props ?? new SliderProps(), slider.gameObject);

            slider.onValueChanged.AddListener(x => control.props.value.From(x));

            control.AddBinding(
                control.props.value.Subscribe(x => slider.value = x.currentValue),
                control.props.minValue.Subscribe(x => slider.minValue = x.currentValue),
                control.props.maxValue.Subscribe(x => slider.maxValue = x.currentValue),
                control.props.wholeNumbers.Subscribe(x => slider.wholeNumbers = x.currentValue),
                control.props.direction.Subscribe(x => slider.direction = x.currentValue),
                control.props.interactable.Subscribe(x => slider.interactable = x.currentValue)
            );

            return control;
        }
    }
}
