using UnityEngine;
using ObserveThing;
using System;
using FofX;
using FofX.Stateful;
using Nessle;
using Nessle.StatefulExtensions;

public class Scratch : MonoBehaviour
{
    public class TestState : ObservableObject
    {
        public ObservablePrimitive<string> someValue { get; private set; }
    }

    public Canvas canvas;
    public UIPrimitiveSet primitives;
    public TestState state;

    public string someValue;

    private void Awake()
    {
        UIBuilder.primitives = primitives;

        state = new TestState();
        state.Initialize("root", new ObservableNodeContext(new UnityLogger()));

        state.context.RegisterObserver(
            _ => someValue = state.someValue.value,
            state.someValue
        );

        var control = new Control("canvas", canvas.gameObject);
        control.Children(
            UIBuilder.InputField().Setup(x =>
            {
                x.AnchoredPosition(new Vector2(0.5f, 0.5f));
                x.SizeDelta(new Vector2(300, 28));
                x.BindValue(
                    x => x.inputText.text,
                    state.someValue,
                    x => x,
                    x => x
                );
            })
        );
    }

    private void Update()
    {
        if (someValue != state.someValue.value)
            state.someValue.ExecuteSet(someValue);
    }
}

public class UnityLogger : FofX.ILogger
{
    public LogLevel logLevel;

    public bool LevelEnabled(LogLevel logLevel)
        => this.logLevel <= logLevel;

    public void Generic(LogLevel logLevel, string message, Exception exception)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                Trace(message);
                break;
            case LogLevel.Debug:
                Debug(message);
                break;
            case LogLevel.Info:
                Info(message);
                break;
            case LogLevel.Warn:
                Warning(message);
                break;
            case LogLevel.Error:
            case LogLevel.Fatal:
                Error(message, exception);
                break;
        }
    }

    public void Debug(string message)
        => UnityEngine.Debug.Log($"<color=#00FFFF>[DEBUG]</color> {message}");

    public void Error(string message)
        => UnityEngine.Debug.LogError($"<color=#FF0000>[ERROR]</color> {message}");

    public void Error(Exception exception)
        => UnityEngine.Debug.LogError($"<color=#FF0000>[ERROR]</color> {exception}");

    public void Error(string message, Exception exception)
        => UnityEngine.Debug.LogError($"<color=#FF0000>[ERROR]</color> Message: {message}\nException: {exception}");

    public void Info(string message)
        => UnityEngine.Debug.Log($"<color=#00FF00>[INFO]</color> {message}");

    public void Trace(string message)
        => UnityEngine.Debug.Log($"[TRACE] {message}");

    public void Warning(string message)
        => UnityEngine.Debug.LogWarning($"<color=#FFFF00>[WARN]</color> Message: {message}");
}