using System;
using UnityEngine;

namespace Nessle
{
    public static class UIBuilder
    {
        public static UIPrimitiveSet primitives { get; set; }

        public static IControl Control(string identifier, params Type[] componentTypes)
            => Control(identifier, new GameObject(identifier, componentTypes), default(RectTransform));

        public static IControl Control(string identifier, RectTransform childParentOverride, params Type[] componentTypes)
            => Control(identifier, new GameObject(identifier, componentTypes), childParentOverride);

        public static IControl Control(string identifier, GameObject gameObject)
            => Control(identifier, gameObject, default(RectTransform));

        public static IControl Control(string identifier, GameObject gameObject, RectTransform childParentOverride)
            => new Control(identifier, gameObject, childParentOverride);

        public static IControl<T> Control<T>(string identifier, T props, params Type[] componentTypes)
            => Control(identifier, props, new GameObject(identifier, componentTypes), default);

        public static IControl<T> Control<T>(string identifier, T props, RectTransform childParentOverride, params Type[] componentTypes)
            => Control(identifier, props, new GameObject(identifier, componentTypes), childParentOverride);

        public static IControl<T> Control<T>(string identifier, T props, GameObject gameObject)
            => Control(identifier, props, gameObject, default);

        public static IControl<T> Control<T>(string identifier, T props, GameObject gameObject, RectTransform childParentOverride)
            => new Control<T>(identifier, props, gameObject, childParentOverride);

        public static PrimitiveControl<T> Control<T>(string identifier, T props, PrimitiveControl<T> prefab)
            where T : new()
        {
            var control = UnityEngine.Object.Instantiate(prefab);
            control.Setup(identifier, props);
            return control;
        }

        public static PrimitiveControl<TextProps> Text(string identifier = "text", TextProps props = default, PrimitiveControl<TextProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.text : prefab);

        public static PrimitiveControl<ImageProps> Image(string identifier = "image", ImageProps props = default, PrimitiveControl<ImageProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.image : prefab);

        public static PrimitiveControl<ButtonProps> Button(string identifier = "button", ButtonProps props = default, PrimitiveControl<ButtonProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.button : prefab);

        public static PrimitiveControl<LayoutProps> HorizontalLayout(string identifier = "horizontalLayout", LayoutProps props = default, PrimitiveControl<LayoutProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.horizontalLayout : prefab);

        public static PrimitiveControl<LayoutProps> VerticalLayout(string identifier = "verticalLayout.layout", LayoutProps props = default, PrimitiveControl<LayoutProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.verticalLayout : prefab);

        public static PrimitiveControl<InputFieldProps> InputField(string identifier = "inputField", InputFieldProps props = default, PrimitiveControl<InputFieldProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.inputField : prefab);

        public static IControl<InputFieldProps<float>> FloatField(string identifier = "floatField", InputFieldProps<float> props = default, PrimitiveControl<InputFieldProps<float>> prefab = default)
        {
            if (prefab != null)
                return Control(identifier, props, prefab);

            props = props ?? new InputFieldProps<float>();
            var inputField = InputField($"{identifier}.inputField", props.inputField);
            var control = Control(identifier, props, inputField.gameObject);
            props.value.From(float.TryParse(inputField.props.value.value, out var value) ? value : 0);
            props.inputField.onEndEdit.From(x => props.value.From(float.TryParse(x, out var value) ? value : 0));
            return control;
        }

        public static IControl<InputFieldProps<int>> IntField(string identifier = "intField", InputFieldProps<int> props = default, PrimitiveControl<InputFieldProps<int>> prefab = default)
        {
            if (prefab != null)
                return Control(identifier, props, prefab);

            props = props ?? new InputFieldProps<int>();
            var inputField = InputField($"{identifier}.inputField", props.inputField);
            var control = Control(identifier, props, inputField.gameObject);
            props.value.From(int.TryParse(inputField.props.value.value, out var value) ? value : 0);
            props.inputField.onEndEdit.From(x => props.value.From(int.TryParse(x, out var value) ? value : 0));
            return control;
        }

        public static IControl<InputFieldProps<double>> DoubleField(string identifier = "doubleField", InputFieldProps<double> props = default, PrimitiveControl<InputFieldProps<double>> prefab = default)
        {
            if (prefab != null)
                return Control(identifier, props, prefab);

            props = props ?? new InputFieldProps<double>();
            var inputField = InputField($"{identifier}.inputField", props.inputField);
            var control = Control(identifier, props, inputField.gameObject);
            props.value.From(int.TryParse(inputField.props.value.value, out var value) ? value : 0);
            props.inputField.onEndEdit.From(x => props.value.From(double.TryParse(x, out var value) ? value : 0));
            return control;
        }

        public static IControl Space(string identifier = "space")
            => Control(identifier);

        public static PrimitiveControl<ScrollbarProps> Scrollbar(string identifier = "scrollbar", ScrollbarProps props = default, PrimitiveControl<ScrollbarProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.scrollbar : prefab);

        public static PrimitiveControl<ScrollRectProps> ScrollRect(string identifier = "scrollRect", ScrollRectProps props = default, PrimitiveControl<ScrollRectProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.scrollRect : prefab);

        public static PrimitiveControl<DropdownProps> Dropdown(string identifier = "dropdown", DropdownProps props = default, PrimitiveControl<DropdownProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.dropdown : prefab);

        public static PrimitiveControl<ToggleProps> Toggle(string identifier = "toggle", ToggleProps props = default, PrimitiveControl<ToggleProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.toggle : prefab);

        public static PrimitiveControl<SliderProps> Slider(string identifier = "slider", SliderProps props = default, PrimitiveControl<SliderProps> prefab = default)
            => Control(identifier, props, prefab == null ? primitives.slider : prefab);
    }
}
