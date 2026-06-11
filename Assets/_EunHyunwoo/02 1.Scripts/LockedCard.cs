using UnityEngine;
using DG.Tweening;

public class LockedCard : MonoBehaviour
{
    public void OnClickLocked()
    {
        transform.DOShakePosition(0.5f, 10f, 20);
    }
}