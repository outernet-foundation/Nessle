using System;
using UnityEngine;
using ObserveThing;
using TMPro;
using UnityEngine.UI;

using ImageType = UnityEngine.UI.Image.Type;
using ImageFillMethod = UnityEngine.UI.Image.FillMethod;

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

        public static TAbstract GetOrAddComponent<TAbstract, TImplmentation>(this GameObject gameObject)
            where TAbstract : Component where TImplmentation : TAbstract
        {
            if (gameObject.TryGetComponent<TAbstract>(out var abs))
                return abs;

            return gameObject.AddComponent<TImplmentation>();
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
    }
}