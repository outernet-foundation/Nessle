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

        public void PopulateMissingPropsFrom(Image image)
        {
            sprite = sprite ?? new ValueObservable<Sprite>(image.sprite);
            color = color ?? new ValueObservable<Color>(image.color);
            imageType = imageType ?? new ValueObservable<ImageType>(image.type);
            fillCenter = fillCenter ?? new ValueObservable<bool>(image.fillCenter);
            pixelsPerUnitMultiplier = pixelsPerUnitMultiplier ?? new ValueObservable<float>(image.pixelsPerUnitMultiplier);
            raycastTarget = raycastTarget ?? new ValueObservable<bool>(image.raycastTarget);
            raycastPadding = raycastPadding ?? new ValueObservable<Vector4>(image.raycastPadding);
            useSpriteMesh = useSpriteMesh ?? new ValueObservable<bool>(image.useSpriteMesh);
            preserveAspect = preserveAspect ?? new ValueObservable<bool>(image.preserveAspect);
            fillOrigin = fillOrigin ?? new ValueObservable<int>(image.fillOrigin);
            fillMethod = fillMethod ?? new ValueObservable<ImageFillMethod>(image.fillMethod);
            fillAmount = fillAmount ?? new ValueObservable<float>(image.fillAmount);
        }

        public IDisposable BindTo(Image image)
        {
            return new ComposedDisposable(
                sprite.Subscribe(x => image.sprite = x.currentValue),
                color.Subscribe(x => image.color = x.currentValue),
                imageType.Subscribe(x => image.type = x.currentValue),
                fillCenter.Subscribe(x => image.fillCenter = x.currentValue),
                pixelsPerUnitMultiplier.Subscribe(x => image.pixelsPerUnitMultiplier = x.currentValue),
                raycastTarget.Subscribe(x => image.raycastTarget = x.currentValue),
                raycastPadding.Subscribe(x => image.raycastPadding = x.currentValue),
                useSpriteMesh.Subscribe(x => image.useSpriteMesh = x.currentValue),
                preserveAspect.Subscribe(x => image.preserveAspect = x.currentValue),
                fillOrigin.Subscribe(x => image.fillOrigin = x.currentValue),
                fillMethod.Subscribe(x => image.fillMethod = x.currentValue),
                fillAmount.Subscribe(x => image.fillAmount = x.currentValue)
            );
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
            props.PopulateMissingPropsFrom(_image);
            AddBinding(props.BindTo(_image));
        }
    }
}