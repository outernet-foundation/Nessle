using System.Collections.Generic;
using System.Linq;
using Nessle;
using ObserveThing;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

namespace Nessle
{
    public class Props
    {
        public static ValueObservable<T> From<T>(T staticValue)
        {
            return new ValueObservable<T>(staticValue);
        }

        // public static ValueObservable<TDest> From<TSource, TDest>(ValueObservable<TSource> boundValue, Func<TSource, TDest> to, Func<TDest, TSource> from)
        // {
        //     return default;
        // }

        // public static ValueObservable<T> From<T>(IValueObservable<T> readonlyValue)
        // {
        //     return default;
        // }

        // public static ListObservable<T> From<T>(params T[] staticList)
        // {
        //     return default;
        // }

        public static ListObservable<T> From<T>(IEnumerable<T> staticList)
        {
            return new ListObservable<T>(staticList);
        }

        // public static ListObservable<TDest> From<TSource, TDest>(ListObservable<TSource> boundList, Func<TSource, TDest> to, Func<TDest, TSource> from)
        // {
        //     return default;
        // }
    }
}