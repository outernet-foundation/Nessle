using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ObserveThing;

namespace Nessle
{
    [RequireComponent(typeof(RectTransform))]
    public class Control : MonoBehaviour, IControl
    {
        public RectTransform rectTransform { get; private set; }

        private List<IDisposable> _bindings = new List<IDisposable>();

        public virtual void Setup()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
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

    public abstract class Control<T> : Control where T : new()
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
