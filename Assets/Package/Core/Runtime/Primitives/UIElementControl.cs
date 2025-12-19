using UnityEngine;
using UnityEngine.UI;
using ObserveThing;
using UnityEngine.Events;

namespace Nessle
{
    public struct UIElementProps
    {
        public ElementProps element;
        public IListObservable<IControl> children;
    }

    public class UIElementControl : Control<UIElementProps>
    {
        protected override void SetupInternal()
        {
            AddBinding(
                props.element.Subscribe(this),
                props.children?.SubscribeAsChildren(rectTransform)
            );
        }
    }
}