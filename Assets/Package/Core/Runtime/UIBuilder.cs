using System;
using ObserveThing;
using UnityEngine;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;

using static Nessle.Props;

namespace Nessle
{
    public static class UIBuilder
    {
        public static UIPrimitiveSet primitives { get; set; }

        public static IControl Control<T>(Control<T> prefab, T props = default, ElementProps element = default, LayoutProps layout = default)
        {
            var control = UnityEngine.Object.Instantiate(prefab);
            control.Setup(props, element, layout);
            return control;
        }

        public static IControl Control(ElementProps element = default, LayoutProps layout = default, IListObservable<IControl> children = default)
        {
            var control = new GameObject("Control", typeof(RectTransform)).AddComponent<Control>();
            control.Setup(children, element, layout);
            return control;
        }

        public static IControl Text(TextProps props = default, ElementProps element = default, LayoutProps layout = default, Control<TextProps> prefab = default)
            => Control(prefab ?? primitives.text, props, element, layout);

        public static IControl Image(ImageProps props = default, ElementProps element = default, LayoutProps layout = default, Control<ImageProps> prefab = default)
            => Control(prefab ?? primitives.image, props, element, layout);

        public static IControl Button(ButtonProps props = default, ElementProps element = default, LayoutProps layout = default, Control<ButtonProps> prefab = default)
            => Control(prefab ?? primitives.button, props, element, layout);

        public static IControl HorizontalLayout(LayoutGroupProps props = default, ElementProps element = default, LayoutProps layout = default, Control<LayoutGroupProps> prefab = default)
            => Control(prefab ?? primitives.horizontalLayout, props, element, layout);

        public static IControl VerticalLayout(LayoutGroupProps props = default, ElementProps element = default, LayoutProps layout = default, Control<LayoutGroupProps> prefab = default)
            => Control(prefab ?? primitives.verticalLayout, props, element, layout);

        public static IControl InputField(InputFieldProps props = default, ElementProps element = default, LayoutProps layout = default, Control<InputFieldProps> prefab = default)
            => Control(prefab ?? primitives.inputField, props, element, layout);

        public static IControl FloatField(InputFieldProps<float> props = default, ElementProps element = default, LayoutProps layout = default, Control<InputFieldProps> prefab = default)
        {
            return Control(
                prefab ?? primitives.inputField,
                new InputFieldProps()
                {
                    value = props.value.ObservableSelect(x => x.ToString()),
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
                },
                element,
                layout
            );
        }

        public static IControl IntField(InputFieldProps<int> props = default, ElementProps element = default, LayoutProps layout = default, Control<InputFieldProps> prefab = default)
        {
            return Control(
                prefab ?? primitives.inputField,
                new InputFieldProps()
                {
                    value = props.value.ObservableSelect(x => x.ToString()),
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
                },
                element,
                layout
            );
        }

        public static IControl DoubleField(InputFieldProps<double> props = default, ElementProps element = default, LayoutProps layout = default, Control<InputFieldProps> prefab = default)
        {
            return Control(
                prefab ?? primitives.inputField,
                new InputFieldProps()
                {
                    value = props.value.ObservableSelect(x => x.ToString()),
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
                },
                element,
                layout
            );
        }

        public static IControl Scrollbar(ScrollbarProps props = default, ElementProps element = default, LayoutProps layout = default, Control<ScrollbarProps> prefab = default)
            => Control(prefab ?? primitives.scrollbar, props, element, layout);

        public static IControl ScrollRect(ScrollRectProps props = default, ElementProps element = default, LayoutProps layout = default, Control<ScrollRectProps> prefab = default)
            => Control(prefab ?? primitives.scrollRect, props, element, layout);

        public static IControl Dropdown(DropdownProps props = default, ElementProps element = default, LayoutProps layout = default, Control<DropdownProps> prefab = default)
            => Control(prefab ?? primitives.dropdown, props, element, layout);

        public static IControl Toggle(ToggleProps props = default, ElementProps element = default, LayoutProps layout = default, Control<ToggleProps> prefab = default)
            => Control(prefab ?? primitives.toggle, props, element, layout);

        public static IControl Slider(SliderProps props = default, ElementProps element = default, LayoutProps layout = default, Control<SliderProps> prefab = default)
            => Control(prefab ?? primitives.slider, props, element, layout);

        public static IControl Canvas(CanvasProps props = default, ElementProps element = default, LayoutProps layout = default, Control<CanvasProps> prefab = default)
            => Control(prefab ?? primitives.canvas, props, element, layout);
    }
}
