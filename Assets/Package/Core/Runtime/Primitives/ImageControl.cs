using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ImageType = UnityEngine.UI.Image.Type;
using ImageFillMethod = UnityEngine.UI.Image.FillMethod;

namespace Nessle
{
    public class ImageProps : IDisposable
    {
        public ValueObservable<Sprite> sprite { get; }
        public ValueObservable<Color> color { get; }
        public ValueObservable<ImageType> imageType { get; }
        public ValueObservable<bool> fillCenter { get; }
        public ValueObservable<float> pixelsPerUnitMultiplier { get; }
        public ValueObservable<bool> raycastTarget { get; }
        public ValueObservable<Vector4> raycastPadding { get; }
        public ValueObservable<bool> useSpriteMesh { get; }
        public ValueObservable<bool> preserveAspect { get; }
        public ValueObservable<int> fillOrigin { get; }
        public ValueObservable<ImageFillMethod> fillMethod { get; }
        public ValueObservable<float> fillAmount { get; }

        public ImageProps() { }

        public ImageProps(
            ValueObservable<Sprite> sprite = default,
            ValueObservable<Color> color = default,
            ValueObservable<ImageType> imageType = default,
            ValueObservable<bool> fillCenter = default,
            ValueObservable<float> pixelsPerUnitMultiplier = default,
            ValueObservable<bool> raycastTarget = default,
            ValueObservable<Vector4> raycastPadding = default,
            ValueObservable<bool> useSpriteMesh = default,
            ValueObservable<bool> preserveAspect = default,
            ValueObservable<int> fillOrigin = default,
            ValueObservable<ImageFillMethod> fillMethod = default,
            ValueObservable<float> fillAmount = default
        )
        {
            this.sprite = sprite;
            this.color = color;
            this.imageType = imageType;
            this.fillCenter = fillCenter;
            this.pixelsPerUnitMultiplier = pixelsPerUnitMultiplier;
            this.raycastTarget = raycastTarget;
            this.raycastPadding = raycastPadding;
            this.useSpriteMesh = useSpriteMesh;
            this.preserveAspect = preserveAspect;
            this.fillOrigin = fillOrigin;
            this.fillMethod = fillMethod;
            this.fillAmount = fillAmount;
        }

        public void Dispose()
        {
            sprite?.Dispose();
            color?.Dispose();
            imageType?.Dispose();
            fillCenter?.Dispose();
            pixelsPerUnitMultiplier?.Dispose();
            raycastTarget?.Dispose();
            raycastPadding?.Dispose();
            useSpriteMesh?.Dispose();
            preserveAspect?.Dispose();
            fillOrigin?.Dispose();
            fillMethod?.Dispose();
            fillAmount?.Dispose();
        }
    }

    [RequireComponent(typeof(Image))]
    public class ImageControl : PrimitiveControl<ImageProps>
    {
        private Image _image;

        protected override void SetupInternal()
        {
            _image = GetComponent<Image>();

            AddBinding(
                props.sprite?.Subscribe(x => _image.sprite = x.currentValue),
                props.color?.Subscribe(x => _image.color = x.currentValue),
                props.imageType?.Subscribe(x => _image.type = x.currentValue),
                props.fillCenter?.Subscribe(x => _image.fillCenter = x.currentValue),
                props.pixelsPerUnitMultiplier?.Subscribe(x => _image.pixelsPerUnitMultiplier = x.currentValue),
                props.raycastTarget?.Subscribe(x => _image.raycastTarget = x.currentValue),
                props.raycastPadding?.Subscribe(x => _image.raycastPadding = x.currentValue),
                props.useSpriteMesh?.Subscribe(x => _image.useSpriteMesh = x.currentValue),
                props.preserveAspect?.Subscribe(x => _image.preserveAspect = x.currentValue),
                props.fillOrigin?.Subscribe(x => _image.fillOrigin = x.currentValue),
                props.fillMethod?.Subscribe(x => _image.fillMethod = x.currentValue),
                props.fillAmount?.Subscribe(x => _image.fillAmount = x.currentValue)
            );
        }
    }
}