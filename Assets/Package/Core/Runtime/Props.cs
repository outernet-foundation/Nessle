using System.Collections.Generic;
using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public static class Props
    {
        public static IValueObservable<T> Value<T>(T value)
            => new ValueObservable<T>(value);

        public static IListObservable<T> List<T>(IEnumerable<T> values)
            => new ListObservable<T>(values);

        public static IListObservable<T> List<T>(params T[] values)
            => new ListObservable<T>(values);

        public static IListObservable<T> List<T>(params IValueObservable<T>[] values)
            => new ListObservable<IValueObservable<T>>(values).ShallowCopyDynamic();
    }
}
