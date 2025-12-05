using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using SliderDirection = UnityEngine.UI.Slider.Direction;

namespace Nessle
{
    public class SliderProps : IDisposable, IValueProps<float>, IInteractableProps
    {
        public ValueObservable<float> value { get; } = new ValueObservable<float>();
        public ValueObservable<float> minValue { get; } = new ValueObservable<float>();
        public ValueObservable<float> maxValue { get; } = new ValueObservable<float>();
        public ValueObservable<bool> wholeNumbers { get; } = new ValueObservable<bool>();
        public ValueObservable<SliderDirection> direction { get; } = new ValueObservable<SliderDirection>();
        public ValueObservable<bool> interactable { get; } = new ValueObservable<bool>();

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
    }
}