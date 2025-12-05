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
        public ValueObservable<float> pixelsPerUnitMultiplier { get; } = new ValueObservable<float>(1);
        public ValueObservable<bool> raycastTarget { get; } = new ValueObservable<bool>(true);
        public ValueObservable<Vector4> raycastPadding { get; } = new ValueObservable<Vector4>();
        public ValueObservable<bool> useSpriteMesh { get; } = new ValueObservable<bool>();
        public ValueObservable<bool> preserveAspect { get; } = new ValueObservable<bool>();
        public ValueObservable<int> fillOrigin { get; } = new ValueObservable<int>();
        public ValueObservable<ImageFillMethod> fillMethod { get; } = new ValueObservable<ImageFillMethod>();
        public ValueObservable<float> fillAmount { get; } = new ValueObservable<float>();

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
            AddBinding(Utility.BindImage(props, _image, true));
        }
    }
}