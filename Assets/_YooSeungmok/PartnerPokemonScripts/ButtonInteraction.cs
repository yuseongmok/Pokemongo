using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData) => transform.DOScale(1.05f, 0.2f);
    public void OnPointerExit(PointerEventData eventData) => transform.DOScale(1f, 0.2f);
    public void OnPointerDown(PointerEventData eventData) 
    {
        transform.DOScale(0.95f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }
}