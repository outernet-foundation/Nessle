using System;
using ObserveThing;
using UnityEngine;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;

namespace Nessle
{
    public static class UIBuilder
    {
        public static UIPrimitiveSet primitives { get; set; }

        public static IControl Control(GameObject gameObject, RectTransform childParentOverride = default)
            => new Control(gameObject, childParentOverride);

        public static IControl Control<T>(T props, PrimitiveControl<T> prefab) where T : new()
        {
            var control = UnityEngine.Object.Instantiate(prefab);
            control.Setup(props);
            return control;
        }

        public static IControl Text(TextProps props = default, PrimitiveControl<TextProps> prefab = default)
            => Control(props, prefab == null ? primitives.text : prefab);

        public static IControl Image(ImageProps props = default, PrimitiveControl<ImageProps> prefab = default)
            => Control(props, prefab == null ? primitives.image : prefab);

        public static IControl Button(ButtonProps props = default, PrimitiveControl<ButtonProps> prefab = default)
            => Control(props, prefab == null ? primitives.button : prefab);

        public static IControl HorizontalLayout(LayoutProps props = default, PrimitiveControl<LayoutProps> prefab = default)
            => Control(props, prefab == null ? primitives.horizontalLayout : prefab);

        public static IControl VerticalLayout(LayoutProps props = default, PrimitiveControl<LayoutProps> prefab = default)
            => Control(props, prefab == null ? primitives.verticalLayout : prefab);

        public static IControl InputField(InputFieldProps props = default, PrimitiveControl<InputFieldProps> prefab = default)
            => Control(props, prefab == null ? primitives.inputField : prefab);

        public static IControl FloatField(InputFieldProps<float> props = default, PrimitiveControl<InputFieldProps> prefab = default)
        {
            return InputField(
                new InputFieldProps()
                {
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

        public static IControl IntField(InputFieldProps<int> props = default, PrimitiveControl<InputFieldProps> prefab = default)
        {
            return InputField(
                new InputFieldProps()
                {
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

        public static IControl DoubleField(InputFieldProps<double> props = default, PrimitiveControl<InputFieldProps> prefab = default)
        {
            return InputField(
                new InputFieldProps()
                {
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
            => Control(new GameObject("Space"));

        public static IControl Scrollbar(ScrollbarProps props = default, PrimitiveControl<ScrollbarProps> prefab = default)
            => Control(props, prefab == null ? primitives.scrollbar : prefab);

        public static IControl ScrollRect(ScrollRectProps props = default, PrimitiveControl<ScrollRectProps> prefab = default)
            => Control(props, prefab == null ? primitives.scrollRect : prefab);

        public static IControl Dropdown(DropdownProps props = default, PrimitiveControl<DropdownProps> prefab = default)
            => Control(props, prefab == null ? primitives.dropdown : prefab);

        public static IControl Toggle(ToggleProps props = default, PrimitiveControl<ToggleProps> prefab = default)
            => Control(props, prefab == null ? primitives.toggle : prefab);

        public static IControl Slider(SliderProps props = default, PrimitiveControl<SliderProps> prefab = default)
            => Control(props, prefab == null ? primitives.slider : prefab);
    }
}
