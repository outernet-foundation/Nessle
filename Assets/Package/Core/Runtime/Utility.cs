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
            where U : IControl => source.ObservableCreate(x => new ValueObservable<U>(create(x)));

        public static IListObservable<U> ObservableCreate<T, U>(this IListObservable<T> source, Func<T, IValueObservable<U>> create) where U : IControl
        {
            return source
                .ObservableSelect(x => create(x).ObservableWithPrevious())
                .ObservableSelect(x =>
                {
                    x.previous?.Dispose();
                    return x.current;
                });
        }

        public static IValueObservable<U> ObservableCreate<T, U>(this IValueObservable<T> source, Func<T, U> create)
            where U : IControl => source.ObservableCreate(x => new ValueObservable<U>(create(x)));

        public static IValueObservable<U> ObservableCreate<T, U>(this IValueObservable<T> source, Func<T, IValueObservable<U>> create)
            where U : IControl
        {
            return source
                .ObservableSelect(x => create(x).ObservableWithPrevious())
                .ObservableSelect(x =>
                {
                    x.previous?.Dispose();
                    return x.current;
                });
        }

        public static IDisposable Subscribe(this ElementProps props, IControl control)
        {
            return new ComposedDisposable(
                props.name?.Subscribe(x => control.gameObject.name = x),
                props.active?.Subscribe(x => control.gameObject.SetActive(x)),
                props.bindings?.Subscribe(
                    onAdd: control.AddBinding,
                    onRemove: control.RemoveBinding
                )
            );
        }

        public static IDisposable Subscribe(this LayoutProps props, IControl control)
        {
            if (props.anchoredPosition != null)
            {
                if (props.position != null)
                    Debug.LogWarning("Both anchoredPosition and position values are set in ElementProps. This may cause errors.");

                if (props.offsetMin != null || props.offsetMax != null)
                    Debug.LogWarning("Both anchoredPosition and offset values are set in ElementProps. This may cause errors.");
            }

            if (props.sizeDelta != null)
            {
                if (props.offsetMin != null || props.offsetMax != null)
                    Debug.LogWarning("Both sizeDelta and offset values are set in ElementProps. This may cause errors.");
            }

            return new ComposedDisposable(
                props.anchorMin?.Subscribe(x => control.rectTransform.anchorMin = x),
                props.anchorMax?.Subscribe(x => control.rectTransform.anchorMax = x),
                props.offsetMin?.Subscribe(x => control.rectTransform.offsetMin = x),
                props.offsetMax?.Subscribe(x => control.rectTransform.offsetMax = x),
                props.anchoredPosition?.Subscribe(x => control.rectTransform.anchoredPosition = x),
                props.sizeDelta?.Subscribe(x => control.rectTransform.sizeDelta = x),
                props.pivot?.Subscribe(x => control.rectTransform.pivot = x),
                props.position?.Subscribe(x => control.rectTransform.localPosition = x),
                props.rotation?.Subscribe(x => control.rectTransform.localRotation = Quaternion.AngleAxis(x, Vector3.forward)),
                props.scale?.Subscribe(x => control.rectTransform.localScale = new Vector3(x.x, x.y, control.rectTransform.localScale.z)),
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

                    x.rectTransform.SetParent(parent, false);
                    x.rectTransform.SetSiblingIndex(siblingIndex);
                },
                onRemove: (index, x) =>
                {
                    if (x == null)
                        return;

                    childrenActual.RemoveAt(index);
                    x.rectTransform.SetParent(null, false);
                }
            );
        }
    }
}