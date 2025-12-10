using System;
using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public interface IControlConstructor
    {
        public IControl control { get; }

        IControl Construct();
    }

    public interface IControlConstructor<T> : IControlConstructor
    {
        public T props { get; }
    }

    public class ControlConstructor : IControlConstructor
    {
        public IControl control { get; }

        public ControlConstructor(IControl control)
        {
            this.control = control;
        }

        public virtual IControl Construct()
        {
            control.Setup();
            return control;
        }
    }

    public class ControlConstructor<T> : ControlConstructor, IControlConstructor<T>
    {
        public T props { get; }

        public ControlConstructor(IControl<T> control, T props) : base(control)
        {
            this.props = props;
        }

        public override IControl Construct()
        {
            control.Setup(props);
            return control;
        }
    }

    public static class UIBuilder
    {
        public static UIPrimitiveSet primitives { get; set; }

        public static IControlConstructor Control(string identifier, params Type[] componentTypes)
            => Control(identifier, new GameObject(identifier, componentTypes), default(RectTransform));

        public static IControlConstructor Control(string identifier, RectTransform childParentOverride, params Type[] componentTypes)
            => Control(identifier, new GameObject(identifier, componentTypes), childParentOverride);

        public static IControlConstructor Control(string identifier, GameObject gameObject)
            => Control(identifier, gameObject, default(RectTransform));

        public static IControlConstructor Control(string identifier, GameObject gameObject, RectTransform childParentOverride)
            => new ControlConstructor(new Control(identifier, gameObject, childParentOverride));

        public static IControlConstructor<T> Control<T>(string identifier, params Type[] componentTypes)
            where T : new() => Control<T>(identifier, new GameObject(identifier, componentTypes), default);

        public static IControlConstructor<T> Control<T>(string identifier, RectTransform childParentOverride, params Type[] componentTypes)
            where T : new() => Control<T>(identifier, new GameObject(identifier, componentTypes), childParentOverride);

        public static IControlConstructor<T> Control<T>(string identifier, GameObject gameObject)
            where T : new() => Control<T>(identifier, gameObject, default);

        public static IControlConstructor<T> Control<T>(string identifier, GameObject gameObject, RectTransform childParentOverride)
            where T : new() => new ControlConstructor<T>(new Control<T>(identifier, gameObject, childParentOverride), new T());

        public static IControlConstructor<T> Control<T>(string identifier, PrimitiveControl<T> prefab)
            where T : new() => new ControlConstructor<T>(UnityEngine.Object.Instantiate(prefab), new T());

        public static IControlConstructor<TextProps> Text(string identifier = "text", TextProps props = default, PrimitiveControl<TextProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.text : prefab);

        public static IControlConstructor<ImageProps> Image(string identifier = "image", ImageProps props = default, PrimitiveControl<ImageProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.image : prefab);

        public static IControlConstructor<ButtonProps> Button(string identifier = "button", ButtonProps props = default, PrimitiveControl<ButtonProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.button : prefab);

        public static IControlConstructor<LayoutProps> HorizontalLayout(string identifier = "horizontalLayout", LayoutProps props = default, PrimitiveControl<LayoutProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.horizontalLayout : prefab);

        public static IControlConstructor<LayoutProps> VerticalLayout(string identifier = "verticalLayout.layout", LayoutProps props = default, PrimitiveControl<LayoutProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.verticalLayout : prefab);

        public static IControlConstructor<InputFieldProps> InputField(string identifier = "inputField", InputFieldProps props = default, PrimitiveControl<InputFieldProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.inputField : prefab);

        public static IControlConstructor<InputFieldProps<float>> FloatField(string identifier = "floatField", InputFieldProps<float> props = default, PrimitiveControl<InputFieldProps<float>> prefab = default)
        {
            if (prefab != null)
                return Control(identifier, props, prefab);

            props = props ?? new InputFieldProps<float>();
            var inputField = InputField($"{identifier}.inputField", props.inputField);
            var control = Control<InputFieldProps>(identifier, gameObject: inputField.control.gameObject);
            props.value.From(float.TryParse(inputField.props.value.value, out var value) ? value : 0);
            props.inputField.onEndEdit.From(x => props.value.From(float.TryParse(x, out var value) ? value : 0));
            control.AddBinding(
                inputField,
                props.value.Subscribe(x => props.inputField.value.From(x.currentValue.ToString()))
            );

            return control;
        }

        public static IControlConstructor<InputFieldProps<int>> IntField(string identifier = "intField", InputFieldProps<int> props = default, PrimitiveControl<InputFieldProps<int>> prefab = default)
        {
            if (prefab != null)
                return Control(identifier, props, prefab);

            props = props ?? new InputFieldProps<int>();
            var inputField = InputField($"{identifier}.inputField", props.inputField);
            var control = Control(identifier, props, inputField.gameObject);
            props.value.From(int.TryParse(inputField.props.value.value, out var value) ? value : 0);
            props.inputField.onEndEdit.From(x => props.value.From(int.TryParse(x, out var value) ? value : 0));
            control.AddBinding(
                inputField,
                props.value.Subscribe(x => props.inputField.value.From(x.currentValue.ToString()))
            );

            return control;
        }

        public static IControlConstructor<InputFieldProps<double>> DoubleField(string identifier = "doubleField", InputFieldProps<double> props = default, PrimitiveControl<InputFieldProps<double>> prefab = default)
        {
            if (prefab != null)
                return Control(identifier, props, prefab);

            props = props ?? new InputFieldProps<double>();
            var inputField = InputField($"{identifier}.inputField", props.inputField);
            var control = Control(identifier, props, inputField.gameObject);
            props.value.From(int.TryParse(inputField.props.value.value, out var value) ? value : 0);
            props.inputField.onEndEdit.From(x => props.value.From(double.TryParse(x, out var value) ? value : 0));
            control.AddBinding(
                inputField,
                props.value.Subscribe(x => props.inputField.value.From(x.currentValue.ToString()))
            );

            return control;
        }

        public static IControl Space(string identifier = "space")
            => Control(identifier);

        public static IControlConstructor<ScrollbarProps> Scrollbar(string identifier = "scrollbar", ScrollbarProps props = default, PrimitiveControl<ScrollbarProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.scrollbar : prefab);

        public static IControlConstructor<ScrollRectProps> ScrollRect(string identifier = "scrollRect", ScrollRectProps props = default, PrimitiveControl<ScrollRectProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.scrollRect : prefab);

        public static IControlConstructor<DropdownProps> Dropdown(string identifier = "dropdown", DropdownProps props = default, PrimitiveControl<DropdownProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.dropdown : prefab);

        public static IControlConstructor<ToggleProps> Toggle(string identifier = "toggle", ToggleProps props = default, PrimitiveControl<ToggleProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.toggle : prefab);

        public static IControlConstructor<SliderProps> Slider(string identifier = "slider", SliderProps props = default, PrimitiveControl<SliderProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.slider : prefab);
    }
}
