using System;
using UnityEngine;
using ObserveThing;
using TMPro;

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

        public static TextStyleProps StylePropsFromText(TMP_Text text)
        {
            return new TextStyleProps(
                new ValueObservable<TMP_FontAsset>(text.font),
                new ValueObservable<TMP_Style>(text.textStyle),
                new ValueObservable<TMP_StyleSheet>(text.styleSheet),
                new ValueObservable<Color>(text.color),
                new ValueObservable<FontStyles>(text.fontStyle),
                new ValueObservable<float>(text.fontSize),
                new ValueObservable<FontWeight>(text.fontWeight),
                new ValueObservable<bool>(text.enableAutoSizing),
                new ValueObservable<float>(text.fontSizeMin),
                new ValueObservable<float>(text.fontSizeMax),
                new ValueObservable<HorizontalAlignmentOptions>(text.horizontalAlignment),
                new ValueObservable<VerticalAlignmentOptions>(text.verticalAlignment),
                new ValueObservable<float>(text.characterSpacing),
                new ValueObservable<float>(text.wordSpacing),
                new ValueObservable<float>(text.lineSpacing),
                new ValueObservable<float>(text.lineSpacingAdjustment),
                new ValueObservable<float>(text.paragraphSpacing),
                new ValueObservable<float>(text.characterWidthAdjustment),
                new ValueObservable<TextWrappingModes>(text.textWrappingMode),
                new ValueObservable<TextOverflowModes>(text.overflowMode),
                new ValueObservable<TextureMappingOptions>(text.horizontalMapping),
                new ValueObservable<TextureMappingOptions>(text.verticalMapping)
            );
        }

        public static IDisposable BindTextStyle(TextStyleProps props, TMP_Text text)
        {
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
    }
}