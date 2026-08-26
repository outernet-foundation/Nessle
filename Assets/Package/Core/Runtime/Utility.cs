using UnityEngine;
using ObserveThing;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

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

        public static IListObservable<U> ObservableCreate<T, U>(this IListObservable<T> source, Func<T, U> create)
            where U : IControl => source.ObservableCreate(x => new ObservableValue<U>(create(x)));

        public static IListObservable<U> ObservableCreate<T, U>(this IListObservable<T> source, Func<T, IValueObservable<U>> create) where U : IControl
        {
            return source
                .ObservableSelect(x => create(x).ObservableWithPrevious().ObservableThen(onNext: x => x.previous?.Dispose()))
                .ObservableThen(onRemove: (index, item) => item.current?.Dispose())
                .ObservableSelect(x => x.current);
        }

        public static IValueObservable<U> ObservableCreate<T, U>(this IValueObservable<T> source, Func<T, U> create)
            where U : IControl => source.ObservableCreate(x => new ObservableValue<U>(create(x)));

        public static IValueObservable<U> ObservableCreate<T, U>(this IValueObservable<T> source, Func<T, IValueObservable<U>> create)
            where U : IControl
        {
            return source
                .ObservableSelect(x => create(x))
                .ObservableWithPrevious()
                .ObservableThen(onNext: x => x.previous?.Dispose())
                .ObservableSelect(x => x.current);
        }

        public static IDisposable Subscribe(this ElementProps element, IControl control)
        {
            return new ComposedDisposable(
                element.name?.Subscribe(x => control.gameObject.name = x),
                element.active?.Subscribe(x => control.gameObject.SetActive(x)),
                element.destroyOnDispose?.Subscribe(x => control.destroyOnDispose = x),
                element.bindings
            );
        }

        public static IDisposable Subscribe(this LayoutProps layout, IControl control)
        {
            if (control.rectTransform == null && (
                layout.anchorMin != null ||
                layout.anchorMax != null ||
                layout.offsetMin != null ||
                layout.offsetMax != null ||
                layout.anchoredPosition != null ||
                layout.sizeDelta != null ||
                layout.pivot != null
            ))
            {
                Debug.LogWarning("RectTransform properties set in LayoutProps but control.rectTransform is null. These properties will be ignored.");
            }

            if (layout.anchoredPosition != null)
            {
                if (layout.localPosition != null)
                    Debug.LogWarning("Both anchoredPosition and position values are set in LayoutProps. This may cause errors.");

                if (layout.offsetMin != null || layout.offsetMax != null)
                    Debug.LogWarning("Both anchoredPosition and offset values are set in LayoutProps. This may cause errors.");
            }

            if (layout.sizeDelta != null)
            {
                if (layout.offsetMin != null || layout.offsetMax != null)
                    Debug.LogWarning("Both sizeDelta and offset values are set in LayoutProps. This may cause errors.");
            }

            var binding = new ComposedDisposable(
                layout.localPosition?.Subscribe(x => control.transform.localPosition = x),
                layout.localRotation?.Subscribe(x => control.transform.localRotation = x),
                layout.localScale?.Subscribe(x => control.transform.localScale = x),
                layout.ignoreLayout?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().ignoreLayout = x),
                layout.minWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().minWidth = x),
                layout.minHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().minHeight = x),
                layout.preferredWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().preferredWidth = x),
                layout.preferredHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().preferredHeight = x),
                layout.flexibleWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().flexibleWidth = x),
                layout.flexibleHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().flexibleHeight = x),
                layout.layoutPriority?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().layoutPriority = x),
                layout.fitContentHorizontal?.Subscribe(x => control.gameObject.GetOrAddComponent<ContentSizeFitter>().horizontalFit = x),
                layout.fitContentVertical?.Subscribe(x => control.gameObject.GetOrAddComponent<ContentSizeFitter>().verticalFit = x)
            );

            if (control.rectTransform == null)
                return binding;

            return new ComposedDisposable(
                binding,
                layout.anchorMin?.Subscribe(x => control.rectTransform.anchorMin = x),
                layout.anchorMax?.Subscribe(x => control.rectTransform.anchorMax = x),
                layout.offsetMin?.Subscribe(x => control.rectTransform.offsetMin = x),
                layout.offsetMax?.Subscribe(x => control.rectTransform.offsetMax = x),
                layout.anchoredPosition?.Subscribe(x => control.rectTransform.anchoredPosition = x),
                layout.sizeDelta?.Subscribe(x => control.rectTransform.sizeDelta = x),
                layout.pivot?.Subscribe(x => control.rectTransform.pivot = x)
            );
        }

        public static IDisposable SubscribeAsChildren(this IListObservable<IControl> children, Transform parent)
        {
            List<IControl> childrenActual = new List<IControl>();

            return children?.Subscribe(
                onAdd: (index, x) =>
                {
                    childrenActual.Insert(index, x);

                    if (x == null)
                        return;

                    int siblingIndex = 0;

                    for (int i = 0; i < index; i++)
                    {
                        if (childrenActual[i] != null)
                            siblingIndex++;
                    }

                    x.transform.SetParent(parent, false);
                    x.transform.SetSiblingIndex(siblingIndex);
                },
                onRemove: (index, x) =>
                {
                    if (x == null)
                        return;

                    childrenActual.RemoveAt(index);
                    x.transform.SetParent(null, false);
                }
            );
        }
    }
}