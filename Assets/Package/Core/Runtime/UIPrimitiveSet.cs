using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Nessle
{
    [CreateAssetMenu(fileName = "UIPrimitiveSet", menuName = "Nessle/UI Primitive Set")]
    public class UIPrimitiveSet : ScriptableObject
    {
        public TextMeshProUGUI text => _text;
        public TMP_InputField inputField => _inputField;
        public Scrollbar scrollbar => _scrollbar;
        public ScrollRect scrollRect => _scrollRect;
        public TMP_Dropdown dropdown => _dropdown;
        public Button button => _button;
        public HorizontalLayoutGroup horizontalLayout => _horizontalLayout;
        public VerticalLayoutGroup verticalLayout => _verticalLayout;
        public Toggle toggle => _toggle;
        public Slider slider => _slider;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private Button _button;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayout;
        [SerializeField] private VerticalLayoutGroup _verticalLayout;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Slider _slider;
    }
}