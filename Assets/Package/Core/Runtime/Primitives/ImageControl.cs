using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using ImageType = UnityEngine.UI.Image.Type;
using ImageFillMethod = UnityEngine.UI.Image.FillMethod;

namespace Nessle
{
    public struct ImageProps
    {
        public ElementProps element;
        public LayoutProps layout;
        public IValueObservable<Sprite> sprite;
        public ImageStyleProps style;
    }

    public struct ImageStyleProps
    {
        public IValueObservable<Color> color;
        public IValueObservable<ImageType> imageType;
        public IValueObservable<bool> fillCenter;
        public IValueObservable<float> pixelsPerUnitMultiplier;
        public IValueObservable<bool> raycastTarget;
        public IValueObservable<Vector4> raycastPadding;
        public IValueObservable<bool> useSpriteMesh;
        public IValueObservable<bool> preserveAspect;
        public IValueObservable<int> fillOrigin;
        public IValueObservable<ImageFillMethod> fillMethod;
        public IValueObservable<float> fillAmount;
    }

    [RequireComponent(typeof(Image))]
    public class ImageControl : Control<ImageProps>
    {
        private Image _image;

        protected override void SetupInternal()
        {
            _image = GetComponent<Image>();

            AddBinding(
                props.element.Subscribe(this),
                props.layout.Subscribe(this),
                props.sprite?.Subscribe(x => _image.sprite = x.currentValue),
                props.style.color?.Subscribe(x => _image.color = x.currentValue),
                props.style.imageType?.Subscribe(x => _image.type = x.currentValue),
                props.style.fillCenter?.Subscribe(x => _image.fillCenter = x.currentValue),
                props.style.pixelsPerUnitMultiplier?.Subscribe(x => _image.pixelsPerUnitMultiplier = x.currentValue),
                props.style.raycastTarget?.Subscribe(x => _image.raycastTarget = x.currentValue),
                props.style.raycastPadding?.Subscribe(x => _image.raycastPadding = x.currentValue),
                props.style.useSpriteMesh?.Subscribe(x => _image.useSpriteMesh = x.currentValue),
                props.style.preserveAspect?.Subscribe(x => _image.preserveAspect = x.currentValue),
                props.style.fillOrigin?.Subscribe(x => _image.fillOrigin = x.currentValue),
                props.style.fillMethod?.Subscribe(x => _image.fillMethod = x.currentValue),
                props.style.fillAmount?.Subscribe(x => _image.fillAmount = x.currentValue)
            );
        }
    }
}