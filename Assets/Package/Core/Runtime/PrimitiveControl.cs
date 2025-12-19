using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ObserveThing;

namespace Nessle
{
    [RequireComponent(typeof(RectTransform))]
    public class PrimitiveControl : MonoBehaviour, IControl
    {
        public RectTransform rectTransform { get; private set; }

        public ValueObservable<IControl> parent { get; } = new ValueObservable<IControl>();
        public ListObservable<IControl> children { get; } = new ListObservable<IControl>();
        public IValueObservable<Rect> rect => _rect;

        private ValueObservable<Rect> _rect = new ValueObservable<Rect>();
        private List<IDisposable> _bindings = new List<IDisposable>();

        [SerializeField]
        protected RectTransform _childParentOverride;

        public virtual void Setup()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            gameObject.GetOrAddComponent<RectTransformChangedHandler>().onReceivedEvent += x => _rect.From(x);

            children.Subscribe(x =>
            {
                if (x.operationType == OpType.Add)
                {
                    x.element.parent.From(this);
                    x.element.rectTransform.SetParent(_childParentOverride == null ? rectTransform : _childParentOverride, false);
                    x.element.rectTransform.SetSiblingIndex(x.index);
                }
                else if (x.operationType == OpType.Remove && (object)x.element.parent.value == this)
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
            });

            SetupInternal();
        }

        protected virtual void SetupInternal() { }
        protected virtual void DisposeInternal() { }

        public void AddBinding(IDisposable binding)
        {
            if (binding != null)
                _bindings.Add(binding);
        }

        public void AddBinding(params IDisposable[] bindings)
        {
            _bindings.AddRange(bindings.Where(x => x != null));
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
            rectTransform.SetSiblingIndex(index);
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
                Destroy(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            DisposeInternal();
        }
    }

    public abstract class PrimitiveControl<T> : PrimitiveControl where T : new()
    {
        public T props { get; private set; }

        public void Setup(T props)
        {
            this.props = props;
            base.Setup();
        }

        public override void Setup()
        {
            props = new T();
            base.Setup();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (props is IDisposable propsDisposable)
                propsDisposable.Dispose();
        }
    }
}
