# Welcome!

Welcome to Nessle! Nessle is a declarative UI library designed on top of the `ObserveThing` observability library. Nessle uses a React-like approach to UI definition to create reusable, reskinnable, code-first interfaces which don't clutter your project with prefabs or scenes. Currently, Nessle is implemented in Unity using it's `GameObject`-driven UI system, but further implementations in other game engines are slated for the near future.

## Controls

In Nessle, all UI is made up of controls. A control is anything which displays information to the user. That means an image, a block of text, a button are all controls. Controls are constructed using static “factory methods”, which take a set of configuration arguments and return an `IControl` object representing the requested control.

Controls are implemented declaratively. This means that they are given a set of rules when they are created, and they adhere to those rules for their entire lifecycle. These rules are called “props” (short for properties). Each prop represents some significant value in the control’s expression, and the value of any prop can update during the control’s lifecycle. When a prop’s value updates, the control handles updating its expression and publishing it to the user.

Controls maintain minimal internal logic and are not aware of the external context in which they are implemented. Similarly, external code is unaware of the inner workings of each control and does not have access to any of the control’s members, including any children the control might have. The sole means for external code to communicate with a control (and vice-versa) is to pass props into the control during the control’s setup step. Once the larger application context no longer needs a given control, it is disposed, and its resources are cleaned up.

> NOTE: In limited cases it is inconvenient to access specific external resources via props. In these cases controls are permitted to access static resources directly, usually provided by a single large data object referred to as `state`. Keeping these static references to a minimum ensures the reusability of controls.

## Props

Props are declarative sets of reactive properties controls consume to evaluate their representation. By convention, props in Nessle are `structs`. This ensures that the values passed into a control do not change after the control is generated.

### Prop Types

All controls take three props objects. The first is a custom type declared specifically for that specific control. These types are usually named using the convention `{ControlType}Props` (i.e. `ButtonProps`, `ImageProps`, `LayoutProps`, etc). These types typically contain some combination of three main types of fields: `ObserveThing` types for reactive properties, delegates for callbacks, and other props types to allow consuming code to completely override the properties of specific child controls.

Next is `element` (of type `ElementProps`). `Element` contains common fields for all controls: name (the name of the control), active (whether the control is active in the hierarchy) and bindings (a list of additional bindings this control should manage). You may notice that `children` is not part of `ElementProps`. This is because not all controls allow children or manage them- this is one of the key ways in which controls differ from `GameObjects`.

Finally, there is `layout` (of type `LayoutProps`). `Layout` contains fields for positioning and resizing the control, as well as determining how the control behaves if it is being managed by Unity’s layout system. Developers familiar with Unity will notice that several of the fields in `layout` are not part of the `Transform` component, nor do they share a common component between themselves. Instead of requiring developers to add or manage additional layout components, Nessle detects if specific fields have been set in `layout` (such as `fitContentVertical` from `ContentSizeFitter` or `preferredWidth` from `LayoutElement`) and adds the required components dynamically.

### Defaulting

Props contain no default values, and any Prop field left empty is **ignored** by the consuming control. Whatever value was **already present** in the control is preserved, it **will not** be reset to a default value. For example, if a control has a `color` prop and the element the color refers to is red, that element will **remain red** if no value is passed into the `color` prop. It **will not** and **should not** become clear, even though this is Unity's default color value (`Color(0,0,0,0)`). This nuance is important, because it ensures that adding new props fields does not alter previously-implemented instances of the control.

## Primitives

In Nessle, “primitives” refer to controls which represent a single piece of data and have no sub-controls. Things like Toggles, Input Fields, Dropdowns, and Buttons are all primitives. When setting up your project, you will need to generate a set of primitives for Nessle to use and assign them to an instance of the `UIPrimitiveSet` `ScriptableObject`. `Nessle.Primitives` provides factory methods and props for all the primitives found in `UIPrimitiveSet`. Once you have an evaluated instance of `UIPrimitiveSet`, it must be assigned to the `Nessle.Primitives.PrimitiveSet` static field. Instances of the primitives in this set will be used whenever their relevant factory methods are called. If the value of `Nessle.Primitives.PrimitiveSet` changes during the lifecycle of the application, controls already generated with the previous set will **not** automatically update to use the new primitives.

> NOTE: If you do not wish to generate your own UI primitives, or if you would like an example of what a complete `UIPrimitiveSet` would look like, Nessle provides a premade set in the `UI Primitives` sample.

