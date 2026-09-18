using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nessle
{
    [RequireComponent(typeof(RectTransform))]
    [ExecuteAlways]
    public class MenuRow : LayoutGroup
    {
        [SerializeField]
        private float[] _columnWidths;

        public int columnCount => _columnWidths.Length;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            var minWidth = 0f;
            var preferredWidth = 0f;

            foreach (var layoutElement in rectChildren)
            {
                minWidth += LayoutUtility.GetMinWidth(layoutElement);
                preferredWidth += LayoutUtility.GetPreferredWidth(layoutElement);
            }

            preferredWidth = Mathf.Max(minHeight, preferredWidth);
            SetLayoutInputForAxis(minWidth, preferredWidth, 0, 0);
        }

        public override void CalculateLayoutInputVertical()
        {
            var minHeight = 0f;
            var preferredHeight = 0f;
            var flexibleHeight = 0f;

            foreach (var layoutElement in rectChildren)
            {
                minHeight = Mathf.Max(minHeight, LayoutUtility.GetMinHeight(layoutElement));
                preferredHeight = Mathf.Max(preferredHeight, LayoutUtility.GetPreferredHeight(layoutElement));
                flexibleHeight += LayoutUtility.GetFlexibleHeight(layoutElement);
            }

            preferredHeight = Mathf.Max(minHeight, preferredHeight);
            SetLayoutInputForAxis(minHeight, preferredHeight, flexibleHeight, 1);
        }

        public float GetColumnWidth(int index)
            => _columnWidths[index];

        public float SetColumnWidth(int index, float width)
            => _columnWidths[index] = width;

        public override void SetLayoutHorizontal()
        {
            float totalWidth = _columnWidths.Sum();
            float rectWidth = rectTransform.rect.width;
            float expendedWidth = 0;

            int index = 0;

            foreach (var layoutElement in rectChildren)
            {
                layoutElement.anchorMin = new Vector2(expendedWidth, layoutElement.anchorMin.y);

                float normalizedWidth = (index < _columnWidths.Length ? _columnWidths[index] : 0) / totalWidth;
                float elementWidth = normalizedWidth * rectWidth;

                SetChildAlongAxis(layoutElement, 0, expendedWidth, elementWidth);

                expendedWidth += elementWidth;

                index++;
            }
        }

        public override void SetLayoutVertical()
        {
            float height = rectTransform.rect.height;

            foreach (var layoutElement in rectChildren)
            {
                SetChildAlongAxis(layoutElement, 1, 0, height);
            }
        }
    }
}