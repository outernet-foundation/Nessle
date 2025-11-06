using Nessle;
using UnityEngine;

using System;
using System.Collections.Generic;
using ObserveThing;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Test
{
    public class ToggleProps : IValueProps<bool>
    {
        public ValueObservable<bool> value { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>();
    }

    public class ToggleControl : ControlMonobehaviour<ToggleProps>
    {
        [SerializeField]
        private Toggle _toggle;

        protected override void Awake()
        {
            base.Awake();
            _toggle.onValueChanged.AddListener(HandleToggleChanged);
        }

        protected override void InitializeInternal()
        {
            AddBinding(
                props.interactable.Subscribe(x => _toggle.interactable = x.currentValue),
                props.value.Subscribe(x => _toggle.isOn = x.currentValue)
            );
        }

        private void HandleToggleChanged(bool isOn)
        {
            props?.value.From(isOn);
        }
    }

    public class ControlMonobehaviour<T> : ControlMonobehaviour, IControl<T>
    {
        public T props { get; private set; }

        public void Initialize(string identifier, T props)
        {
            this.props = props;
            base.Initialize(identifier);
        }

        public override void Initialize(string identifier)
        {
            throw new InvalidOperationException("Cannot initialize a control without an instance of it's props. Use Initialize(string identifier, T props) instead.");
        }

        public override void Dispose()
        {
            base.Dispose();

            if (props is IDisposable propsDisposable)
                propsDisposable.Dispose();
        }
    }

    [RequireComponent(typeof(RectTransform))]
    public class ControlMonobehaviour : UIBehaviour, IControl
    {
        public IControl parent { get; private set; }
        public string identifier { get; private set; }
        public string identifierFull { get; private set; }
        RectTransform IControl.transform => _rectTransform;
        public IValueObservable<Rect> rect => _rect;

        private RectTransform _rectTransform;
        private ValueObservable<Rect> _rect = new ValueObservable<Rect>();
        private HashSet<IControl> _children = new HashSet<IControl>();
        private List<IDisposable> _bindings = new List<IDisposable>();

        protected override void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            _rect.From(_rectTransform.rect);
        }

        public virtual void Initialize(string identifier)
        {
            this.identifier = identifier;
            InitializeInternal();
        }

        protected virtual void InitializeInternal() { }

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

        public void SetParent(IControl parent)
        {
            var prevParent = this.parent;
            this.parent = parent;

            if (prevParent != null)
                prevParent.RemoveChild(this);

            if (parent != null)
                parent.AddChild(this);

            HandleControlHierarchyChanged();
        }

        public void HandleControlHierarchyChanged()
        {
            identifierFull = parent == null ? identifier : $"{parent.identifierFull}.{identifier}";

            foreach (var child in _children)
                child.HandleControlHierarchyChanged();
        }

        public void AddChild(IControl child)
        {
            if (child.parent != (IControl)this)
            {
                child.SetParent(this);
                return;
            }

            _children.Add(child);
            child.transform.SetParent(transform, false);
        }

        public void RemoveChild(IControl child)
        {
            if (child.parent == (IControl)this)
            {
                child.SetParent(null);
                return;
            }

            _children.Remove(child);
            child.transform.SetParent(null, false);
        }

        public virtual void Dispose()
        {
            _rect.Dispose();

            foreach (var child in _children)
                child.Dispose();

            foreach (var binding in _bindings)
                binding.Dispose();

            Destroy(gameObject);
        }
    }
}