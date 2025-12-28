using ObserveThing;

namespace Nessle
{
    public struct UIElementProps
    {
        public ElementProps element;
        public TransformProps transform;
        public IListObservable<IControl> children;
    }

    public class UIElementControl : Control<UIElementProps>
    {
        protected override void SetupInternal()
        {
            AddBinding(
                props.element.Subscribe(this),
                props.transform.Subscribe(this),
                props.children?.SubscribeAsChildren(rectTransform)
            );
        }
    }
}