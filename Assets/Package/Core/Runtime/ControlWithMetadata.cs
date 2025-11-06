using System;
using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public interface IControlWithMetadata<TData> : IControl
    {
        IControl control { get; }
        TData metadata { get; }
    }

    public class ControlWithMetadata<TData> : IControlWithMetadata<TData>
    {
        public IControl control { get; }
        public TData metadata { get; }
        public IControl parent => control.parent;
        public string identifier => control.identifier;
        public string identifierFull => control.identifierFull;

        public RectTransform transform => control.transform;
        public GameObject gameObject => control.gameObject;
        public IValueObservable<Rect> rect => control.rect;

        public int childCount => control.childCount;

        public ControlWithMetadata(IControl control, TData metadata)
        {
            if (control is not IControl unityControl)
                throw new Exception($"{parent} is not a {nameof(IControl)}");

            this.control = control;
            this.metadata = metadata;
        }

        public void AddBinding(IDisposable binding)
            => control.AddBinding(binding);

        public void AddBinding(params IDisposable[] bindings)
            => control.AddBinding(bindings);

        public void RemoveBinding(IDisposable binding)
            => control.RemoveBinding(binding);

        public void RemoveBinding(params IDisposable[] bindings)
            => control.RemoveBinding(bindings);

        public void SetParent(IControl parent)
            => control.SetParent(parent);

        public void SetSiblingIndex(int index)
            => control.SetSiblingIndex(index);

        void IControl.AddChild(IControl child)
            => control.AddChild(child);

        void IControl.RemoveChild(IControl child)
            => control.RemoveChild(child);

        public void Dispose()
            => control.Dispose();

        public void HandleControlHierarchyChanged()
            => control.HandleControlHierarchyChanged();

        public IControl GetChild(int index)
            => control.GetChild(index);
    }

    public interface IControlWithMetadata<TProps, TData> : IControlWithMetadata<TData>
    {
        new IControl<TProps> control { get; }
        IControl IControlWithMetadata<TData>.control => control;
    }

    public class ControlWithMetadata<TProps, TData> : ControlWithMetadata<TData>, IControlWithMetadata<TProps, TData>
    {
        new public IControl<TProps> control { get; }
        public TProps props => control.props;

        public ControlWithMetadata(IControl<TProps> control, TData metadata) : base(control, metadata)
        {
            this.control = control;
        }
    }
}
