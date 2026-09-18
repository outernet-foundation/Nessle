using System;
using System.Collections.Generic;
using ObserveThing;
using UnityEngine;
using TMP_ContentType = TMPro.TMP_InputField.ContentType;

using static Nessle.Props;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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


        // public struct NestedMenuProps
        // {
        //     public IValueObservable<float> indent;
        //     public IValueObservable<string> label;
        //     public IValueObservable<bool> foldout;
        //     public IListObservable<IControl> inlineContent;
        //     public IListObservable<NestedMenuProps> children;
        // }

        // public static IControl NestedMenu(NestedMenuProps props = default, ElementProps element = default, LayoutProps layout = default)
        // {
        //     return VerticalLayout(
        //         element: element,
        //         layout: layout,
        //         props: new()
        //         {
        //             childControlWidth = Value(true),
        //             childControlHeight = Value(true),
        //             childForceExpandWidth = Value(true),
        //             children = List(
        //                 HorizontalLayout(new()
        //                 {
        //                     children = List(
        //                         HorizontalLayout(new()
        //                         {
        //                             children = List(
        //                                 Control(layout: new() { preferredWidth = props.indent }),
        //                                 Text(new() { value = props.label })
        //                             )
        //                         }),
        //                         HorizontalLayout(new()
        //                         {
        //                             children =
        //                         })
        //                     )
        //                 }),
        //                 VerticalLayout()
        //             )
        //         }
        //     );
        // }

        // public static IListObservable<T> ObservableAsList<T>(this IValueObservable<T> source)
        //     =>

        // public struct MenuGridProps
        // {
        //     public IListObservable<float> columnWidths;
        //     public IValueObservable<bool> normalizeWidths;
        //     public IListObservable<MenuRowProps> rows;
        // }

        // public struct MenuRowProps
        // {
        //     public IListObservable<IControl> elements;
        // }

        // public struct ColumnsProps
        // {
        //     public IValueObservable<float> spacing;
        //     public IListObservable<IControl> columns;
        // }

        // public static IControl MenuGrid(MenuGridProps props = default, ElementProps element = default, LayoutProps layout = default)
        // {
        //     List<ObservableValue<float>> columnWidths = new List<ObservableValue<float>>();

        //     props.rows.ObservableCreate(x => HorizontalLayout(new()
        //     {
        //         childControlWidth = Value(true),
        //         childControlHeight = Value(true),

        //     }));

        //     Action layoutColumns = () =>
        //     {
        //         float step = 1f / columns.Count;

        //         for (int i = 0; i < columns.Count; i++)
        //         {
        //             var child = columns[i];
        //             child.rectTransform.anchorMin = new Vector2(step * i, 0);
        //             child.rectTransform.anchorMax = new Vector2(step * (i + 1), 1);

        //             child.rectTransform.offsetMin = new Vector2(Mathf.Lerp(0f, spacingValue, i * step), 0);
        //             child.rectTransform.offsetMax = new Vector2(Mathf.Lerp(-spacingValue, 0f, (i + 1) * step), 0);
        //         }
        //     };

        //     props.spacing = props.spacing ?? Value(0f);
        //     List<IControl> columns = new List<IControl>();
        //     float spacingValue = 0;

        //     var control = Control(
        //         element: element,
        //         layout: layout,
        //         children: props.columns
        //     );



        //     control.AddBinding(
        //         props.columns?.Subscribe(
        //             onAdd: (index, x) =>
        //             {
        //                 columns.Insert(index, x);
        //                 layoutColumns();
        //             },
        //             onRemove: (index, x) =>
        //             {
        //                 columns.RemoveAt(index);
        //                 layoutColumns();
        //             }
        //         ),
        //         props.spacing?.Subscribe(
        //             onNext: x =>
        //             {
        //                 spacingValue = x;
        //                 layoutColumns();
        //             }
        //         )
        //     );

        //     return control;
        // }
    }
}
