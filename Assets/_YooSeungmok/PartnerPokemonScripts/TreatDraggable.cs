using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; 

public class TreatDraggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private Camera mainCamera;

    void Start()
    {
        startPos = transform.position;
        mainCamera = Camera.main;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        transform.SetAsLastSibling();

        Vector2 mousePos = Vector2.zero;

        if (Mouse.current != null)
        {
            mousePos = Mouse.current.position.ReadValue();
        }
        else if (Touchscreen.current != null)
        {
            mousePos = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)transform.parent, 
            mousePos, 
            eventData.pressEventCamera, // UI를 렌더링하는 카메라를 자동으로 가져옵니다.
            out Vector3 worldPos))
        {
            // Z축이 틀어져서 카메라 뒤로 넘어가는 현상을 방지하기 위해 기존 UI의 Z축을 유지하거나 0으로 고정합니다.
            worldPos.z = startPos.z; 
            transform.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Vector2.zero;
        if (Mouse.current != null) mousePos = Mouse.current.position.ReadValue();
        else if (Touchscreen.current != null) mousePos = Touchscreen.current.primaryTouch.position.ReadValue();
        
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Partner")) 
                {
                    PartnerManager.Instance.GiveTreat();
                }
            }
        }

        // 원래 자리로 복귀
        transform.position = startPos; 
    }
}