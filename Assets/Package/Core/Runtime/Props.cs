using System.Collections.Generic;
using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public static class Props
    {
        public static IValueObservable<T> From<T>(T value)
            => new ValueObservable<T>(value);

        public static IListObservable<T> From<T>(IEnumerable<T> values)
            => new ListObservable<T>(values);

        public static IListObservable<T> From<T>(params T[] values)
            => new ListObservable<T>(values);
    }
}
