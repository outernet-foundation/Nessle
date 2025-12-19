using UnityEngine;

namespace Nessle
{
    [CreateAssetMenu(fileName = "UIPrimitiveSet", menuName = "Nessle/UI Primitive Set")]
    public class UIPrimitiveSet : ScriptableObject
    {
        public Control<TextProps> text => _text;
        public Control<ImageProps> image => _image;
        public Control<InputFieldProps> inputField => _inputField;
        public Control<ScrollbarProps> scrollbar => _scrollbar;
        public Control<ScrollRectProps> scrollRect => _scrollRect;
        public Control<DropdownProps> dropdown => _dropdown;
        public Control<ButtonProps> button => _button;
        public Control<LayoutProps> horizontalLayout => _horizontalLayout;
        public Control<LayoutProps> verticalLayout => _verticalLayout;
        public Control<ToggleProps> toggle => _toggle;
        public Control<SliderProps> slider => _slider;

        [SerializeField] private Control<TextProps> _text;
        [SerializeField] private Control<ImageProps> _image;
        [SerializeField] private Control<InputFieldProps> _inputField;
        [SerializeField] private Control<ScrollbarProps> _scrollbar;
        [SerializeField] private Control<ScrollRectProps> _scrollRect;
        [SerializeField] private Control<DropdownProps> _dropdown;
        [SerializeField] private Control<ButtonProps> _button;
        [SerializeField] private Control<LayoutProps> _horizontalLayout;
        [SerializeField] private Control<LayoutProps> _verticalLayout;
        [SerializeField] private Control<ToggleProps> _toggle;
        [SerializeField] private Control<SliderProps> _slider;
    }
}