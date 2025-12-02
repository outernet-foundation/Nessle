using ObserveThing;
using UnityEngine;

namespace Nessle
{
    public static class ImageExtensions
    {
        public static T Sprite<T>(this T control, Sprite sprite)
            where T : IControl<UIBuilder.ImageProps>
        {
            control.props.sprite.From(sprite);
            return control;
        }

        public static T Sprite<T>(this T control, IValueObservable<Sprite> sprite)
            where T : IControl<UIBuilder.ImageProps>
        {
            control.props.sprite.From(sprite);
            return control;
        }
    }
}
