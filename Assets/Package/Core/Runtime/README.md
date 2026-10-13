# Welcome!

Welcome to Nessle! Nessle is a declarative UI library designed on top of the `ObserveThing` observability library. Nessle uses a React-like approach to UI definition to create reusable, reskinnable, code-first interfaces which don't clutter your project with prefabs or scenes. Currently, Nessle is implemented in Unity using it's `GameObject`-driven UI system, but further implementations in other game engines are slated for the near future.

## Controls

In Nessle, all UI is made up of controls. A control is anything which displays information to the user. That means an image, a block of text, a button are all controls. Controls are constructed using static “factory methods”, which take a set of configuration arguments and return an `IControl` object representing the requested control.

Controls are implemented declaratively. This means that they are given a set of rules when they are created, and they adhere to those rules for their entire lifecycle. These rules are called “props” (short for properties). Each prop represents some significant value in the control’s expression, and the value of any prop can update during the control’s lifecycle. When a prop’s value updates, the control handles updating its expression and publishing it to the user.

Controls maintain minimal internal logic and are not aware of the external context in which they are implemented. Similarly, external code is unaware of the inner workings of each control and does not have access to any of the control’s members, including any children the control might have. The sole means for external code to communicate with a control (and vice-versa) is to pass props into the control during the control’s setup step. Once the larger application context no longer needs a given control, it is disposed, and its resources are cleaned up.

> NOTE: In limited cases it is inconvenient to access specific external resources via props. In these cases controls are permitted to access static resources directly, usually provided by a single large data object referred to as `state`. Keeping these static references to a minimum ensures the reusability of controls.

## Props

Props are declarative sets of reactive properties controls consume to evaluate their representation. By convention, props in Nessle are `structs`. This ensures that the values passed into a control do not change after the control is generated. Props contain no default values, and any Prop field left empty is **ignored** by the consuming control. Whatever value was **already present** in the control is preserved, it **will not** be reset to a default value. For example, if a control has a `color` prop and the element the color refers to is red, that element will **remain red** if no value is passed into the `color` prop. This nuance is important, because it ensures that adding new props fields does not alter previously-implemented instances of the control.

All controls take three props objects. The first is a custom type declared specifically for that specific control. These types are usually named using the convention `{ControlType}Props` (i.e. `ButtonProps`, `ImageProps`, `LayoutProps`, etc). These types typically contain some combination of three main types of fields: `ObserveThing` types for reactive properties, delegates for callbacks, and other props types to allow consuming code to completely override the properties of specific child controls.

Next is `element` (of type `ElementProps`). `Element` contains common fields for all controls: name (the name of the control), active (whether the control is active in the hierarchy) and bindings (a list of additional bindings this control should manage). You may notice that `children` is not part of `ElementProps`. This is because not all controls allow children or manage them- this is one of the key ways in which controls differ from `GameObjects`.

Finally, there is `layout` (of type `LayoutProps`). `Layout` contains fields for positioning and resizing the control, as well as determining how the control behaves if it is being managed by Unity’s layout system. Developers familiar with Unity will notice that several of the fields in `layout` are not part of the `Transform` component, nor do they share a common component between themselves. Instead of requiring developers to add or manage additional layout components, Nessle detects if specific fields have been set in `layout` (such as `fitContentVertical` from `ContentSizeFitter` or `preferredWidth` from `LayoutElement`) and adds the required components dynamically.

## Primitives

In Nessle, “primitives” refer to controls which represent a single piece of data and have no sub-controls. Things like Toggles, Input Fields, Dropdowns, and Buttons are all primitives. When setting up your project, you will need to generate a set of primitives for Nessle to use and assign them to an instance of the `UIPrimitiveSet` `ScriptableObject`. `Nessle.Primitives` provides factory methods and props for all the primitives found in `UIPrimitiveSet`. Once you have an evaluated instance of `UIPrimitiveSet`, it must be assigned to the `Nessle.Primitives.PrimitiveSet` static field. Instances of the primitives in this set will be used whenever their relevant factory methods are called. If the value of `Nessle.Primitives.PrimitiveSet` changes during the lifecycle of the application, controls already generated with the previous set will **not** automatically update to use the new primitives.

> NOTE: If you do not wish to generate your own UI primitives, or if you would like an example of what a complete `UIPrimitiveSet` would look like, Nessle provides a premade set in the `UI Primitives` sample.

## Bindings

