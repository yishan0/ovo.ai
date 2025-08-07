using UnityEngine;
using UnityEngine.EventSystems;
public class DragWindow : MonoBehaviour, IDragHandler
{
    public RectTransform window;
    public void OnDrag(PointerEventData e)
    {
        window.anchoredPosition += e.delta;
    }
}