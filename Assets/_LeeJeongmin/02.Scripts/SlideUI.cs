using UnityEngine;
using DG.Tweening; 

public class SlideUI : MonoBehaviour
{
    [SerializeField] private RectTransform slidePanel;

    [SerializeField] private CanvasGroup canvasGroup;

    private float hidePositionY; 

    private void Start()
    {
        hidePositionY = -slidePanel.rect.height;

        slidePanel.anchoredPosition = new Vector2(0, hidePositionY);

        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OpenScreen()
    {
        slidePanel.DOKill();

        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        slidePanel.DOAnchorPosY(0f, 0.4f).SetEase(Ease.OutQuart);
    }

    public void CloseScreen()
    {
        slidePanel.DOKill();

        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        slidePanel.DOAnchorPosY(hidePositionY, 0.3f).SetEase(Ease.InQuart);
    }
}