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
        public TransformProps transform;
        public IValueObservable<Sprite> sprite;
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
                props.transform.Subscribe(this),
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