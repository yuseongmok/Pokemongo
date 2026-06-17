using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class RegionButton : MonoBehaviour
{
    public GameObject RobbyPanel;
    public GameObject DexPanel;
    public GameObject Panel0001;
    public GameObject Panel0004;
    public GameObject CreditPopup;

    public void OnClickKanto()
    {
        RobbyPanel.SetActive(false);
        DexPanel.SetActive(true);
    }

    public void OnClickBack()
    {
        DexPanel.SetActive(false);
        RobbyPanel.SetActive(true);
    }

    public void OnClickPokemon0001()
    {
        DexPanel.SetActive(false);
        Panel0001.SetActive(true);
    }

    public void OnClickPokemon0004()
    {
        DexPanel.SetActive(false);
        Panel0004.SetActive(true);
    }

    public void OnClickBackFrom0001()
    {
        Panel0001.SetActive(false);
        DexPanel.SetActive(true);
    }

    public void OnClickBackFrom0004()
    {
        Panel0004.SetActive(false);
        DexPanel.SetActive(true);
    }

    public void OnClickCredit()
    {
        CreditPopup.SetActive(true);
        CanvasGroup cg = CreditPopup.GetComponent<CanvasGroup>();
        if (cg == null) cg = CreditPopup.AddComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.DOFade(1, 0.5f);
    }

    public void OnClickCreditClose()
    {
        CanvasGroup cg = CreditPopup.GetComponent<CanvasGroup>();
        cg.DOFade(0, 0.5f).OnComplete(() => CreditPopup.SetActive(false));
    }

    public void OnClickBackToMain()
 {
    SceneManager.LoadScene("main");
 }
}