## Bindings

Most developers will not interact directly with bindings when using Nessle. A binding in it's most basic form is an object which implements the `IDisposable` interface, and has been passed to a control using `element.bindinds.Add` or `AddBinding` from within a control component. These objects are disposed when the control is disposed. Bindings are almost exclusively used to manage subscriptions to props, but there is no requirements placed on bindings beyond that they implement the `IDisposable` interface.

## Passing Static Prop Values

By convention, nearly all fields in props objects are `Observables` from `ObserveThing`. This allows controls to upate their representation when props values change. However, it is not uncommon for consuming code to set the value of a prop field once and never update it. These static values must still be wrapped in an `Observable` to satisfy the type requirements of the props field:

```csharp
using Nessle;

public class Foo
{
    public void Bar()
    {
        var text = Primitives.VerticalLayout(new()
        {
            children = new ObservableList(
                Primitives.Text(new()
                {
                    value = new ObservableValue("Item in a list"),
                    style = new()
                    {
                        color = new ObservableValue(Color.red),
                        overflowMode = new ObservableValue(TMPro.TextOverflowModes.Ellipsis),
                        fontStyle = new ObservableValue(TMPro.FontStyles.Bold)
                    }
                })
            );
        });
    }
}
```

This is a bit cumbersome. So for convenience, Nessle provides a `Props` static class with methods for constructing static values, lists, sets and dictionaries. Statically referencing `Props` makes the above code much more legible:

```csharp
using Nessle;
using static Nessle.Props;

public class Foo
{
    public void Bar()
    {
        var text = Primitives.VerticalLayout(new()
        {
            children = List(
                Primitives.Text(new()
                {
                    value = Value("Item in a list"),
                    style = new()
                    {
                        color = Value(Color.red),
                        overflowMode = Value(TMPro.TextOverflowModes.Ellipsis),
                        fontStyle = Value(TMPro.FontStyles.Bold)
                    }
                })
            );
        });
    }
}
```

Additionally, we can statically reference `Primitives` to make things even more clear:

```csharp
using Nessle;
using static Nessle.Props;
using static Nessle.Primitives;

public class Foo
{
    public void Bar()
    {
        var text = VerticalLayout(new()
        {
            children = List(
                Text(new()
                {
                    value = Value("Item in a list"),
                    style = new()
                    {
                        color = Value(Color.red),
                        overflowMode = Value(TMPro.TextOverflowModes.Ellipsis),
                        fontStyle = Value(TMPro.FontStyles.Bold)
                    }
                })
            );
        });
    }
}
```

## Writing Your Own Controls

### Compound Controls

Most controls are just sets of other, more primitive controls. These "compound controls" consist of a unique props type and factory method, but don't require any additional components or prefabs to function. For example:

```csharp
using System;
using Nessle;
using ObserveThing;
using UnityEngine;

using static Nessle.Primitives;
using static Nessle.Props;

public static class Controls
{
    public struct ToggleButtonProps
    {
        public IValueObservable<string> onLabel;
        public IValueObservable<string> offLabel;
        public IValueObservable<Color> onColor;
        public IValueObservable<Color> offColor;
        public IValueObservable<bool> value;
        public Action<bool> onValueChanged;
    }

    public static IControl ToggleButton(
        ToggleButtonProps props = default,
        ElementProps element = default,
        LayoutProps layout = default
    )
    {
        var valueInternal = ObservableValue<bool>();

        element.bindings.Add(
            props.value?.Subscribe(x => valueInternal.value = x),
            valueInternal.Subscribe(x => props.onValueChanged?.Invoke(value))
        );

        return Button(
            element: element,
            layout: layout,
            props: new()
            {
                background = new() {
                    style = new() {
                        color = valueInternal.ObservableSelect(x => x ? props.onColor : props.offColor)
                    }
                },
                content = Value(
                    Text(new()
                    {
                        value = valueInternal.ObservableSelect(x => x ? props.onLabel : props.offLabel),
                        style = new() { overflowMode = Value(TMPro.TextOverflowModes.Ellipsis) }
                    })
                ),
                onClick = () => valueInternal.value = !valueInternal.value
            }
        );
    }
}
```

As you can see, the above control is made up of two distinct controls: the `Button`, which constitutes the majority of the control, and the `Text`, which is being used as a label. `Element` and `layout` are passed into the button at the top, which will handle those values internally. The `Button`'s props are then constructed based on the values passed in `ToggleButtonProps`. The color of the button's background and the text of the label are driven by `props.value`, routed through `valueInternal`. This is a common technique for handling cases where consuming code may not pass a props field you need, or when you need to read/modify the value from within a control. This line:

