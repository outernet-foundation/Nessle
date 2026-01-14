using UnityEngine;
using ObserveThing;
using System;
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

        public static TAbstract GetOrAddComponent<TAbstract, TImplmentation>(this GameObject gameObject)
            where TAbstract : Component where TImplmentation : TAbstract
        {
            if (gameObject.TryGetComponent<TAbstract>(out var abs))
                return abs;

            return gameObject.AddComponent<TImplmentation>();
        }

        public static IListObservable<U> CreateDynamic<T, U>(this IListObservable<T> source, System.Func<T, U> create)
            where U : IControl => new CreateListObservable<T, U>(source, create);

        public static IListObservable<U> CreateDynamic<T, U>(this IListObservable<T> source, System.Func<T, IValueObservable<U>> create)
            where U : IControl => source.SelectDynamic(create).CreateDynamic(x => x);

        public static IValueObservable<U> CreateDynamic<T, U>(this IValueObservable<T> source, System.Func<T, U> create)
            where U : IControl => new CreateValueObservable<T, U>(source, create);

        public static IValueObservable<U> CreateDynamic<T, U>(this IValueObservable<T> source, System.Func<T, IValueObservable<U>> create)
            where U : IControl => source.SelectDynamic(create).CreateDynamic(x => x);

        public static IDisposable Subscribe(this ElementProps props, IControl control)
        {
            return new ComposedDisposable(
                props.name?.Subscribe(x => control.gameObject.name = x.currentValue),
                props.active?.Subscribe(x => control.gameObject.SetActive(x.currentValue)),
                props.bindings?.Subscribe(x =>
                {
                    if (x.operationType == OpType.Add)
                    {
                        control.AddBinding(x.element);
                    }
                    else if (x.operationType == OpType.Remove)
                    {
                        control.RemoveBinding(x.element);
                    }
                })
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
                props.anchorMin?.Subscribe(x => control.rectTransform.anchorMin = x.currentValue),
                props.anchorMax?.Subscribe(x => control.rectTransform.anchorMax = x.currentValue),
                props.offsetMin?.Subscribe(x => control.rectTransform.offsetMin = x.currentValue),
                props.offsetMax?.Subscribe(x => control.rectTransform.offsetMax = x.currentValue),
                props.anchoredPosition?.Subscribe(x => control.rectTransform.anchoredPosition = x.currentValue),
                props.sizeDelta?.Subscribe(x => control.rectTransform.sizeDelta = x.currentValue),
                props.pivot?.Subscribe(x => control.rectTransform.pivot = x.currentValue),
                props.position?.Subscribe(x => control.rectTransform.localPosition = x.currentValue),
                props.rotation?.Subscribe(x => control.rectTransform.localRotation = Quaternion.AngleAxis(x.currentValue, Vector3.forward)),
                props.scale?.Subscribe(x => control.rectTransform.localScale = new Vector3(x.currentValue.x, x.currentValue.y, control.rectTransform.localScale.z)),
                props.ignoreLayout?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().ignoreLayout = x.currentValue),
                props.minWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().minWidth = x.currentValue),
                props.minHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().minHeight = x.currentValue),
                props.preferredWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().preferredWidth = x.currentValue),
                props.preferredHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().preferredHeight = x.currentValue),
                props.flexibleWidth?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().flexibleWidth = x.currentValue ? 1 : 0),
                props.flexibleHeight?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().flexibleHeight = x.currentValue ? 1 : 0),
                props.layoutPriority?.Subscribe(x => control.gameObject.GetOrAddComponent<LayoutElement>().layoutPriority = x.currentValue),
                props.fitContentHorizontal?.Subscribe(x => control.gameObject.GetOrAddComponent<ContentSizeFitter>().horizontalFit = x.currentValue),
                props.fitContentVertical?.Subscribe(x => control.gameObject.GetOrAddComponent<ContentSizeFitter>().verticalFit = x.currentValue)
            );
        }

        public static IDisposable SubscribeAsChildren(this IListObservable<IControl> children, RectTransform parent)
        {
            return children?.Subscribe(x =>
            {
                if (x.operationType == OpType.Add)
                {
                    x.element.rectTransform.SetParent(parent, false);
                    x.element.rectTransform.SetSiblingIndex(x.index);
                }
                else if (x.operationType == OpType.Remove)
                {
                    x.element.rectTransform.SetParent(null, false);
                }
            });
        }
    }
}