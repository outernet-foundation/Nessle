using System;
using System.Collections.Generic;
using ObserveThing;

namespace Nessle
{
    public class CreateListObservable<T, U> : IListObservable<U> where U : IControl
    {
        private IListObservable<T> _list;
        private Func<T, U> _create;
        private IDisposable _listStream;
        private List<U> _currentList = new List<U>();
        private Queue<ListEventArgs<U>> _argsPool = new Queue<ListEventArgs<U>>();
        private List<ObserverData> _observers = new List<ObserverData>();

        public CreateListObservable(IListObservable<T> list, Func<T, U> create)
        {
            _list = list;
            _create = create;
        }

        private class ObserverData
        {
            public bool disposed;
            public ObserveThing.IObserver<IListEventArgs<U>> observer;
        }

        public IDisposable Subscribe(ObserveThing.IObserver<IListEventArgs<U>> observer)
        {
            if (_observers.Count == 0)
            {
                _listStream = _list.Subscribe(
                    HandleSourceChanged,
                    HandleSourceError,
                    HandleSourceDisposed
                );
            }

            var observerData = new ObserverData() { observer = observer };
            _observers.Add(observerData);

            var args = AllocateArgs();

            args.source = this;

            for (int i = 0; i < _currentList.Count; i++)
            {
                args.element = _currentList[i];
                args.index = i;
                observer.OnNext(args);
            }

            DeallocateArgs(args);

            return new Disposable(() =>
            {
                observerData.disposed = true;
                _observers.Remove(observerData);

                if (_observers.Count == 0)
                {
                    _listStream?.Dispose();
                    _listStream = null;
                }
            });
        }

        private ListEventArgs<U> AllocateArgs()
            => _argsPool.Count == 0 ? new ListEventArgs<U>() : _argsPool.Dequeue();

        private void DeallocateArgs(ListEventArgs<U> args)
            => _argsPool.Enqueue(args);

        private void SafeNotifyObservers(Action<ObserveThing.IObserver<IListEventArgs<U>>> notify)
        {
            var observers = _observers.ToArray();

            foreach (var observerData in observers)
            {
                if (observerData.disposed)
                    continue;

                notify(observerData.observer);
            }
        }

        private void HandleSourceChanged(IListEventArgs<T> args)
        {
            int index = -1;
            U element = default;
            OpType opType = default;

            if (args.operationType == OpType.Add)
            {
                index = args.index;
                element = _create(args.element);
                opType = OpType.Add;

                _currentList.Insert(args.index, element);
            }
            else if (args.operationType == OpType.Remove)
            {
                index = args.index;
                element = _currentList[args.index];
                opType = OpType.Remove;

                _currentList.RemoveAt(args.index);
            }

            var opArgs = AllocateArgs();

            opArgs.index = index;
            opArgs.element = element;
            opArgs.operationType = opType;

            SafeNotifyObservers(x => x.OnNext(opArgs));

            DeallocateArgs(opArgs);

            if (opType == OpType.Remove)
                element.Dispose();
        }

        private void HandleSourceError(Exception error)
        {
            SafeNotifyObservers(x => x.OnError(error));
        }

        private void HandleSourceDisposed()
        {
            SafeNotifyObservers(x => x.OnDispose());

            foreach (var element in _currentList)
                element.Dispose();

            _currentList.Clear();
        }
    }
}