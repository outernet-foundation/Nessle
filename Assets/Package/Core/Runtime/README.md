# Welcome!

Welcome to Nessle! Nessle is a declarative UI library designed on top of the `ObserveThing` observability library. Nessle uses a React-like approach to UI definition to create reusable, reskinnable, code-first interfaces which don't clutter your project with prefabs or scenes. Currently, Nessle is implemented in Unity using it's `GameObject`-driven UI system, but further implementations in other game engines are slated for the near future.

## Quick Start

Nessle only needs one configuration step in order to function- the value of `UIBuilder.primitives` must be set to an instance of `UIPrimitiveSet`. `UIPrimitiveSet` is a `ScriptableObject` which defines the "primitives" (buttons, toggles, input fields, dropdowns, etc) Nessle will use to construct the UI. If you just want to test Nessle out without setting up your own primitives, the `UI Primitives` sample included with Nessle's Unity package provides a generic set to get you started. Once you have a fully evaluated instance of `UIPrimitiveSet`, simply set `UIBuilder.primitives` to that instance and Nessle will be ready to roll:

```csharp
public class Foo
{
    public UIPrimitiveSet primitives;

    public void Bar()
    {
        // Nessle is not ready on this line
        UIBuilder.primitives = primitives;
        // Nessle is ready on this line!
    }
}
```

You can change the value of `UIBuilder.primitives` at any point. Changing this value **does not** change UI which has already been constructed.

```csharp
public class Foo
{
    public UIPrimitiveSet primitives1;
    public UIPrimitiveSet primitives2;

    public void Bar()
    {
        UIBuilder.primitives = primitives1;
        // UI constructed here will use the values of primitives1
        UIBuilder.primitives = primitives2;
        // UI constructed here will use the values of primitives2
        // UI previously constructed will remain unchanged
    }
}
```

Once `UIBuilder.primitives` is evaluated, you can start generating controls. The easiest way to do this is to statically reference `Nessle.UIBuilder` and `Nessle.Props`. With those static references, you can directly call Nessle's construction methods without qualifying them with a static class:

```csharp
using UnityEngine.UI;

using Nessle;

using static Nessle.UIBuilder;
using static Nessle.Props;

public class Foo
{
    public UIPrimitiveSet primitives;
    public IControl ui;

    public void Bar()
    {
        UIBuilder.primitives = primitives;
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
        content = List(Text(new() { value = Value("Click Me!") })),
        onClick = () => Debug.Log("Hello World!")
    }
)
```

Every control has a `layout` property, used to place and resize the control when it's location and size are not being driven by an external layout component (such at `VerticalLayout`). You'll notice that the properties of `layout` are not limited Unity's position/rotation/scale, but also include things like `fitContentHorizontal`. Nessle automatically detects when self-layout properties like this are set and adds the appropriate components to the control.

## Anatomy Of A Control

Every control has three property objects which are passed into it's construction method: `props`, `element`, and `layout`. Additionally, any control can have it's source prefab overwritten in any specific construction call. Any or all of these arguments can be excluded from a given construction call. If no source prefab is designated, the one in `UIBuilder.primitives` will be used. These requirements taken together mean that the "canonical" constructor method for a control named `Slider` would look like this:

```csharp
public static IControl Slider(
    SliderProps props = default,
    ElementProps element = default,
    LayoutProps layout = default,
    Control<SliderProps> prefab = default
) => UIBuilder.Control(prefab ?? primitives.slider, props, element, layout);
```

In the above scenario, `SliderProps` is a custom `struct` which defines the unique properties the slider needs to operate. `ElementProps` holds a common set of properties used by all controls- things like the control's active state, it's name, and a list of it's bindings. Finally, `LayoutProps` contains the properties the control uses to size and place itself if no other control is driving it's position or size.

An example `SliderProps` might look something like this:

```csharp
public struct SliderProps
{
    public IValueObservable<float> value;
    public ImageProps handleImage;
    public ImageProps backgroundImage;

    public Action<float> onValueChanged;
}
```
