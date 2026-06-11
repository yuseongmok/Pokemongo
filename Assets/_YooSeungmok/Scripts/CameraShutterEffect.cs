using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraShutterEffect : MonoBehaviour
{
    [Header("UI Elements")]
    public Image flashImage;         
    public GameObject uiToHide;      

    [Header("Audio")]
    public AudioSource audioSource;   // 소리를 낼 오디오 소스
    public AudioClip shutterSound;    // 찰칵 소리 파일 (.mp3)

    [Header("Settings")]
    public float fadeDuration = 0.4f; // 플래시가 사라지는 시간

    // 카메라 버튼에 연결할 함수
    public void OnCameraButtonClick()
    {
        StartCoroutine(ShutterSequence());
    }

    private IEnumerator ShutterSequence()
    {
        if (audioSource != null && shutterSound != null)
        {
            audioSource.PlayOneShot(shutterSound);
        }

        
        if (uiToHide != null) uiToHide.SetActive(false);

        
        Color flashColor = flashImage.color;
        flashColor.a = 1f;
        flashImage.color = flashColor;

        
        if (uiToHide != null) uiToHide.SetActive(true);


        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            flashColor.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            flashImage.color = flashColor;
            yield return null;
        }

        flashColor.a = 0f;
        flashImage.color = flashColor;
    }
}