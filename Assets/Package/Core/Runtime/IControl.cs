using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ObserveThing;

namespace Nessle
{
    public interface IControl : IDisposable
    {
        string identifier { get; }
        string identifierFull { get; }
        GameObject gameObject { get; }
        RectTransform rectTransform { get; }

        ValueObservable<IControl> parent { get; }
        ListObservable<IControl> children { get; }
        IValueObservable<Rect> rect { get; }

        void AddBinding(IDisposable binding);
        void AddBinding(params IDisposable[] bindings);
        void RemoveBinding(IDisposable binding);
        void RemoveBinding(params IDisposable[] bindings);

        void HandleControlHierarchyChanged();
    }

    public interface IControl<out T> : IControl
    {
        public T props { get; }
    }
}
