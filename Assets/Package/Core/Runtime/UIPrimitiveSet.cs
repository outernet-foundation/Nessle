using UnityEngine;

namespace Nessle
{
    [CreateAssetMenu(fileName = "UIPrimitiveSet", menuName = "Nessle/UI Primitive Set")]
    public class UIPrimitiveSet : ScriptableObject
    {
        public PrimitiveControl<TextProps> text => _text;
        public PrimitiveControl<ImageProps> image => _image;
        public PrimitiveControl<InputFieldProps> inputField => _inputField;
        public PrimitiveControl<ScrollbarProps> scrollbar => _scrollbar;
        public PrimitiveControl<ScrollRectProps> scrollRect => _scrollRect;
        public PrimitiveControl<DropdownProps> dropdown => _dropdown;
        public PrimitiveControl<ButtonProps> button => _button;
        public PrimitiveControl<LayoutProps> horizontalLayout => _horizontalLayout;
        public PrimitiveControl<LayoutProps> verticalLayout => _verticalLayout;
        public PrimitiveControl<ToggleProps> toggle => _toggle;
        public PrimitiveControl<SliderProps> slider => _slider;

        [SerializeField] private PrimitiveControl<TextProps> _text;
        [SerializeField] private PrimitiveControl<ImageProps> _image;
        [SerializeField] private PrimitiveControl<InputFieldProps> _inputField;
        [SerializeField] private PrimitiveControl<ScrollbarProps> _scrollbar;
        [SerializeField] private PrimitiveControl<ScrollRectProps> _scrollRect;
        [SerializeField] private PrimitiveControl<DropdownProps> _dropdown;
        [SerializeField] private PrimitiveControl<ButtonProps> _button;
        [SerializeField] private PrimitiveControl<LayoutProps> _horizontalLayout;
        [SerializeField] private PrimitiveControl<LayoutProps> _verticalLayout;
        [SerializeField] private PrimitiveControl<ToggleProps> _toggle;
        [SerializeField] private PrimitiveControl<SliderProps> _slider;
    }
}