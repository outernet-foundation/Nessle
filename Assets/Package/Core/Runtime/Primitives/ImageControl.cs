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
        public ValueObservable<Sprite> sprite { get; } = new ValueObservable<Sprite>();
        public ValueObservable<Color> color { get; } = new ValueObservable<Color>();
        public ValueObservable<ImageType> imageType { get; } = new ValueObservable<ImageType>();
        public ValueObservable<bool> fillCenter { get; } = new ValueObservable<bool>(true);
        public ValueObservable<float> pixelsPerUnitMultiplier { get; } = new ValueObservable<float>();
        public ValueObservable<bool> raycastTarget { get; } = new ValueObservable<bool>(true);
        public ValueObservable<Vector4> raycastPadding { get; } = new ValueObservable<Vector4>();
        public ValueObservable<bool> useSpriteMesh { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> preserveAspect { get; } = new ValueObservable<bool>();
        public ValueObservable<int> fillOrigin { get; } = new ValueObservable<int>();
        public ValueObservable<ImageFillMethod> fillMethod { get; } = new ValueObservable<ImageFillMethod>();
        public ValueObservable<float> fillAmount { get; } = new ValueObservable<float>();

        public void PopulateFrom(Image image)
        {
            sprite.From(image.sprite);
            color.From(image.color);
            imageType.From(image.type);
            fillCenter.From(image.fillCenter);
            pixelsPerUnitMultiplier.From(image.pixelsPerUnitMultiplier);
            raycastTarget.From(image.raycastTarget);
            raycastPadding.From(image.raycastPadding);
            useSpriteMesh.From(image.useSpriteMesh);
            preserveAspect.From(image.preserveAspect);
            fillOrigin.From(image.fillOrigin);
            fillMethod.From(image.fillMethod);
            fillAmount.From(image.fillAmount);
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
            AddBinding(props.BindTo(_image));
        }

        protected override ImageProps GetDefaultProps()
        {
            var props = new ImageProps();
            props.PopulateFrom(_image);
            return props;
        }
    }
}