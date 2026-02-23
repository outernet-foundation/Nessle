using ObserveThing;
using UnityEngine;
using UnityEngine.UI;

namespace Nessle
{
    public struct CanvasProps
    {
        public ElementProps element;
        public LayoutProps layout;
        public IListObservable<IControl> children;

        public IValueObservable<RenderMode> renderMode;
        public IValueObservable<float> scaleFactor;
        public IValueObservable<float> referencePixelsPerUnit;
        public IValueObservable<bool> overridePixelPerfect;
        public IValueObservable<bool> vertexColorAlwaysGammaSpace;
        public IValueObservable<bool> pixelPerfect;
        public IValueObservable<float> planeDistance;
        public IValueObservable<bool> overrideSorting;
        public IValueObservable<int> sortingOrder;
        public IValueObservable<int> targetDisplay;
        public IValueObservable<int> sortingLayerID;
        public IValueObservable<AdditionalCanvasShaderChannels> additionalShaderChannels;
        public IValueObservable<string> sortingLayerName;
        public IValueObservable<StandaloneRenderResize> updateRectTransformForStandalone;
        public IValueObservable<Camera> worldCamera;
        public IValueObservable<float> normalizedSortingGridSize;

        public CanvasScalerProps canvasScaler;
    }

    public struct CanvasScalerProps
    {
        public IValueObservable<CanvasScaler.ScaleMode> uiScaleMode;
        public IValueObservable<float> referencePixelsPerUnit;
        public IValueObservable<float> scaleFactor;
        public IValueObservable<Vector2> referenceResolution;
        public IValueObservable<CanvasScaler.ScreenMatchMode> screenMatchMode;
        public IValueObservable<float> matchWidthOrHeight;
        public IValueObservable<CanvasScaler.Unit> physicalUnit;
        public IValueObservable<float> fallbackScreenDPI;
        public IValueObservable<float> defaultSpriteDPI;
        public IValueObservable<float> dynamicPixelsPerUnit;
    }

    [RequireComponent(typeof(Canvas))]
    public class CanvasControl : Control<CanvasProps>
    {
        private Canvas _canvas;
        private CanvasScaler _canvasScaler;

        protected override void SetupInternal()
        {
            _canvas = GetComponent<Canvas>();

            if (
                props.canvasScaler.uiScaleMode != null ||
                props.canvasScaler.referencePixelsPerUnit != null ||
                props.canvasScaler.scaleFactor != null ||
                props.canvasScaler.referenceResolution != null ||
                props.canvasScaler.screenMatchMode != null ||
                props.canvasScaler.matchWidthOrHeight != null ||
                props.canvasScaler.physicalUnit != null ||
                props.canvasScaler.fallbackScreenDPI != null ||
                props.canvasScaler.defaultSpriteDPI != null ||
                props.canvasScaler.dynamicPixelsPerUnit != null
            )
            {
                _canvasScaler = gameObject.GetOrAddComponent<CanvasScaler>();
            }

            AddBinding(
                props.element.Subscribe(this),
                props.layout.Subscribe(this),
                props.children?.SubscribeAsChildren(rectTransform),
                props.renderMode?.Subscribe(x => _canvas.renderMode = x),
                props.scaleFactor?.Subscribe(x => _canvas.scaleFactor = x),
                props.referencePixelsPerUnit?.Subscribe(x => _canvas.referencePixelsPerUnit = x),
                props.overridePixelPerfect?.Subscribe(x => _canvas.overridePixelPerfect = x),
                props.vertexColorAlwaysGammaSpace?.Subscribe(x => _canvas.vertexColorAlwaysGammaSpace = x),
                props.pixelPerfect?.Subscribe(x => _canvas.pixelPerfect = x),
                props.planeDistance?.Subscribe(x => _canvas.planeDistance = x),
                props.overrideSorting?.Subscribe(x => _canvas.overrideSorting = x),
                props.sortingOrder?.Subscribe(x => _canvas.sortingOrder = x),
                props.targetDisplay?.Subscribe(x => _canvas.targetDisplay = x),
                props.sortingLayerID?.Subscribe(x => _canvas.sortingLayerID = x),
                props.additionalShaderChannels?.Subscribe(x => _canvas.additionalShaderChannels = x),
                props.sortingLayerName?.Subscribe(x => _canvas.sortingLayerName = x),
                props.updateRectTransformForStandalone?.Subscribe(x => _canvas.updateRectTransformForStandalone = x),
                props.worldCamera?.Subscribe(x => _canvas.worldCamera = x),
                props.normalizedSortingGridSize?.Subscribe(x => _canvas.normalizedSortingGridSize = x),
                props.canvasScaler.uiScaleMode?.Subscribe(x => _canvasScaler.uiScaleMode = x),
                props.canvasScaler.referencePixelsPerUnit?.Subscribe(x => _canvasScaler.referencePixelsPerUnit = x),
                props.canvasScaler.scaleFactor?.Subscribe(x => _canvasScaler.scaleFactor = x),
                props.canvasScaler.referenceResolution?.Subscribe(x => _canvasScaler.referenceResolution = x),
                props.canvasScaler.screenMatchMode?.Subscribe(x => _canvasScaler.screenMatchMode = x),
                props.canvasScaler.matchWidthOrHeight?.Subscribe(x => _canvasScaler.matchWidthOrHeight = x),
                props.canvasScaler.physicalUnit?.Subscribe(x => _canvasScaler.physicalUnit = x),
                props.canvasScaler.fallbackScreenDPI?.Subscribe(x => _canvasScaler.fallbackScreenDPI = x),
                props.canvasScaler.defaultSpriteDPI?.Subscribe(x => _canvasScaler.defaultSpriteDPI = x),
                props.canvasScaler.dynamicPixelsPerUnit?.Subscribe(x => _canvasScaler.dynamicPixelsPerUnit = x)
            );
        }
    }
}