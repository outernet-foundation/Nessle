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

        public void PopulateFrom(Slider slider)
        {
            value.From(slider.value);
            minValue.From(slider.minValue);
            maxValue.From(slider.maxValue);
            wholeNumbers.From(slider.wholeNumbers);
            direction.From(slider.direction);
            interactable.From(slider.interactable);
        }

        public IDisposable BindTo(Slider slider)
        {
            return new ComposedDisposable(
                value.Subscribe(x => slider.value = x.currentValue),
                minValue.Subscribe(x => slider.minValue = x.currentValue),
                maxValue.Subscribe(x => slider.maxValue = x.currentValue),
                wholeNumbers.Subscribe(x => slider.wholeNumbers = x.currentValue),
                direction.Subscribe(x => slider.direction = x.currentValue),
                interactable.Subscribe(x => slider.interactable = x.currentValue)
            );
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
            AddBinding(props.BindTo(_slider));
        }

        protected override SliderProps CompleteProps()
        {
            var props = new SliderProps();
            props.PopulateFrom(_slider);
            return props;
        }
    }
}