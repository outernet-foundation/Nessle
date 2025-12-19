using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Nessle
{
    [RequireComponent(typeof(RectTransform))]
    public class Control<T> : MonoBehaviour, IControl
    {
        public T props { get; private set; }
        public RectTransform rectTransform { get; private set; }

        private List<IDisposable> _bindings = new List<IDisposable>();

        public void Setup(T props)
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            this.props = props;
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

        public void Dispose()
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
}
