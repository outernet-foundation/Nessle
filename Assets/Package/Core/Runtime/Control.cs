using System;
using System.Linq;
using System.Collections.Generic;
using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public interface IControl : IDisposable
    {
        string identifier { get; }
        string identifierFull { get; }
        GameObject gameObject { get; }
        RectTransform transform { get; }

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

    public class Control : IControl
    {
        public string identifier { get; private set; }
        public string identifierFull { get; private set; }
        public GameObject gameObject { get; private set; }
        public RectTransform transform { get; private set; }

        public ValueObservable<IControl> parent { get; } = new ValueObservable<IControl>();
        public ListObservable<IControl> children { get; } = new ListObservable<IControl>();
        public IValueObservable<Rect> rect => _rect;

        private RectTransform _transformParentOverride;
        private ValueObservable<Rect> _rect = new ValueObservable<Rect>();
        private List<IDisposable> _bindings = new List<IDisposable>();

        private RectTransform _transformParent => _transformParentOverride == null ?
            transform : _transformParentOverride;

        public Control(string identifier, params Type[] components)
            : this(identifier, new GameObject(identifier, components)) { }

        public Control(string identifier, GameObject gameObject, RectTransform transformParentOverride = default)
        {
            this.identifier = identifierFull = identifier;
            this.gameObject = gameObject;

            _transformParentOverride = transformParentOverride;

            gameObject.name = identifier;

            transform = gameObject.GetOrAddComponent<RectTransform>();
            gameObject.GetOrAddComponent<RectTransformChangedHandler>().onReceivedEvent += x => _rect.From(x);

            children.Subscribe(x =>
            {
                if (x.operationType == OpType.Add)
                {
                    x.element.parent.From(this);
                    x.element.transform.SetParent(_transformParent, false);
                    x.element.transform.SetSiblingIndex(x.index);
                }
                else if (x.operationType == OpType.Remove && x.element.parent.value == this)
                {
                    x.element.parent.From(default(IControl));
                }
            });

            parent.Subscribe(x =>
            {
                if (x.previousValue != null && x.previousValue.children.Contains(this))
                    x.previousValue.children.Remove(this);

                if (x.currentValue != null && !x.currentValue.children.Contains(this))
                    x.currentValue.children.Add(this);

                HandleControlHierarchyChanged();
            });
        }

        public void AddBinding(IDisposable binding)
        {
            _bindings.Add(binding);
        }

        public void AddBinding(params IDisposable[] bindings)
        {
            _bindings.AddRange(bindings);
        }

        public void RemoveBinding(IDisposable binding)
        {
            _bindings.Remove(binding);
        }

        public void RemoveBinding(params IDisposable[] bindings)
        {
            foreach (var binding in bindings)
                _bindings.Remove(binding);
        }

        public void SetSiblingIndex(int index)
        {
            transform.SetSiblingIndex(index);
        }

        public void HandleControlHierarchyChanged()
        {
            identifierFull = parent.value == null ? identifier : $"{parent.value.identifierFull}.{identifier}";

            foreach (var child in children)
                child.HandleControlHierarchyChanged();
        }

        public virtual void Dispose()
        {
            _rect.Dispose();

            if (parent.value != null)
                parent.value.children.Remove(this);

            while (children.count > 0)
                children[children.count - 1].Dispose();

            foreach (var binding in _bindings)
                binding.Dispose();

            parent.Dispose();
            children.Dispose();

            if (Application.isPlaying)
            {
                UnityEngine.Object.Destroy(gameObject);
            }
            else
            {
                UnityEngine.Object.DestroyImmediate(gameObject);
            }
        }
    }

    public class Control<T> : Control, IControl<T>
    {
        public T props { get; }

        public Control(string identifier, T props, params Type[] components)
            : this(identifier, props, new GameObject(identifier, components)) { }

        public Control(string identifier, T props, GameObject gameObject, RectTransform transformParentOverride = default)
            : base(identifier, gameObject, transformParentOverride)
        {
            this.props = props;
        }

        public override void Dispose()
        {
            base.Dispose();

            if (props is IDisposable propsDisposable)
                propsDisposable.Dispose();
        }
    }
}
