using System;
using ObserveThing;
using UnityEngine;
using UnityEngine.UI;

namespace Nessle
{
    public struct FullscreenUIProps
    {
        public ElementProps element;
        public ImageProps background;
        public IValueObservable<bool> automaticallyUpdateSafeArea;
        public Func<IDisposable, IValueObservable<IControl>> contentConstructor;
    }

    public class FullscreenUIControl : Control<FullscreenUIProps>
    {
        public Canvas fullscreenUI;
        public Image background;
        public RectTransform safeArea;
        public bool automaticallyUpdateSafeArea = true;

        private bool _setup;
        private bool _disposedDuringSetup;

        private void LateUpdate()
        {
            if (automaticallyUpdateSafeArea)
            {
                safeArea.anchorMin = Vector2.one * 0.5f;
                safeArea.anchorMax = Vector2.one * 0.5f;
                safeArea.offsetMin = Vector2.zero;
                safeArea.offsetMax = Vector2.zero;

                var scale = new Vector2(1f / transform.lossyScale.x, 1f / transform.lossyScale.y);
                var delta = Screen.safeArea.max - Screen.safeArea.min;

                safeArea.sizeDelta = Vector2.Scale(scale, delta);
            }
        }

        private void DisposeFromClientCode()
        {
            if (_setup)
            {
                Dispose();
            }
            else
            {
                _disposedDuringSetup = true;
            }
        }

        protected override void SetupInternal()
        {
            fullscreenUI.transform.SetParent(null);
            fullscreenUI.transform.SetAsLastSibling();

            var backgroundControl = background.gameObject.GetOrAddComponent<Control<ImageProps>, ImageControl>();
            backgroundControl.Setup(props.background);

            var content = props.contentConstructor?.Invoke(new Disposable(DisposeFromClientCode));

            AddBinding(
                backgroundControl,
                props.element.Subscribe(this),
                props.automaticallyUpdateSafeArea?.Subscribe(x => automaticallyUpdateSafeArea = x.currentValue),
                content?.Subscribe(x =>
                {
                    if (x.previousValue != null && x.previousValue.rectTransform.parent == this)
                        x.previousValue.rectTransform.SetParent(null);

                    if (x.currentValue != null)
                        x.currentValue.rectTransform.SetParent(safeArea, false);
                })
            );

            if (_disposedDuringSetup)
                Dispose();
        }

        private void OnDestroy()
        {
            Destroy(fullscreenUI.gameObject);
        }
    }
}