using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ImageType = UnityEngine.UI.Image.Type;
using ImageFillMethod = UnityEngine.UI.Image.FillMethod;

namespace Nessle
{
    public class ImageProps : IDisposable, IColorProps
    {
        public ValueObservable<Sprite> sprite { get; private set; }
        public ValueObservable<Color> color { get; private set; }
        public ValueObservable<ImageType> imageType { get; private set; }
        public ValueObservable<bool> fillCenter { get; private set; }
        public ValueObservable<float> pixelsPerUnitMultiplier { get; private set; }
        public ValueObservable<bool> raycastTarget { get; private set; }
        public ValueObservable<Vector4> raycastPadding { get; private set; }
        public ValueObservable<bool> useSpriteMesh { get; private set; }
        public ValueObservable<bool> preserveAspect { get; private set; }
        public ValueObservable<int> fillOrigin { get; private set; }
        public ValueObservable<ImageFillMethod> fillMethod { get; private set; }
        public ValueObservable<float> fillAmount { get; private set; }

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

        public void CompleteWith(
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
            this.sprite = this.sprite ?? sprite;
            this.color = this.color ?? color;
            this.imageType = this.imageType ?? imageType;
            this.fillCenter = this.fillCenter ?? fillCenter;
            this.pixelsPerUnitMultiplier = this.pixelsPerUnitMultiplier ?? pixelsPerUnitMultiplier;
            this.raycastTarget = this.raycastTarget ?? raycastTarget;
            this.raycastPadding = this.raycastPadding ?? raycastPadding;
            this.useSpriteMesh = this.useSpriteMesh ?? useSpriteMesh;
            this.preserveAspect = this.preserveAspect ?? preserveAspect;
            this.fillOrigin = this.fillOrigin ?? fillOrigin;
            this.fillMethod = this.fillMethod ?? fillMethod;
            this.fillAmount = this.fillAmount ?? fillAmount;
        }

        public void Dispose()
        {
            sprite.Dispose();
            color.Dispose();
            imageType.Dispose();
            fillCenter.Dispose();
            pixelsPerUnitMultiplier.Dispose();
            raycastTarget.Dispose();
            raycastPadding.Dispose();
            useSpriteMesh.Dispose();
            preserveAspect.Dispose();
            fillOrigin.Dispose();
            fillMethod.Dispose();
            fillAmount.Dispose();
        }
    }

    [RequireComponent(typeof(Image))]
    public class ImageControl : PrimitiveControl<ImageProps>
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        protected override void SetupInternal()
        {
            AddBinding(
                props.sprite.Subscribe(x => _image.sprite = x.currentValue),
                props.color.Subscribe(x => _image.color = x.currentValue),
                props.imageType.Subscribe(x => _image.type = x.currentValue),
                props.fillCenter.Subscribe(x => _image.fillCenter = x.currentValue),
                props.pixelsPerUnitMultiplier.Subscribe(x => _image.pixelsPerUnitMultiplier = x.currentValue),
                props.raycastTarget.Subscribe(x => _image.raycastTarget = x.currentValue),
                props.raycastPadding.Subscribe(x => _image.raycastPadding = x.currentValue),
                props.useSpriteMesh.Subscribe(x => _image.useSpriteMesh = x.currentValue),
                props.preserveAspect.Subscribe(x => _image.preserveAspect = x.currentValue),
                props.fillOrigin.Subscribe(x => _image.fillOrigin = x.currentValue),
                props.fillMethod.Subscribe(x => _image.fillMethod = x.currentValue),
                props.fillAmount.Subscribe(x => _image.fillAmount = x.currentValue)
            );
        }
    }
}