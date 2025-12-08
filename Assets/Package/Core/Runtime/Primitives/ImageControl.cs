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
            this.sprite = sprite ?? new ValueObservable<Sprite>();
            this.color = color ?? new ValueObservable<Color>();
            this.imageType = imageType ?? new ValueObservable<ImageType>();
            this.fillCenter = fillCenter ?? new ValueObservable<bool>(true);
            this.pixelsPerUnitMultiplier = pixelsPerUnitMultiplier ?? new ValueObservable<float>(1);
            this.raycastTarget = raycastTarget ?? new ValueObservable<bool>(true);
            this.raycastPadding = raycastPadding ?? new ValueObservable<Vector4>();
            this.useSpriteMesh = useSpriteMesh ?? new ValueObservable<bool>();
            this.preserveAspect = preserveAspect ?? new ValueObservable<bool>();
            this.fillOrigin = fillOrigin ?? new ValueObservable<int>();
            this.fillMethod = fillMethod ?? new ValueObservable<ImageFillMethod>();
            this.fillAmount = fillAmount ?? new ValueObservable<float>();
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

        public override ImageProps GetInstanceProps()
        {
            return new ImageProps(
                new ValueObservable<Sprite>(_image.sprite),
                new ValueObservable<Color>(_image.color),
                new ValueObservable<ImageType>(_image.type),
                new ValueObservable<bool>(_image.fillCenter),
                new ValueObservable<float>(_image.pixelsPerUnitMultiplier),
                new ValueObservable<bool>(_image.raycastTarget),
                new ValueObservable<Vector4>(_image.raycastPadding),
                new ValueObservable<bool>(_image.useSpriteMesh),
                new ValueObservable<bool>(_image.preserveAspect),
                new ValueObservable<int>(_image.fillOrigin),
                new ValueObservable<ImageFillMethod>(_image.fillMethod),
                new ValueObservable<float>(_image.fillAmount)
            );
        }
    }
}