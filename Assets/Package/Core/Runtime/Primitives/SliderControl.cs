using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using SliderDirection = UnityEngine.UI.Slider.Direction;
using UnityEngine.Events;

namespace Nessle
{
    public struct SliderProps
    {
        public ElementProps element;
        public LayoutProps layout;
        public IValueObservable<float> value;
        public IValueObservable<float> minValue;
        public IValueObservable<float> maxValue;
        public IValueObservable<bool> wholeNumbers;
        public IValueObservable<SliderDirection> direction;
        public IValueObservable<bool> interactable;
        public UnityAction<float> onValueChanged;
    }

    [RequireComponent(typeof(Slider))]
    public class SliderControl : Control<SliderProps>
    {
        private Slider _slider;

        protected override void SetupInternal()
        {
            _slider = GetComponent<Slider>();

            if (props.onValueChanged != null)
                _slider.onValueChanged.AddListener(props.onValueChanged);

            AddBinding(
                props.element.Subscribe(this),
                props.layout.Subscribe(this),
                props.value?.Subscribe(x => _slider.value = x.currentValue),
                props.minValue?.Subscribe(x => _slider.minValue = x.currentValue),
                props.maxValue?.Subscribe(x => _slider.maxValue = x.currentValue),
                props.wholeNumbers?.Subscribe(x => _slider.wholeNumbers = x.currentValue),
                props.direction?.Subscribe(x => _slider.direction = x.currentValue),
                props.interactable?.Subscribe(x => _slider.interactable = x.currentValue)
            );
        }
    }
}