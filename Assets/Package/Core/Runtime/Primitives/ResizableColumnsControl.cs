using System;
using UnityEngine;

namespace Nessle
{
    public class DraggableHandleControl : MonoBehaviour
    {
        public Action onDragged;
    }

    public class ResizableColumnControl : MonoBehaviour
    {
        public DraggableHandleControl separatorPrefab;

    }
}