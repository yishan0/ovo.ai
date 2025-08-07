using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Transform originalParent;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        transform.SetParent(canvas.transform); // Bring to front
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        SnapToGrid();
    }

    void SnapToGrid()
    {
        GridLayoutGroup grid = originalParent.GetComponent<GridLayoutGroup>();
        Vector2 gridOrigin = ((RectTransform)originalParent).anchoredPosition;
        Vector2 localPos = rectTransform.anchoredPosition - gridOrigin;

        float cellWidth = grid.cellSize.x + grid.spacing.x;
        float cellHeight = grid.cellSize.y + grid.spacing.y;

        int col = Mathf.RoundToInt(localPos.x / cellWidth);
        int row = Mathf.RoundToInt(-localPos.y / cellHeight);

        Vector2 snappedPosition = new Vector2(col * cellWidth, -row * cellHeight);
        rectTransform.anchoredPosition = gridOrigin + snappedPosition;
    }
}