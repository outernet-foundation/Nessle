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
        public ValueObservable<Sprite> sprite { get; set; }
        public ValueObservable<Color> color { get; set; }
        public ValueObservable<ImageType> imageType { get; set; }
        public ValueObservable<bool> fillCenter { get; set; }
        public ValueObservable<float> pixelsPerUnitMultiplier { get; set; }
        public ValueObservable<bool> raycastTarget { get; set; }
        public ValueObservable<Vector4> raycastPadding { get; set; }
        public ValueObservable<bool> useSpriteMesh { get; set; }
        public ValueObservable<bool> preserveAspect { get; set; }
        public ValueObservable<int> fillOrigin { get; set; }
        public ValueObservable<ImageFillMethod> fillMethod { get; set; }
        public ValueObservable<float> fillAmount { get; set; }

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
            props.sprite = props.sprite ?? new ValueObservable<Sprite>(_image.sprite);
            props.color = props.color ?? new ValueObservable<Color>(_image.color);
            props.imageType = props.imageType ?? new ValueObservable<ImageType>(_image.type);
            props.fillCenter = props.fillCenter ?? new ValueObservable<bool>(_image.fillCenter);
            props.pixelsPerUnitMultiplier = props.pixelsPerUnitMultiplier ?? new ValueObservable<float>(_image.pixelsPerUnitMultiplier);
            props.raycastTarget = props.raycastTarget ?? new ValueObservable<bool>(_image.raycastTarget);
            props.raycastPadding = props.raycastPadding ?? new ValueObservable<Vector4>(_image.raycastPadding);
            props.useSpriteMesh = props.useSpriteMesh ?? new ValueObservable<bool>(_image.useSpriteMesh);
            props.preserveAspect = props.preserveAspect ?? new ValueObservable<bool>(_image.preserveAspect);
            props.fillOrigin = props.fillOrigin ?? new ValueObservable<int>(_image.fillOrigin);
            props.fillMethod = props.fillMethod ?? new ValueObservable<ImageFillMethod>(_image.fillMethod);
            props.fillAmount = props.fillAmount ?? new ValueObservable<float>(_image.fillAmount);

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