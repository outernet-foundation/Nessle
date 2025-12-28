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
            => Control(new GameObject(name), props);

        public static IControl Control(GameObject gameObject, UIElementProps props)
        {
            var control = gameObject.GetOrAddComponent<UIElementControl>();
            control.Setup(props);
            return control;
        }

        public static IControl Control<T>(Control<T> prefab, T props) where T : new()
        {
            var control = UnityEngine.Object.Instantiate(prefab);
            control.Setup(props);
            return control;
        }

        public static IControl Text(TextProps props)
            => Control(primitives.text, props);

        public static IControl Text(Control<TextProps> prefab, TextProps props)
            => Control(prefab, props);

        public static IControl Image(ImageProps props)
            => Control(primitives.image, props);

        public static IControl Image(Control<ImageProps> prefab, ImageProps props)
            => Control(prefab, props);

        public static IControl Button(ButtonProps props)
            => Control(primitives.button, props);

        public static IControl Button(Control<ButtonProps> prefab, ButtonProps props)
            => Control(prefab, props);

        public static IControl HorizontalLayout(LayoutProps props)
            => Control(primitives.horizontalLayout, props);

        public static IControl HorizontalLayout(Control<LayoutProps> prefab, LayoutProps props)
            => Control(prefab, props);

        public static IControl VerticalLayout(LayoutProps props)
            => Control(primitives.verticalLayout, props);

        public static IControl VerticalLayout(Control<LayoutProps> prefab, LayoutProps props)
            => Control(prefab, props);

        public static IControl InputField(InputFieldProps props)
            => Control(primitives.inputField, props);

        public static IControl InputField(Control<InputFieldProps> prefab, InputFieldProps props)
            => Control(prefab, props);

        public static IControl FloatField(InputFieldProps<float> props)
            => FloatField(primitives.inputField, props);

        public static IControl FloatField(Control<InputFieldProps> prefab, InputFieldProps<float> props)
        {
            return Control(
                prefab,
                new InputFieldProps()
                {
                    element = props.element,
                    value = props.value.SelectDynamic(x => x.ToString()),
                    placeholderValue = props.placeholderValue,
                    inputTextStyle = props.inputTextStyle,
                    placeholderTextStyle = props.placeholderTextStyle,
                    contentType = Props.Value(TMP_ContentType.DecimalNumber),
                    readOnly = props.readOnly,
                    lineType = props.lineType,
                    characterLimit = props.characterLimit,
                    interactable = props.interactable,
                    onEndEdit = x => props.onValueChanged?.Invoke(float.TryParse(x, out var result) ? result : 0),
                    background = props.background
                }
            );
        }

        public static IControl IntField(InputFieldProps<int> props)
            => IntField(primitives.inputField, props);

        public static IControl IntField(Control<InputFieldProps> prefab, InputFieldProps<int> props)
        {
            return Control(
                prefab,
                new InputFieldProps()
                {
                    element = props.element,
                    value = props.value.SelectDynamic(x => x.ToString()),
                    placeholderValue = props.placeholderValue,
                    inputTextStyle = props.inputTextStyle,
                    placeholderTextStyle = props.placeholderTextStyle,
                    contentType = Props.Value(TMP_ContentType.IntegerNumber),
                    readOnly = props.readOnly,
                    lineType = props.lineType,
                    characterLimit = props.characterLimit,
                    interactable = props.interactable,
                    onEndEdit = x => props.onValueChanged?.Invoke(int.TryParse(x, out var result) ? result : 0),
                    background = props.background
                }
            );
        }

        public static IControl DoubleField(InputFieldProps<double> props)
            => DoubleField(primitives.inputField, props);

        public static IControl DoubleField(Control<InputFieldProps> prefab, InputFieldProps<double> props)
        {
            return Control(
                prefab,
                new InputFieldProps()
                {
                    element = props.element,
                    value = props.value.SelectDynamic(x => x.ToString()),
                    placeholderValue = props.placeholderValue,
                    inputTextStyle = props.inputTextStyle,
                    placeholderTextStyle = props.placeholderTextStyle,
                    contentType = Props.Value(TMP_ContentType.DecimalNumber),
                    readOnly = props.readOnly,
                    lineType = props.lineType,
                    characterLimit = props.characterLimit,
                    interactable = props.interactable,
                    onEndEdit = x => props.onValueChanged?.Invoke(double.TryParse(x, out var result) ? result : 0),
                    background = props.background
                }
            );
        }

        public static IControl Space()
            => Control("Space", new());

        public static IControl Scrollbar(ScrollbarProps props)
            => Control(primitives.scrollbar, props);

        public static IControl Scrollbar(Control<ScrollbarProps> prefab, ScrollbarProps props)
            => Control(prefab, props);

        public static IControl ScrollRect(ScrollRectProps props)
            => Control(primitives.scrollRect, props);

        public static IControl ScrollRect(Control<ScrollRectProps> prefab, ScrollRectProps props)
            => Control(prefab, props);

        public static IControl Dropdown(DropdownProps props)
            => Control(primitives.dropdown, props);

        public static IControl Dropdown(Control<DropdownProps> prefab, DropdownProps props)
            => Control(prefab, props);

        public static IControl Toggle(ToggleProps props)
            => Control(primitives.toggle, props);

        public static IControl Toggle(Control<ToggleProps> prefab, ToggleProps props)
            => Control(prefab, props);

        public static IControl Slider(SliderProps props)
            => Control(primitives.slider, props);

        public static IControl Slider(Control<SliderProps> prefab, SliderProps props)
            => Control(prefab, props);

        public static IControl FullscreenUI(FullscreenUIProps props)
            => Control(primitives.fullscreenUI, props);

        public static IControl FullscreenUI(Control<FullscreenUIProps> prefab, FullscreenUIProps props)
            => Control(prefab, props);
    }
}
