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

        private ValueObservable<Rect> _rect = new ValueObservable<Rect>();
        private List<IControl> _children = new List<IControl>();
        private List<IDisposable> _bindings = new List<IDisposable>();

        public Control(string identifier, params Type[] components)
            : this(identifier, new GameObject(identifier, components)) { }

        public Control(string identifier, GameObject gameObject)
        {
            this.identifier = identifierFull = identifier;
            this.gameObject = gameObject;

            gameObject.name = identifier;

            transform = gameObject.GetOrAddComponent<RectTransform>();
            gameObject.GetOrAddComponent<RectTransformChangedHandler>().onReceivedEvent += x => _rect.From(x);

            children.Subscribe(x =>
            {
                if (x.operationType == OpType.Add)
                {
                    x.element.parent.From(this);
                }
                else if (x.operationType == OpType.Remove && x.element.parent.value == this)
                {
                    x.element.parent.From(default(IControl));
                }
            });

            parent.Subscribe(x =>
            {
                if (x.previousValue.children.Contains(this))
                    x.previousValue.children.Remove(this);

                if (!x.currentValue.children.Contains(this))
                    x.currentValue.children.Add(this);

                HandleControlHierarchyChanged();
            });
        }

        public IControl GetChild(int index)
            => _children[index];

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
            identifierFull = parent == null ? identifier : $"{parent.value.identifierFull}.{identifier}";

            foreach (var child in _children)
                child.HandleControlHierarchyChanged();
        }

        public void AddChild(IControl child)
        {
            if (child.parent.value != this)
            {
                child.parent.From(this);
                return;
            }

            _children.Add(child);
            child.transform.SetParent(transform, false);
        }

        public virtual void Dispose()
        {
            _rect.Dispose();

            foreach (var child in _children)
                child.Dispose();

            foreach (var binding in _bindings)
                binding.Dispose();

            parent.Dispose();
            children.Dispose();

            UnityEngine.Object.Destroy(gameObject);
        }
    }

    public class Control<T> : Control, IControl<T>
    {
        public T props { get; }

        public Control(string identifier, T props, params Type[] components)
            : this(identifier, props, new GameObject(identifier, components)) { }

        public Control(string identifier, T props, GameObject gameObject)
            : base(identifier, gameObject)
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
