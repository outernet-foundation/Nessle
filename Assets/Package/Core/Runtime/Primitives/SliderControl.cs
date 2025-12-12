using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using SliderDirection = UnityEngine.UI.Slider.Direction;

namespace Nessle
{
    public class SliderProps : IDisposable
    {
        public ValueObservable<float> value { get; }
        public ValueObservable<float> minValue { get; }
        public ValueObservable<float> maxValue { get; }
        public ValueObservable<bool> wholeNumbers { get; }
        public ValueObservable<SliderDirection> direction { get; }
        public ValueObservable<bool> interactable { get; }
        public ValueObservable<Action<float>> onValueChanged { get; }

        public SliderProps() { }

        public SliderProps(
            ValueObservable<float> value = default,
            ValueObservable<float> minValue = default,
            ValueObservable<float> maxValue = default,
            ValueObservable<bool> wholeNumbers = default,
            ValueObservable<SliderDirection> direction = default,
            ValueObservable<bool> interactable = default,
            ValueObservable<Action<float>> onValueChanged = default
        )
        {
            this.value = value;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.wholeNumbers = wholeNumbers;
            this.direction = direction;
            this.interactable = interactable;
            this.onValueChanged = onValueChanged;
        }

        public void Dispose()
        {
            value?.Dispose();
            minValue?.Dispose();
            maxValue?.Dispose();
            wholeNumbers?.Dispose();
            direction?.Dispose();
            interactable?.Dispose();
            onValueChanged?.Dispose();
        }
    }

    [RequireComponent(typeof(Slider))]
    public class SliderControl : PrimitiveControl<SliderProps>
    {
        private Slider _slider;

        protected override void SetupInternal()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(x => props?.onValueChanged?.value?.Invoke(x));

            AddBinding(
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