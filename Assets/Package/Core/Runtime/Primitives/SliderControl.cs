using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using SliderDirection = UnityEngine.UI.Slider.Direction;

namespace Nessle
{
    public class SliderProps : IDisposable, IValueProps<float>, IInteractableProps
    {
        public ValueObservable<float> value { get; }
        public ValueObservable<float> minValue { get; }
        public ValueObservable<float> maxValue { get; }
        public ValueObservable<bool> wholeNumbers { get; }
        public ValueObservable<SliderDirection> direction { get; }
        public ValueObservable<bool> interactable { get; }

        public SliderProps(
            ValueObservable<float> value = default,
            ValueObservable<float> minValue = default,
            ValueObservable<float> maxValue = default,
            ValueObservable<bool> wholeNumbers = default,
            ValueObservable<SliderDirection> direction = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = value ?? new ValueObservable<float>();
            this.minValue = minValue ?? new ValueObservable<float>();
            this.maxValue = maxValue ?? new ValueObservable<float>();
            this.wholeNumbers = wholeNumbers ?? new ValueObservable<bool>();
            this.direction = direction ?? new ValueObservable<SliderDirection>();
            this.interactable = interactable ?? new ValueObservable<bool>();
        }

        public void Dispose()
        {
            value.Dispose();
            minValue.Dispose();
            maxValue.Dispose();
            wholeNumbers.Dispose();
            direction.Dispose();
            interactable.Dispose();
        }
    }

    [RequireComponent(typeof(Slider))]
    public class SliderControl : PrimitiveControl<SliderProps>
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(x => props?.value.From(x));
        }

        protected override void SetupInternal()
        {
            AddBinding(
                props.value.Subscribe(x => _slider.value = x.currentValue),
                props.minValue.Subscribe(x => _slider.minValue = x.currentValue),
                props.maxValue.Subscribe(x => _slider.maxValue = x.currentValue),
                props.wholeNumbers.Subscribe(x => _slider.wholeNumbers = x.currentValue),
                props.direction.Subscribe(x => _slider.direction = x.currentValue),
                props.interactable.Subscribe(x => _slider.interactable = x.currentValue)
            );
        }

        public override SliderProps GetInstanceProps()
        {
            return new SliderProps(
                new ValueObservable<float>(_slider.value),
                new ValueObservable<float>(_slider.minValue),
                new ValueObservable<float>(_slider.maxValue),
                new ValueObservable<bool>(_slider.wholeNumbers),
                new ValueObservable<SliderDirection>(_slider.direction),
                new ValueObservable<bool>(_slider.interactable)
            );
        }
    }
}