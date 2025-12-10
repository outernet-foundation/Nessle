using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using SliderDirection = UnityEngine.UI.Slider.Direction;

namespace Nessle
{
    public class SliderProps : IDisposable, IValueProps<float>, IInteractableProps
    {
        public ValueObservable<float> value { get; set; }
        public ValueObservable<float> minValue { get; set; }
        public ValueObservable<float> maxValue { get; set; }
        public ValueObservable<bool> wholeNumbers { get; set; }
        public ValueObservable<SliderDirection> direction { get; set; }
        public ValueObservable<bool> interactable { get; set; }

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
            props.value = props.value ?? new ValueObservable<float>(_slider.value);
            props.minValue = props.minValue ?? new ValueObservable<float>(_slider.minValue);
            props.maxValue = props.maxValue ?? new ValueObservable<float>(_slider.maxValue);
            props.wholeNumbers = props.wholeNumbers ?? new ValueObservable<bool>(_slider.wholeNumbers);
            props.direction = props.direction ?? new ValueObservable<SliderDirection>(_slider.direction);
            props.interactable = props.interactable ?? new ValueObservable<bool>(_slider.interactable);

            AddBinding(
                props.value.Subscribe(x => _slider.value = x.currentValue),
                props.minValue.Subscribe(x => _slider.minValue = x.currentValue),
                props.maxValue.Subscribe(x => _slider.maxValue = x.currentValue),
                props.wholeNumbers.Subscribe(x => _slider.wholeNumbers = x.currentValue),
                props.direction.Subscribe(x => _slider.direction = x.currentValue),
                props.interactable.Subscribe(x => _slider.interactable = x.currentValue)
            );
        }
    }
}