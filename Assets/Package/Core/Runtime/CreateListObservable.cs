using System;
using System.Collections.Generic;
using ObserveThing;

namespace Nessle
{
    public class CreateListObservable<T> : IListObservable<T> where T : IControl
    {
        private IListObservable<T> _list;

        public CreateListObservable(IListObservable<T> list)
        {
            _list = list;
        }

        public IDisposable Subscribe(ObserveThing.IObserver<IListEventArgs<T>> observer)
            => new Instance(this, _list, observer);

        private class Instance : IDisposable
        {
            private ListEventArgs<T> _args = new ListEventArgs<T>();
            private ObserveThing.IObserver<IListEventArgs<T>> _observer;
            private IDisposable _listStream;
            private bool _disposed;

            private List<T> _currentList = new List<T>();

            public Instance(IObservable source, IListObservable<T> list, ObserveThing.IObserver<IListEventArgs<T>> observer)
            {
                _args.source = source;
                _observer = observer;
                _listStream = list.Subscribe(
                    HandleSourceChanged,
                    HandleSourceError,
                    HandleSourceDisposed
                );
            }

            private void HandleSourceChanged(IListEventArgs<T> args)
            {
                if (args.operationType == OpType.Add)
                {
                    _currentList.Add(args.element);
                }
                else if (args.operationType == OpType.Remove)
                {
                    _currentList.Remove(args.element);
                    args.element.Dispose();
                }
            }

            private void HandleSourceError(Exception error)
            {
                _observer.OnError(error);
            }

            private void HandleSourceDisposed()
            {
                Dispose();
            }

            public void Dispose()
            {
                if (_disposed)
                    return;

                _disposed = true;

                _listStream.Dispose();
                _observer.OnDispose();
            }
        }
    }
}