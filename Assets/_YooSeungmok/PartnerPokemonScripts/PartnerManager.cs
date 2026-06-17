using UnityEngine;

public class PartnerManager : MonoBehaviour
{
    public static PartnerManager Instance { get; private set; }

    [Header("Animator")]
    public Animator dratiniAnimator; 

    [Header("Effects")]
    public GameObject treatEffectPrefab; 
    public Transform effectSpawnPoint;   

    [Header("Audio (Sound)")]
    public AudioSource audioSource;  
    public AudioClip crySound;       
    public AudioClip eatSound;           

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

  
    public void PlayWithPartner()
    {
        if (dratiniAnimator != null)
        {
            dratiniAnimator.SetTrigger("trg_Play");
        }

        if (audioSource != null && crySound != null)
        {
            audioSource.PlayOneShot(crySound);
        }

        Debug.Log("미뇽과 놀기: 애니메이션 및 울음소리 재생!");
    }
    public void GiveTreat()
    {
        if (dratiniAnimator != null)
        {
            dratiniAnimator.SetTrigger("trg_Eat"); 
        }

        if (treatEffectPrefab != null && effectSpawnPoint != null)
        {
            GameObject fx = Instantiate(treatEffectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
            Destroy(fx, 2.0f); 
        }

        if (audioSource != null && eatSound != null)
        {
            audioSource.PlayOneShot(eatSound);
        }

        Debug.Log("간식 주기: 애니메이션, 이펙트 및 사운드 재생!");
    }
}