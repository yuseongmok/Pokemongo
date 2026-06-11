using DG.Tweening;
using UnityEngine;

public class PopUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private RectTransform popupBox;

    private void Awake()
    {
        CloseInstant();
    }

    public void Open()
    {
        canvasGroup.DOKill();
        popupBox.DOKill();

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        popupBox.localScale = Vector3.zero;

        canvasGroup
            .DOFade(1f, 0.25f);

        popupBox
            .DOScale(1f, 0.35f)
            .SetEase(Ease.OutBack);

    }

    public void Close()
    {
        canvasGroup.DOKill();
        popupBox.DOKill();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            popupBox.DOScale(0f, 0.2f)
                    .SetEase(Ease.InBack)
        );

        sequence.Join(
            canvasGroup.DOFade(0f, 0.2f)
        );

        sequence.OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });

    }

    private void CloseInstant()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        popupBox.localScale = Vector3.zero;
    }
}
