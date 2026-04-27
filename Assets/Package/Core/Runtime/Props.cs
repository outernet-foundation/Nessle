using System.Collections.Generic;
using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public static class Props
    {
        public static IValueObservable<T> Value<T>(T value)
            => new ObservableValue<T>(value);

        public static IListObservable<T> List<T>(IEnumerable<T> values)
            => new ObservableList<T>(values);

        public static IListObservable<T> List<T>(params T[] values)
            => new ObservableList<T>(values);

        public static IListObservable<T> List<T>(params IValueObservable<T>[] values)
            => new ObservableList<IValueObservable<T>>(values).ObservableShallowCopy();
    }
}
