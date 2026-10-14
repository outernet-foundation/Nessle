using System.Collections.Generic;
using ObserveThing;

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
            => new ObservableList<IValueObservable<T>>(values).ObservableUnwrap();

        public static IDictionaryObservable<TKey, TValue> Dictionary<TKey, TValue>(params KeyValuePair<TKey, TValue>[] kvps)
            => new ObservableDictionary<TKey, TValue>(kvps);

        public static ISetObservable<T> Set<T>(params T[] values)
            => new ObservableSet<T>(values);
    }
}
