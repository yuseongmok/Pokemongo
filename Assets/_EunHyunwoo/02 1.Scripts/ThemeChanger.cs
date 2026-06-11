using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ThemeChanger : MonoBehaviour
{
    [Header("전체 배경 패널")]
    public Image mainBackground;

    [Header("카드 버튼들 (관동,성도,호연 등)")]
    public Image[] backgroundImages;

    [Header("포켓몬 버튼")]
    public Image pokemonButtonImage;

    [Header("G-MAX 버튼")]
    public Image gmaxButtonImage;

    [Header("도감 텍스트")]
    public TextMeshProUGUI titleText;

    [Header("잡은 수 텍스트")]
    public TextMeshProUGUI countText;

    [Header("잠긴 버튼들")]
    public GameObject[] lockedButtons;

    // 원래 색
    Color originalColor     = new Color(1f, 1f, 1f, 1f);
    Color originalTextColor = new Color(1f, 0.85f, 0.3f);

    // G-MAX 테마 색깔
    Color bgColor_Gmax   = new Color(0.15f, 0.08f, 0.30f);
    Color cardColor_Gmax = new Color(0.50f, 0.20f, 0.75f);
    Color btnColor_Gmax  = new Color(0.65f, 0.30f, 0.90f);
    Color gmaxTextColor  = new Color(0.95f, 0.85f, 1.00f);

    public void OnClickPokemon()
    {
        mainBackground.DOColor(originalColor, 0.4f);
        foreach (Image img in backgroundImages)
            img.DOColor(originalColor, 0.4f);
        pokemonButtonImage.DOColor(originalColor, 0.4f);
        gmaxButtonImage.DOColor(originalColor, 0.4f);
        titleText.DOColor(originalTextColor, 0.4f);
        countText.DOColor(originalTextColor, 0.4f);
    }

    public void OnClickGmax()
    {
        mainBackground.DOColor(bgColor_Gmax, 0.4f);
        foreach (Image img in backgroundImages)
            img.DOColor(cardColor_Gmax, 0.4f);
        gmaxButtonImage.DOColor(btnColor_Gmax, 0.4f);
        pokemonButtonImage.DOColor(bgColor_Gmax, 0.4f);
        titleText.DOColor(gmaxTextColor, 0.4f);
        countText.DOColor(gmaxTextColor, 0.4f);
    }

    public void OnClickLocked0() { ShakeButton(lockedButtons[0]); }
    public void OnClickLocked1() { ShakeButton(lockedButtons[1]); }
    public void OnClickLocked2() { ShakeButton(lockedButtons[2]); }
    public void OnClickLocked3() { ShakeButton(lockedButtons[3]); }
    public void OnClickLocked4() { ShakeButton(lockedButtons[4]); }
    public void OnClickLocked5() { ShakeButton(lockedButtons[5]); }
    public void OnClickLocked6() { ShakeButton(lockedButtons[6]); }

    void ShakeButton(GameObject button)
    {
        button.transform.DOKill();
        button.transform
            .DOShakePosition(0.8f, new Vector3(20f, 0f, 0f), 10, 90);
    }
}