Most developers will not interact directly with bindings when using Nessle. A binding in it's most basic form is an object which implements the `IDisposable` interface, and has been passed to a control using `element.bindinds.Add` or `AddBinding` from within a control component. These objects are disposed when the control is disposed. Bindings are almost exclusively used to manage subscriptions to props, but there is no requirements placed on bindings beyond that they implement the `IDisposable` interface.

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

public static class AdditionalControls
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

## Quick Start

Nessle only needs one configuration step in order to function- the value of `Primitives.PrimitiveSet` must be set to an instance of `UIPrimitiveSet`. `UIPrimitiveSet` is a `ScriptableObject` which defines the "primitives" (buttons, toggles, input fields, dropdowns, etc) Nessle will use to construct the UI. If you just want to test Nessle out without setting up your own primitives, the `UI Primitives` sample included with Nessle's Unity package provides a generic set to get you started. Once you have a fully evaluated instance of `UIPrimitiveSet`, simply set `Primitives.PrimitiveSet` to that instance and Nessle will be ready to roll:

```csharp
public class Foo
{
    public UIPrimitiveSet primitives;

    public void Bar()
    {
        // Nessle is not ready on this line
        Primitives.PrimitiveSet = primitives;
        // Nessle is ready on this line!
    }
}
```

You can change the value of `Primitives.PrimitiveSet` at any point. Changing this value **does not** change UI which has already been constructed.

```csharp
public class Foo
{
    public UIPrimitiveSet primitives1;
    public UIPrimitiveSet primitives2;

    public void Bar()
    {
        Primitives.PrimitiveSet = primitives1;
        // UI constructed here will use the values of primitives1
        Primitives.PrimitiveSet = primitives2;
        // UI constructed here will use the values of primitives2
        // UI previously constructed will remain unchanged
    }
}
```

Once `Primitives.PrimitiveSet` is evaluated, you can start generating controls. The easiest way to do this is to statically reference `Nessle.UIBuilder` and `Nessle.Props`. With those static references, you can directly call Nessle's construction methods without qualifying them with a static class:

```csharp
using UnityEngine.UI;

using Nessle;

using static Nessle.Primitives;
using static Nessle.Props;

public class Foo
{
    public UIPrimitiveSet primitives;
    public IControl ui;

    public void Bar()
    {
        Primitives.PrimitiveSet = primitives;
        ui = Canvas(new()
        {
            children = List(
                Button(
                    layout: new()
                    {
                        localPosition = Value(new Vector3(100, 100, 0)),
                        fitContentHorizontal = Value(FitMode.PreferredSize)
                    }
                    props: new()
                    {
                        content = List(Text(new() { value = Value("Click Me!") })),
                        onClick = () => Debug.Log("Hello World!")
                    }
                )
            )
        });
    }
}
```

There are several things at play in the above example, so let's take them one at a time. First, this line:

```csharp
ui = Canvas(new()
```

Here we construct a Unity canvas. Technically, we could pass no arguments and just get an empty Canvas in the scene:

```csharp
ui = Canvas();
```

An empty Canvas doesn't do us much good, so instead we pass a `CanvasProps` object so we can define the properties of the Canvas. Since the control-specific props object the first argument in any control construction method, we can let the compiler figure out what it's type is an just use `new()`. `CanvasProps` has many props, but for now we just define `children` so we can add the button:

```csharp
children = List(
    Button(
```

It's worth noting here that `List` in this context is **not** a C# `System.Collections.List` or `System.Collections.List<T>`. Rather, it is an `ObservableList<T>` from `ObserveThing`, supplied through Nessle's `Props` static class: `Nessle.Props.List()`. This method takes a set of values as params (in this case, controls), allowing consuming code to easily declare static lists easily in-line. By convention, _most if not all_ props fields in Nessle controls are Observable types. This allows UIs to be declarative and reactive- controls subscribe to changes in props to update their representations instead of relying on manual updates from external code.

For the button constructor method, we define two arguments: `props`, which we know already, and `layout`:

```csharp
Button(
    layout: new()
    {
        localPosition = Value(new Vector3(100, 100, 0)),
        fitContentHorizontal = Value(FitMode.PreferredSize)
    }
    props: new()
    {
        content = Value(Text(new() { value = Value("Click Me!") })),
        onClick = () => Debug.Log("Hello World!")
    }
)
```

Every control has a `layout` property, used to place and resize the control when it's location and size are not being driven by an external layout component (such at `VerticalLayout`). You'll notice that the properties of `layout` are not limited Unity's position/rotation/scale, but also include things like `fitContentHorizontal`. Nessle automatically detects when self-layout properties like this are set and adds the appropriate components to the control.
