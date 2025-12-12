using System;
using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using SliderDirection = UnityEngine.UI.Slider.Direction;

namespace Nessle
{
    public class SliderProps : IDisposable, IValueProps<float>, IInteractableProps
    {
        public ValueObservable<float> value { get; private set; }
        public ValueObservable<float> minValue { get; private set; }
        public ValueObservable<float> maxValue { get; private set; }
        public ValueObservable<bool> wholeNumbers { get; private set; }
        public ValueObservable<SliderDirection> direction { get; private set; }
        public ValueObservable<bool> interactable { get; private set; }

        public SliderProps(
            ValueObservable<float> value = default,
            ValueObservable<float> minValue = default,
            ValueObservable<float> maxValue = default,
            ValueObservable<bool> wholeNumbers = default,
            ValueObservable<SliderDirection> direction = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = value;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.wholeNumbers = wholeNumbers;
            this.direction = direction;
            this.interactable = interactable;
        }

        public void CompleteWith(
            ValueObservable<float> value = default,
            ValueObservable<float> minValue = default,
            ValueObservable<float> maxValue = default,
            ValueObservable<bool> wholeNumbers = default,
            ValueObservable<SliderDirection> direction = default,
            ValueObservable<bool> interactable = default
        )
        {
            this.value = this.value ?? value;
            this.minValue = this.minValue ?? minValue;
            this.maxValue = this.maxValue ?? maxValue;
            this.wholeNumbers = this.wholeNumbers ?? wholeNumbers;
            this.direction = this.direction ?? direction;
            this.interactable = this.interactable ?? interactable;
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

        protected override void SetupInternal()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(x => props?.value.From(x));

            props.CompleteWith(
                Props.From(_slider.value),
                Props.From(_slider.minValue),
                Props.From(_slider.maxValue),
                Props.From(_slider.wholeNumbers),
                Props.From(_slider.direction),
                Props.From(_slider.interactable)
            );

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