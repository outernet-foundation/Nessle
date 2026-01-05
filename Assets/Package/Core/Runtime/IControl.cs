using System;
using UnityEngine;
using ObserveThing;
using FitMode = UnityEngine.UI.ContentSizeFitter.FitMode;

namespace Nessle
{
    public struct ElementProps
    {
        public IValueObservable<string> name;
        public IValueObservable<bool> active;
        public IListObservable<IDisposable> bindings;
    }

    public struct LayoutProps
    {
        public IValueObservable<Vector2> anchorMin;
        public IValueObservable<Vector2> anchorMax;
        public IValueObservable<Vector2> offsetMin;
        public IValueObservable<Vector2> offsetMax;
        public IValueObservable<Vector2> anchoredPosition;
        public IValueObservable<Vector2> sizeDelta;
        public IValueObservable<Vector2> pivot;
        public IValueObservable<Vector2> position;
        public IValueObservable<float> rotation;
        public IValueObservable<Vector2> scale;
        public IValueObservable<bool> ignoreLayout;
        public IValueObservable<float> minWidth;
        public IValueObservable<float> minHeight;
        public IValueObservable<float> preferredWidth;
        public IValueObservable<float> preferredHeight;
        public IValueObservable<bool> flexibleWidth;
        public IValueObservable<bool> flexibleHeight;
        public IValueObservable<int> layoutPriority;
        public IValueObservable<FitMode> fitContentHorizontal;
        public IValueObservable<FitMode> fitContentVertical;
    }

    public interface IControl : IDisposable
    {
        GameObject gameObject { get; }
        RectTransform rectTransform { get; }
        Transform transform { get; }

        void AddBinding(IDisposable binding);
        void AddBinding(params IDisposable[] bindings);
        void RemoveBinding(IDisposable binding);
        void RemoveBinding(params IDisposable[] bindings);
    }
}
