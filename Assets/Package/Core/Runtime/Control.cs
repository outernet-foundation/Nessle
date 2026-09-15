using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using ObserveThing;

namespace Nessle
{
    public class Control : Control<IListObservable<IControl>>
    {
        protected override void SetupInternal()
        {
            AddBinding(props.SubscribeAsChildren(transform));
        }
    }

    public class Control<T> : MonoBehaviour, IControl
    {
        public ElementProps element { get; private set; }
        public LayoutProps layout { get; private set; }
        public T props { get; private set; }
        public RectTransform rectTransform { get; private set; }

        public bool destroyOnDispose
        {
            get => _destroyOnDispose;
            set => _destroyOnDispose = value;
        }

        [SerializeField]
        private bool _destroyOnDispose = true;

        private List<IDisposable> _bindings = new List<IDisposable>();
        private bool _destroyed = false;

        public void Setup(T props = default, ElementProps element = default, LayoutProps layout = default)
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            this.props = props;
            AddBinding(element.Subscribe(this), layout.Subscribe(this));
            SetupInternal();
        }

        protected virtual void SetupInternal() { }
        protected virtual void DisposeInternal() { }

        public void AddBinding(params IDisposable[] bindings)
        {
            _bindings.AddRange(bindings);
        }

        protected virtual void OnDestroy()
        {
            _destroyed = true;
            Dispose();
        }

        public void Dispose()
        {
            if (!_destroyed && destroyOnDispose)
            {
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                else
                {
                    DestroyImmediate(gameObject);
                }

                return;
            }

            foreach (var binding in _bindings)
                binding?.Dispose();

            DisposeInternal();
        }
    }
}
