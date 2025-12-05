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

        public static void CopyFromText(TextStyleProps props, TMP_Text text)
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
    }
}