```csharp
props.value?.Subscribe(x => valueInternal.value = x)
```

Ensures that whenever consuming code updates the value of `props.value`, the change gets propagated to the rest of the control. This subscription, along with the subscription to `valueInternal` are passed as bindings to the `Button`, and will be cleaned up when the control is disposed. Finally, the `Button`'s `onClick` property is set to toggle `valueInternal` on and off:

```csharp
onClick = () => valueInternal.value = !valueInternal.value
```

Once the control is set up, it can be reused depending on the consuming context's needs:

```csharp
using System;
using Nessle;
using ObserveThing;
using UnityEngine;

using static Nessle.Primitives;
using static Nessle.Props;

public class Foo
{
    public void Bar()
    {
        var isOn = new ObservableValue<bool>();
        var ready = new ObservableValue<bool>();

        var onOffButton = ToggleButton(new()
        {
            value = isOn,
            onColor = Value(Color.green),
            offColor = Value(Color.red),
            onLabel = Value("On"),
            offLabel = Value("Off"),
            onValueChanged = x => isOn.value = x
        });

        var readyButton = ToggleButton(new()
        {
            value = ready,
            onColor = Value(Color.yellow),
            offColor = Value(Color.grey),
            onLabel = Value("Ready!"),
            offLabel = Value("Not Ready"),
            onValueChanged = x => ready.value = x
        })
    }
}
```

### Component Controls

Sometimes it is more convenient for a control to be implemented as a prefab, either because it has visual requirements that can only be expressed directly in-engine (like animation or irregular layouts) or because it is a pre-existing UI element which has not yet been (or may never be) ported to the Nessle framework. In these cases, you can simply extend the `Control` class:

```csharp
public struct DialProps
{
    public IValueObservable<float> value;
    public Action<float> onValueChanged;
}

public class DialControl : Control<DialProps>, IDragHandler
{
    public Transform dialTransform;

    public void OnDrag(PointerEventData eventData)
    {
        var worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        var localPosition = transform.InverseTransformPoint(worldPosition);

        var angle = Vector2.Angle(new Vector2(0, 1), localPosition);

        props.onValueChanged?.Invoke(angle);
    }

    protected override void SetupInternal()
    {
        AddBinding(
            props.value?.Subscribe(x => dialTransform.localRotation = Quaternion.AngleAxis(x, Vector3.forward))
        );
    }
}
```

The `Control<T>` component provides `SetupInternal` and `DisposeInternal` methods for child classes to override. By the time `SetupInternal` is called, `props`, `element`, and `layout` are all evaluated. `Control<T>` automatically binds to `element` and `layout` properties. Within `SetupInternal`, the child class can use `AddBinding` to add additional bindings to the control's lifecycle. They will be automatically cleaned up when the control is disposed.

Once a control prefab is generated, it should be added somewhere it can be easily statically accessed. This could be by adding it directly to a static class, or by mimicking the pattern used by `Nessle.Primitives`:

```csharp
// Add directly

using Nessle;

using static Nessle.Primitives;

public static class Controls
{
    public static Control<DialProps> DialControl;

    public static IControl Dial(DialProps props = default, ElementProps element = default, LayoutProps layout = default)
        => Control(DialControl, props, element, layout);
}
```

```csharp
// Use Nessle.Primitives pattern

using Nessle;

using static Nessle.Primitives;

public class ControlSet : ScriptableObject
{
    public Control<DialProps> dial => _dial;

    [SerializeField]
    private Control<DialProps> _dial;
}

public static class Controls
{
    public static ControlSet ControlSet;

    public static IControl Dial(DialProps props = default, ElementProps element = default, LayoutProps layout = default)
        => Control(ControlSet.dial, props, element, layout);
}
```

Regardless of how the new control is referenced, constructing it in client code becomes trivial, and looks just like native Nessle:

```csharp
using Nessle;
using static Nessle.Primitives;
using static Controls;

public class Foo
{
    public void Bar()
    {
        Canvas(new()
        {
            children = List(
                Dial(
                    layout: new() { localPosition = Value(new Vector3(300, 300)) }
                    props: new() { onValueChanged = x => Debug.Log($"Value set to {x}") }
                )
            )
        })
    }
}
```