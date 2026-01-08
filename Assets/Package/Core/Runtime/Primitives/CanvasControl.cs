using ObserveThing;
using UnityEngine;
using UnityEngine.UI;

namespace Nessle
{
    public struct CanvasProps
    {
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
                props.renderMode?.Subscribe(x => _canvas.renderMode = x.currentValue),
                props.scaleFactor?.Subscribe(x => _canvas.scaleFactor = x.currentValue),
                props.referencePixelsPerUnit?.Subscribe(x => _canvas.referencePixelsPerUnit = x.currentValue),
                props.overridePixelPerfect?.Subscribe(x => _canvas.overridePixelPerfect = x.currentValue),
                props.vertexColorAlwaysGammaSpace?.Subscribe(x => _canvas.vertexColorAlwaysGammaSpace = x.currentValue),
                props.pixelPerfect?.Subscribe(x => _canvas.pixelPerfect = x.currentValue),
                props.planeDistance?.Subscribe(x => _canvas.planeDistance = x.currentValue),
                props.overrideSorting?.Subscribe(x => _canvas.overrideSorting = x.currentValue),
                props.sortingOrder?.Subscribe(x => _canvas.sortingOrder = x.currentValue),
                props.targetDisplay?.Subscribe(x => _canvas.targetDisplay = x.currentValue),
                props.sortingLayerID?.Subscribe(x => _canvas.sortingLayerID = x.currentValue),
                props.additionalShaderChannels?.Subscribe(x => _canvas.additionalShaderChannels = x.currentValue),
                props.sortingLayerName?.Subscribe(x => _canvas.sortingLayerName = x.currentValue),
                props.updateRectTransformForStandalone?.Subscribe(x => _canvas.updateRectTransformForStandalone = x.currentValue),
                props.worldCamera?.Subscribe(x => _canvas.worldCamera = x.currentValue),
                props.normalizedSortingGridSize?.Subscribe(x => _canvas.normalizedSortingGridSize = x.currentValue),
                props.canvasScaler.uiScaleMode?.Subscribe(x => _canvasScaler.uiScaleMode = x.currentValue),
                props.canvasScaler.referencePixelsPerUnit?.Subscribe(x => _canvasScaler.referencePixelsPerUnit = x.currentValue),
                props.canvasScaler.scaleFactor?.Subscribe(x => _canvasScaler.scaleFactor = x.currentValue),
                props.canvasScaler.referenceResolution?.Subscribe(x => _canvasScaler.referenceResolution = x.currentValue),
                props.canvasScaler.screenMatchMode?.Subscribe(x => _canvasScaler.screenMatchMode = x.currentValue),
                props.canvasScaler.matchWidthOrHeight?.Subscribe(x => _canvasScaler.matchWidthOrHeight = x.currentValue),
                props.canvasScaler.physicalUnit?.Subscribe(x => _canvasScaler.physicalUnit = x.currentValue),
                props.canvasScaler.fallbackScreenDPI?.Subscribe(x => _canvasScaler.fallbackScreenDPI = x.currentValue),
                props.canvasScaler.defaultSpriteDPI?.Subscribe(x => _canvasScaler.defaultSpriteDPI = x.currentValue),
                props.canvasScaler.dynamicPixelsPerUnit?.Subscribe(x => _canvasScaler.dynamicPixelsPerUnit = x.currentValue)
            );
        }
    }
}