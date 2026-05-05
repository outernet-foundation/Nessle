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
                .ObservableForEach(onRemove: (index, item) => item.current?.Dispose())
                .ObservableSelect(x => x.current);
        }

        public static IValueObservable<U> ObservableCreate<T, U>(this IValueObservable<T> source, Func<T, U> create)
            where U : IControl => source.ObservableCreate(x => new ObservableValue<U>(create(x)));

        public static IValueObservable<U> ObservableCreate<T, U>(this IValueObservable<T> source, Func<T, IValueObservable<U>> create)
            where U : IControl
        {
            return source
                .ObservableSelect(x => create(x).ObservableWithPrevious().ObservableThen(onNext: x => x.previous?.Dispose()))
                .ObservableSelect(x => x.current);
        }

        public static IDisposable Subscribe(this ElementProps props, IControl control)
        {
            return new ComposedDisposable(
                props.name?.Subscribe(x => control.gameObject.name = x),
                props.active?.Subscribe(x => control.gameObject.SetActive(x)),
                props.destroyOnDispose?.Subscribe(x => control.destroyOnDispose = x),
                props.bindings?.Subscribe(
                    onAdd: control.AddBinding,
                    onRemove: control.RemoveBinding
                )
            );
        }

        public static IDisposable Subscribe(this LayoutProps props, IControl control)
        {
            if (control.rectTransform == null && (
                props.anchorMin != null ||
                props.anchorMax != null ||
                props.offsetMin != null ||
                props.offsetMax != null ||
                props.anchoredPosition != null ||
                props.sizeDelta != null ||
                props.pivot != null
            ))
            {
                Debug.LogWarning("RectTransform properties set in LayoutProps but control.rectTransform is null. These properties will be ignored.");
            }

            if (props.anchoredPosition != null)
            {
                if (props.position != null)
                    Debug.LogWarning("Both anchoredPosition and position values are set in LayoutProps. This may cause errors.");

                if (props.offsetMin != null || props.offsetMax != null)
                    Debug.LogWarning("Both anchoredPosition and offset values are set in LayoutProps. This may cause errors.");
            }

            if (props.sizeDelta != null)
            {
                if (props.offsetMin != null || props.offsetMax != null)
                    Debug.LogWarning("Both sizeDelta and offset values are set in LayoutProps. This may cause errors.");
            }

            var binding = new ComposedDisposable(
                props.position?.Subscribe(x => control.transform.localPosition = x),
                props.rotation?.Subscribe(x => control.transform.localRotation = Quaternion.AngleAxis(x, Vector3.forward)),
                props.scale?.Subscribe(x => control.transform.localScale = x),
                props.ignoreLayout?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().ignoreLayout = x),
                props.minWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().minWidth = x),
                props.minHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().minHeight = x),
                props.preferredWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().preferredWidth = x),
                props.preferredHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().preferredHeight = x),
                props.flexibleWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().flexibleWidth = x),
                props.flexibleHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().flexibleHeight = x),
                props.layoutPriority?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().layoutPriority = x),
                props.fitContentHorizontal?.Subscribe(x => control.gameObject.GetOrAddComponent<ContentSizeFitter>().horizontalFit = x),
                props.fitContentVertical?.Subscribe(x => control.gameObject.GetOrAddComponent<ContentSizeFitter>().verticalFit = x)
            );

            if (control.rectTransform == null)
                return binding;

            return new ComposedDisposable(
                binding,
                props.anchorMin?.Subscribe(x => control.rectTransform.anchorMin = x),
                props.anchorMax?.Subscribe(x => control.rectTransform.anchorMax = x),
                props.offsetMin?.Subscribe(x => control.rectTransform.offsetMin = x),
                props.offsetMax?.Subscribe(x => control.rectTransform.offsetMax = x),
                props.anchoredPosition?.Subscribe(x => control.rectTransform.anchoredPosition = x),
                props.sizeDelta?.Subscribe(x => control.rectTransform.sizeDelta = x),
                props.pivot?.Subscribe(x => control.rectTransform.pivot = x)
            );
        }

        public static IDisposable SubscribeAsChildren(this IListObservable<IControl> children, RectTransform parent)
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