using System;
using ObserveThing;
using UnityEngine;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;

namespace Nessle
{
    public static class UIBuilder
    {
        public static UIPrimitiveSet primitives { get; set; }

        public static IControl Control(string name, UIElementProps props)
        {
            var control = new GameObject(name).GetOrAddComponent<UIElementControl>();
            control.Setup(props);
            return control;
        }

        public static IControl Control<T>(T props, Control<T> prefab) where T : new()
        {
            var control = UnityEngine.Object.Instantiate(prefab);
            control.Setup(props);
            return control;
        }

        public static IControl Text(TextProps props, Control<TextProps> prefab = default)
            => Control(props, prefab == null ? primitives.text : prefab);

        public static IControl Image(ImageProps props, Control<ImageProps> prefab = default)
            => Control(props, prefab == null ? primitives.image : prefab);

        public static IControl Button(ButtonProps props, Control<ButtonProps> prefab = default)
            => Control(props, prefab == null ? primitives.button : prefab);

        public static IControl HorizontalLayout(LayoutProps props, Control<LayoutProps> prefab = default)
            => Control(props, prefab == null ? primitives.horizontalLayout : prefab);

        public static IControl VerticalLayout(LayoutProps props, Control<LayoutProps> prefab = default)
            => Control(props, prefab == null ? primitives.verticalLayout : prefab);

        public static IControl InputField(InputFieldProps props, Control<InputFieldProps> prefab = default)
            => Control(props, prefab == null ? primitives.inputField : prefab);

        public static IControl FloatField(InputFieldProps<float> props, Control<InputFieldProps> prefab = default)
        {
            return InputField(
                new InputFieldProps()
                {
                    element = props.element,
                    value = props.value.SelectDynamic(x => x.ToString()),
                    placeholderValue = props.placeholderValue,
                    inputTextStyle = props.inputTextStyle,
                    placeholderTextStyle = props.placeholderTextStyle,
                    contentType = Props.From(TMP_ContentType.DecimalNumber),
                    readOnly = props.readOnly,
                    lineType = props.lineType,
                    characterLimit = props.characterLimit,
                    interactable = props.interactable,
                    onEndEdit = x => props.onValueChanged?.Invoke(float.TryParse(x, out var result) ? result : 0),
                    background = props.background
                },
                prefab
            );
        }

        public static IControl IntField(InputFieldProps<int> props, Control<InputFieldProps> prefab = default)
        {
            return InputField(
                new InputFieldProps()
                {
                    element = props.element,
                    value = props.value.SelectDynamic(x => x.ToString()),
                    placeholderValue = props.placeholderValue,
                    inputTextStyle = props.inputTextStyle,
                    placeholderTextStyle = props.placeholderTextStyle,
                    contentType = Props.From(TMP_ContentType.IntegerNumber),
                    readOnly = props.readOnly,
                    lineType = props.lineType,
                    characterLimit = props.characterLimit,
                    interactable = props.interactable,
                    onEndEdit = x => props.onValueChanged?.Invoke(int.TryParse(x, out var result) ? result : 0),
                    background = props.background
                },
                prefab
            );
        }

        public static IControl DoubleField(InputFieldProps<double> props, Control<InputFieldProps> prefab = default)
        {
            return InputField(
                new InputFieldProps()
                {
                    element = props.element,
                    value = props.value.SelectDynamic(x => x.ToString()),
                    placeholderValue = props.placeholderValue,
                    inputTextStyle = props.inputTextStyle,
                    placeholderTextStyle = props.placeholderTextStyle,
                    contentType = Props.From(TMP_ContentType.DecimalNumber),
                    readOnly = props.readOnly,
                    lineType = props.lineType,
                    characterLimit = props.characterLimit,
                    interactable = props.interactable,
                    onEndEdit = x => props.onValueChanged?.Invoke(double.TryParse(x, out var result) ? result : 0),
                    background = props.background
                },
                prefab
            );
        }

        public static IControl Space()
            => Control("Space", new());

        public static IControl Scrollbar(ScrollbarProps props, Control<ScrollbarProps> prefab = default)
            => Control(props, prefab == null ? primitives.scrollbar : prefab);

        public static IControl ScrollRect(ScrollRectProps props, Control<ScrollRectProps> prefab = default)
            => Control(props, prefab == null ? primitives.scrollRect : prefab);

        public static IControl Dropdown(DropdownProps props, Control<DropdownProps> prefab = default)
            => Control(props, prefab == null ? primitives.dropdown : prefab);

        public static IControl Toggle(ToggleProps props, Control<ToggleProps> prefab = default)
            => Control(props, prefab == null ? primitives.toggle : prefab);

        public static IControl Slider(SliderProps props, Control<SliderProps> prefab = default)
            => Control(props, prefab == null ? primitives.slider : prefab);
    }
}
