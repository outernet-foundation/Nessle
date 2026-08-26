using System;
using UnityEngine;
using ObserveThing;
using FitMode = UnityEngine.UI.ContentSizeFitter.FitMode;

namespace Nessle
{
    public class BindingCollection : IDisposable
    {
        private bool _disposed;
        private IDisposable[] _bindings;

        public BindingCollection(params IDisposable[] bindings)
            => _bindings = bindings;

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            foreach (var binding in _bindings)
                binding?.Dispose();
        }
    }

    public struct ElementProps
    {
        public IValueObservable<string> name;
        public IValueObservable<bool> active;
        public IValueObservable<bool> destroyOnDispose;
        public BindingCollection bindings;
    }

    public struct LayoutProps
    {
        public IValueObservable<Vector3> localPosition;
        public IValueObservable<Quaternion> localRotation;
        public IValueObservable<Vector3> localScale;

        public IValueObservable<Vector2> anchorMin;
        public IValueObservable<Vector2> anchorMax;
        public IValueObservable<Vector2> offsetMin;
        public IValueObservable<Vector2> offsetMax;
        public IValueObservable<Vector2> anchoredPosition;
        public IValueObservable<Vector2> sizeDelta;
        public IValueObservable<Vector2> pivot;

        public IValueObservable<bool> ignoreLayout;
        public IValueObservable<float> minWidth;
        public IValueObservable<float> minHeight;
        public IValueObservable<float> preferredWidth;
        public IValueObservable<float> preferredHeight;
        public IValueObservable<float> flexibleWidth;
        public IValueObservable<float> flexibleHeight;
        public IValueObservable<int> layoutPriority;

        public IValueObservable<FitMode> fitContentHorizontal;
        public IValueObservable<FitMode> fitContentVertical;
    }

    public interface IControl : IDisposable
    {
        GameObject gameObject { get; }
        RectTransform rectTransform { get; }
        Transform transform { get; }
        bool destroyOnDispose { get; set; }
    }
}
