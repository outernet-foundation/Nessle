using System;
using System.Collections.Generic;
using ObserveThing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FitMode = UnityEngine.UI.ContentSizeFitter.FitMode;

namespace Nessle
{
    public static class ControlExtensions
    {
        public static T Style<T>(this T constructor, Action<T> setup)
            where T : IControlConstructor
        {
            setup(constructor);
            return constructor;
        }

        public static T Children<T>(this T constructor, params IControl[] children)
            where T : IControl
        {
            constructor.children.From(children);
            return constructor;
        }

        public static T Children<T>(this T constructor, IEnumerable<IControl> children)
            where T : IControl
        {
            constructor.children.From(children);
            return constructor;
        }

        public static T Children<T>(this T constructor, IListObservable<IControl> children)
            where T : IControl
        {
            constructor.children.From(children);
            return constructor;
        }

        public static T Active<T>(this T constructor, IValueObservable<bool> active)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(active.Subscribe(x => constructor.control.gameObject.SetActive(x.currentValue)));
            return constructor;
        }

        public static T Selected<T>(this T constructor, IValueObservable<bool> selected)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(selected.Subscribe(x =>
            {
                if (x.currentValue)
                {
                    EventSystem.current.SetSelectedGameObject(constructor.control.gameObject);
                }
                else if (!EventSystem.current.alreadySelecting &&
                    EventSystem.current.currentSelectedGameObject == constructor.control.gameObject)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }));
            return constructor;
        }

        public static T Active<T>(this T constructor, bool active)
            where T : IControlConstructor
        {
            constructor.control.gameObject.SetActive(active);
            return constructor;
        }

        public static T FillParent<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(0, 0);
            constructor.control.rectTransform.anchorMax = new Vector2(1, 1);
            constructor.control.rectTransform.offsetMin = new Vector2(0, 0);
            constructor.control.rectTransform.offsetMax = new Vector2(0, 0);
            return constructor;
        }

        public static T FillParentWidth<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(0, constructor.control.rectTransform.anchorMin.y);
            constructor.control.rectTransform.anchorMax = new Vector2(1, constructor.control.rectTransform.anchorMax.y);
            constructor.control.rectTransform.offsetMin = new Vector2(0, constructor.control.rectTransform.offsetMin.y);
            constructor.control.rectTransform.offsetMax = new Vector2(0, constructor.control.rectTransform.offsetMax.y);
            return constructor;
        }

        public static T FillParentHeight<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(constructor.control.rectTransform.anchorMin.x, 0);
            constructor.control.rectTransform.anchorMax = new Vector2(constructor.control.rectTransform.anchorMax.x, 1);
            constructor.control.rectTransform.offsetMin = new Vector2(constructor.control.rectTransform.offsetMin.x, 0);
            constructor.control.rectTransform.offsetMax = new Vector2(constructor.control.rectTransform.offsetMax.x, 0);
            return constructor;
        }

        public static T AnchorToTop<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(constructor.control.rectTransform.anchorMin.x, 1);
            constructor.control.rectTransform.anchorMax = new Vector2(constructor.control.rectTransform.anchorMax.x, 1);
            return constructor;
        }

        public static T AnchorToBottom<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(constructor.control.rectTransform.anchorMin.x, 0);
            constructor.control.rectTransform.anchorMax = new Vector2(constructor.control.rectTransform.anchorMax.x, 0);
            return constructor;
        }

        public static T AnchorToLeft<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(0, constructor.control.rectTransform.anchorMin.y);
            constructor.control.rectTransform.anchorMax = new Vector2(0, constructor.control.rectTransform.anchorMax.y);
            return constructor;
        }

        public static T AnchorToRight<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(1, constructor.control.rectTransform.anchorMin.y);
            constructor.control.rectTransform.anchorMax = new Vector2(1, constructor.control.rectTransform.anchorMax.y);
            return constructor;
        }

        public static T AnchorToTopLeft<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(0, 1);
            constructor.control.rectTransform.anchorMax = new Vector2(0, 1);
            return constructor;
        }

        public static T AnchorToTopRight<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(1, 1);
            constructor.control.rectTransform.anchorMax = new Vector2(1, 1);
            return constructor;
        }

        public static T AnchorToBottomLeft<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(0, 0);
            constructor.control.rectTransform.anchorMax = new Vector2(0, 0);
            return constructor;
        }

        public static T AnchorToBottomRight<T>(this T constructor)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = new Vector2(1, 0);
            constructor.control.rectTransform.anchorMax = new Vector2(1, 0);
            return constructor;
        }

        public static T SetPivot<T>(this T constructor, Vector2 pivot)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.pivot = pivot;
            return constructor;
        }

        public static T SetPivot<T>(this T constructor, IValueObservable<Vector2> pivot)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(pivot.Subscribe(x => constructor.control.rectTransform.pivot = x.currentValue));
            return constructor;
        }

        public static T LocalPosition<T>(this T constructor, Vector3 localPosition)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.localPosition = localPosition;
            return constructor;
        }

        public static T LocalPosition<T>(this T constructor, IValueObservable<Vector2> localPosition)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(localPosition.Subscribe(x => constructor.control.rectTransform.localPosition = x.currentValue));
            return constructor;
        }

        public static T Anchor<T>(this T constructor, Vector2 anchor)
            where T : IControlConstructor
        {
            constructor.AnchorMin(anchor);
            constructor.AnchorMax(anchor);
            return constructor;
        }

        public static T Anchor<T>(this T constructor, IValueObservable<Vector2> anchor)
            where T : IControlConstructor
        {
            constructor.AnchorMin(anchor);
            constructor.AnchorMax(anchor);
            return constructor;
        }

        public static T AnchorMin<T>(this T constructor, Vector2 anchorMin)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMin = anchorMin;
            return constructor;
        }

        public static T AnchorMin<T>(this T constructor, IValueObservable<Vector2> anchorMin)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(anchorMin.Subscribe(x => constructor.control.rectTransform.anchorMin = x.currentValue));
            return constructor;
        }

        public static T AnchorMax<T>(this T constructor, Vector2 anchorMax)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchorMax = anchorMax;
            return constructor;
        }

        public static T AnchorMax<T>(this T constructor, IValueObservable<Vector2> anchorMax)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(anchorMax.Subscribe(x => constructor.control.rectTransform.anchorMax = x.currentValue));
            return constructor;
        }

        public static T OffsetMin<T>(this T constructor, Vector2 offsetMin)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.offsetMin = offsetMin;
            return constructor;
        }

        public static T OffsetMin<T>(this T constructor, IValueObservable<Vector2> offsetMin)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(offsetMin.Subscribe(x => constructor.control.rectTransform.offsetMin = x.currentValue));
            return constructor;
        }

        public static T OffsetMax<T>(this T constructor, Vector2 offsetMax)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.offsetMax = offsetMax;
            return constructor;
        }

        public static T OffsetMax<T>(this T constructor, IValueObservable<Vector2> offsetMax)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(offsetMax.Subscribe(x => constructor.control.rectTransform.offsetMax = x.currentValue));
            return constructor;
        }

        public static T AnchoredPosition<T>(this T constructor, Vector2 anchoredPosition)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.anchoredPosition = anchoredPosition;
            return constructor;
        }

        public static T AnchoredPosition<T>(this T constructor, IValueObservable<Vector2> anchoredPosition)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(anchoredPosition.Subscribe(x => constructor.control.rectTransform.anchoredPosition = x.currentValue));
            return constructor;
        }

        public static T SizeDelta<T>(this T constructor, Vector2 sizeDelta)
            where T : IControlConstructor
        {
            constructor.control.rectTransform.sizeDelta = sizeDelta;
            return constructor;
        }

        public static T SizeDelta<T>(this T constructor, IValueObservable<Vector2> sizeDelta)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(sizeDelta.Subscribe(x => constructor.control.rectTransform.sizeDelta = x.currentValue));
            return constructor;
        }

        public static T IgnoreLayout<T>(this T constructor, IValueObservable<bool> ignoreLayout)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(ignoreLayout.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().ignoreLayout = x.currentValue));
            return constructor;
        }

        public static T IgnoreLayout<T>(this T constructor, bool ignoreLayout)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().ignoreLayout = ignoreLayout;
            return constructor;
        }

        public static T MinWidth<T>(this T constructor, IValueObservable<float> minWidth)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(minWidth.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().minWidth = x.currentValue));
            return constructor;
        }

        public static T MinWidth<T>(this T constructor, float minWidth)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().minWidth = minWidth;
            return constructor;
        }

        public static T MinHeight<T>(this T constructor, IValueObservable<float> minHeight)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(minHeight.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().minHeight = x.currentValue));
            return constructor;
        }

        public static T MinHeight<T>(this T constructor, float minHeight)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().minHeight = minHeight;
            return constructor;
        }

        public static T PreferredWidth<T>(this T constructor, IValueObservable<float> preferredWidth)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(preferredWidth.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().preferredWidth = x.currentValue));
            return constructor;
        }

        public static T PreferredWidth<T>(this T constructor, float preferredWidth)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().preferredWidth = preferredWidth;
            return constructor;
        }

        public static T PreferredHeight<T>(this T constructor, IValueObservable<float> preferredHeight)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(preferredHeight.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().preferredHeight = x.currentValue));
            return constructor;
        }

        public static T PreferredHeight<T>(this T constructor, float preferredHeight)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().preferredHeight = preferredHeight;
            return constructor;
        }

        public static T FlexibleWidth<T>(this T constructor, IValueObservable<bool> flexibleWidth)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(flexibleWidth.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().flexibleWidth = x.currentValue ? 1 : -1));
            return constructor;
        }

        public static T FlexibleWidth<T>(this T constructor, bool flexibleWidth)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().flexibleWidth = flexibleWidth ? 1 : -1;
            return constructor;
        }

        public static T FlexibleHeight<T>(this T constructor, IValueObservable<bool> flexibleHeight)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(flexibleHeight.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().flexibleHeight = x.currentValue ? 1 : -1));
            return constructor;
        }

        public static T FlexibleHeight<T>(this T constructor, bool flexibleHeight)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().flexibleHeight = flexibleHeight ? 1 : -1;
            return constructor;
        }

        public static T LayoutPriority<T>(this T constructor, IValueObservable<int> layoutPriority)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(layoutPriority.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<LayoutElement>().layoutPriority = x.currentValue));
            return constructor;
        }

        public static T LayoutPriority<T>(this T constructor, int layoutPriority)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<LayoutElement>().layoutPriority = layoutPriority;
            return constructor;
        }

        public static T FitContentVertical<T>(this T constructor, IValueObservable<FitMode> fitContentVertical)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(fitContentVertical.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<ContentSizeFitter>().verticalFit = x.currentValue));
            return constructor;
        }

        public static T FitContentVertical<T>(this T constructor, FitMode fitContentVertical)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<ContentSizeFitter>().verticalFit = fitContentVertical;
            return constructor;
        }

        public static T FitContentHorizontal<T>(this T constructor, IValueObservable<FitMode> fitContentHorizontal)
            where T : IControlConstructor
        {
            constructor.control.AddBinding(fitContentHorizontal.Subscribe(x => constructor.control.gameObject.GetOrAddComponent<ContentSizeFitter>().horizontalFit = x.currentValue));
            return constructor;
        }

        public static T FitContentHorizontal<T>(this T constructor, FitMode fitContentHorizontal)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<ContentSizeFitter>().horizontalFit = fitContentHorizontal;
            return constructor;
        }

        public static T OnHoverEntered<T>(this T constructor, Action<PointerEventData> onHoverEntered)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<PointerEnterHandler>().onReceivedEvent += onHoverEntered;
            return constructor;
        }

        public static T OnHoverExited<T>(this T constructor, Action<PointerEventData> onHoverExited)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<PointerExitHandler>().onReceivedEvent += onHoverExited;
            return constructor;
        }

        public static T OnPointerDown<T>(this T constructor, Action<PointerEventData> onPointerDown)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<PointerDownHandler>().onReceivedEvent += onPointerDown;
            return constructor;
        }

        public static T OnPointerUp<T>(this T constructor, Action<PointerEventData> onPointerUp)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<PointerUpHandler>().onReceivedEvent += onPointerUp;
            return constructor;
        }

        public static T OnPointerClick<T>(this T constructor, Action<PointerEventData> onPointerClick)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<PointerClickHandler>().onReceivedEvent += onPointerClick;
            return constructor;
        }

        public static T OnDragStarted<T>(this T constructor, Action<PointerEventData> onDragStarted)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<BeginDragHandler>().onReceivedEvent += onDragStarted;
            return constructor;
        }

        public static T OnDrag<T>(this T constructor, Action<PointerEventData> onDrag)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<DragHandler>().onReceivedEvent += onDrag;
            return constructor;
        }

        public static T OnDragEnded<T>(this T constructor, Action<PointerEventData> onDragEnded)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<EndDragHandler>().onReceivedEvent += onDragEnded;
            return constructor;
        }

        public static T OnSelect<T>(this T constructor, Action<BaseEventData> onSelect)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<SelectHandler>().onReceivedEvent += onSelect;
            return constructor;
        }

        public static T OnDeselect<T>(this T constructor, Action<BaseEventData> onDeselect)
            where T : IControlConstructor
        {
            constructor.control.gameObject.GetOrAddComponent<DeselectHandler>().onReceivedEvent += onDeselect;
            return constructor;
        }
    }
}
