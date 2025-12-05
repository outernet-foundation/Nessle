using System;
using System.Linq;
using UnityEngine;
using ObserveThing;
using TMPro;
using UnityEngine.UI;

namespace Nessle
{
    public static class Utility
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject)
            where T : Component
        {
            if (!gameObject.TryGetComponent<T>(out var component))
                component = gameObject.AddComponent<T>();

            return component;
        }

        public static Color Alpha(this Color color, float alpha)
            => new Color(color.r, color.g, color.b, alpha);

        public static void From<T>(this ValueObservable<string> observable, IValueObservable<T> source)
            => observable.From(source.SelectDynamic(x => x.ToString()));

        public static void AlphaFrom<T>(this ValueObservable<Color> observable, float alpha)
            => observable.From(observable.value.Alpha(alpha));

        public static void AlphaFrom<T>(this ValueObservable<Color> observable, IValueObservable<float> alpha)
            => observable.From(alpha.SelectDynamic(x => observable.value.Alpha(x)));

        public static IListObservable<U> CreateDynamic<T, U>(this IListObservable<T> source, System.Func<T, U> create)
            where U : IControl => new CreateListObservable<U>(source.SelectDynamic(create));

        public static IListObservable<U> CreateDynamic<T, U>(this IListObservable<T> source, System.Func<T, IValueObservable<U>> create)
            where U : IControl => new CreateListObservable<U>(source.SelectDynamic(create));

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

        public static IDisposable BindTextStyle(TextStyleProps props, TMP_Text text, bool copyFromText = false)
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

        public static void CopyFromText(TextProps props, TMP_Text text)
        {
            props.value.From(text.text);
            CopyFromText(props.style, text);
        }

        public static IDisposable BindText(TextProps props, TMP_Text text, bool copyFromText = false)
        {
            if (copyFromText)
                CopyFromText(props, text);

            return new ComposedDisposable(
                props.value.Subscribe(x => text.text = x.currentValue),
                BindTextStyle(props.style, text, copyFromText)
            );
        }

        public static void CopyFromLayout(LayoutProps props, HorizontalOrVerticalLayoutGroup layout)
        {
            props.padding.From(layout.padding);
            props.spacing.From(layout.spacing);
            props.childAlignment.From(layout.childAlignment);
            props.reverseArrangement.From(layout.reverseArrangement);
            props.childForceExpandHeight.From(layout.childForceExpandHeight);
            props.childForceExpandWidth.From(layout.childForceExpandWidth);
            props.childControlWidth.From(layout.childControlWidth);
            props.childControlHeight.From(layout.childControlHeight);
            props.childScaleWidth.From(layout.childScaleWidth);
            props.childScaleHeight.From(layout.childScaleHeight);
        }

        public static IDisposable BindLayout(LayoutProps props, HorizontalOrVerticalLayoutGroup layout, bool copyFromLayout = false)
        {
            if (copyFromLayout)
                CopyFromLayout(props, layout);

            return new ComposedDisposable(
                props.padding.Subscribe(x => layout.padding = x.currentValue),
                props.spacing.Subscribe(x => layout.spacing = x.currentValue),
                props.childAlignment.Subscribe(x => layout.childAlignment = x.currentValue),
                props.reverseArrangement.Subscribe(x => layout.reverseArrangement = x.currentValue),
                props.childForceExpandHeight.Subscribe(x => layout.childForceExpandHeight = x.currentValue),
                props.childForceExpandWidth.Subscribe(x => layout.childForceExpandWidth = x.currentValue),
                props.childControlWidth.Subscribe(x => layout.childControlWidth = x.currentValue),
                props.childControlHeight.Subscribe(x => layout.childControlHeight = x.currentValue),
                props.childScaleWidth.Subscribe(x => layout.childScaleWidth = x.currentValue),
                props.childScaleHeight.Subscribe(x => layout.childScaleHeight = x.currentValue)
            );
        }

        public static void CopyFromDropdown(DropdownProps props, TMP_Dropdown dropdown)
        {
            props.value.From(dropdown.value);
            props.allowMultiselect.From(dropdown.MultiSelect);
            props.options.From(dropdown.options.Select(x => x.text));
            props.interactable.From(dropdown.interactable);
            CopyFromText(props.captionTextStyle, dropdown.captionText);
            CopyFromText(props.itemTextStyle, dropdown.itemText);
        }

        public static IDisposable BindDropdown(DropdownProps props, TMP_Dropdown dropdown, bool copyFromDropdown = false)
        {
            if (copyFromDropdown)
                CopyFromDropdown(props, dropdown);

            return new ComposedDisposable(
                props.value.Subscribe(x => dropdown.value = x.currentValue),
                props.allowMultiselect.Subscribe(x => dropdown.MultiSelect = x.currentValue),
                props.options.Subscribe(_ => dropdown.options = props.options.Select(x => new TMP_Dropdown.OptionData() { text = x }).ToList()),
                props.interactable.Subscribe(x => dropdown.interactable = x.currentValue),
                BindTextStyle(props.captionTextStyle, dropdown.captionText, true),
                BindTextStyle(props.itemTextStyle, dropdown.itemText, true)
            );
        }

        public static void CopyFromButton(ButtonProps props, Button button, Image background = default)
        {
            props.interactable.From(button.interactable);

            if (background != null)
                CopyFromImage(props.background, background);
        }

        public static IDisposable BindButton(ButtonProps props, Button button, Image background = default, bool copyFromButton = false)
        {
            if (copyFromButton)
                CopyFromButton(props, button, background);

            if (background != null)
            {
                return new ComposedDisposable(
                    props.interactable.Subscribe(x => button.interactable = x.currentValue),
                    BindImage(props.background, background)
                );
            }

            return props.interactable.Subscribe(x => button.interactable = x.currentValue);
        }

        public static void CopyFromImage(ImageProps props, Image image)
        {
            props.sprite.From(image.sprite);
            props.color.From(image.color);
            props.imageType.From(image.type);
            props.fillCenter.From(image.fillCenter);
            props.pixelsPerUnitMultiplier.From(image.pixelsPerUnitMultiplier);
            props.raycastTarget.From(image.raycastTarget);
            props.raycastPadding.From(image.raycastPadding);
            props.useSpriteMesh.From(image.useSpriteMesh);
            props.preserveAspect.From(image.preserveAspect);
            props.fillOrigin.From(image.fillOrigin);
            props.fillMethod.From(image.fillMethod);
            props.fillAmount.From(image.fillAmount);
        }

        public static IDisposable BindImage(ImageProps props, Image image, bool copyFromImage = false)
        {
            if (copyFromImage)
                CopyFromImage(props, image);

            return new ComposedDisposable(
                props.sprite.Subscribe(x => image.sprite = x.currentValue),
                props.color.Subscribe(x => image.color = x.currentValue),
                props.imageType.Subscribe(x => image.type = x.currentValue),
                props.fillCenter.Subscribe(x => image.fillCenter = x.currentValue),
                props.pixelsPerUnitMultiplier.Subscribe(x => image.pixelsPerUnitMultiplier = x.currentValue),
                props.raycastTarget.Subscribe(x => image.raycastTarget = x.currentValue),
                props.raycastPadding.Subscribe(x => image.raycastPadding = x.currentValue),
                props.useSpriteMesh.Subscribe(x => image.useSpriteMesh = x.currentValue),
                props.preserveAspect.Subscribe(x => image.preserveAspect = x.currentValue),
                props.fillOrigin.Subscribe(x => image.fillOrigin = x.currentValue),
                props.fillMethod.Subscribe(x => image.fillMethod = x.currentValue),
                props.fillAmount.Subscribe(x => image.fillAmount = x.currentValue)
            );
        }
    }
}