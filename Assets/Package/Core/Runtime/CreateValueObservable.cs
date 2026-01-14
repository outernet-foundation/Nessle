using System;
using System.Collections.Generic;
using ObserveThing;

namespace Nessle
{
    public class CreateValueObservable<T, U> : IValueObservable<U> where U : IControl
    {
        private IValueObservable<T> _value;
        private Func<T, U> _create;
        private IDisposable _valueStream;
        private U _currentValue = default;
        private Queue<ValueEventArgs<U>> _argsPool = new Queue<ValueEventArgs<U>>();
        private List<ObserverData> _observers = new List<ObserverData>();

        public CreateValueObservable(IValueObservable<T> value, Func<T, U> create)
        {
            _value = value;
            _create = create;
        }

        private class ObserverData
        {
            public bool disposed;
            public ObserveThing.IObserver<IValueEventArgs<U>> observer;
        }

        public IDisposable Subscribe(ObserveThing.IObserver<IValueEventArgs<U>> observer)
        {
            if (_observers.Count == 0)
            {
                _valueStream = _value.Subscribe(
                    HandleSourceChanged,
                    HandleSourceError,
                    HandleSourceDisposed
                );
            }

            var observerData = new ObserverData() { observer = observer };
            _observers.Add(observerData);

            var args = AllocateArgs();

            args.source = this;
            args.previousValue = default;
            args.currentValue = _currentValue;

            observer.OnNext(args);

            DeallocateArgs(args);

            return new Disposable(() =>
            {
                observerData.disposed = true;
                _observers.Remove(observerData);

                if (_observers.Count == 0)
                {
                    _valueStream?.Dispose();
                    _valueStream = null;
                }
            });
        }

        private ValueEventArgs<U> AllocateArgs()
            => _argsPool.Count == 0 ? new ValueEventArgs<U>() : _argsPool.Dequeue();

        private void DeallocateArgs(ValueEventArgs<U> args)
            => _argsPool.Enqueue(args);

        private void SafeNotifyObservers(Action<ObserveThing.IObserver<IValueEventArgs<U>>> notify)
        {
            var observers = _observers.ToArray();

            foreach (var observerData in observers)
            {
                if (observerData.disposed)
                    continue;

                notify(observerData.observer);
            }
        }

        private void HandleSourceChanged(IValueEventArgs<T> args)
        {
            var opArgs = AllocateArgs();

            opArgs.previousValue = _currentValue;
            _currentValue = _create(args.currentValue);
            opArgs.currentValue = _currentValue;

            SafeNotifyObservers(x => x.OnNext(opArgs));

            opArgs.previousValue?.Dispose();
            DeallocateArgs(opArgs);
        }

        private void HandleSourceError(Exception error)
        {
            SafeNotifyObservers(x => x.OnError(error));
        }

        private void HandleSourceDisposed()
        {
            SafeNotifyObservers(x => x.OnDispose());

            _currentValue?.Dispose();
            _currentValue = default;
        }
    }
}