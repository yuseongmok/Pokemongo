using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; 

public class PokemonEnhanceManager : MonoBehaviour
{
    [Header("Pokemon Data")]
    public int currentCP = 13;
    public int stardust = 24000;
    public int candy = 3;
    
    // 강화 시 필요한 비용
    private int costStardust = 200;
    private int costCandy = 1;

    [Header("UI References")]
    public TextMeshProUGUI cpText;
    public TextMeshProUGUI stardustText;
    public TextMeshProUGUI candyText;
    public Button enhanceButton;

    [Header("Effects")]
    public GameObject enhanceFxPrefab; // 강화 성공 시 터질 이펙트 프리팹
    public Transform fxSpawnPoint;     // 이펙트가 생성될 위치

    [Header("Camera Shake")]
    public CameraShake cameraShake; // 카메라 진동 스크립트 참조
    public float shakeDuration = 0.3f; // 진동 시간
    public float shakeMagnitude = 0.1f; 

    [Header("Audio")]
    public AudioSource audioSource;       // 소리를 재생할 오디오 소스
    public AudioClip enhanceSuccessSound;

    void Start()
    {
        UpdateUI(false); // 시작 시에는 애니메이션 없이 갱신
    }

    // 강화 버튼에 연결할 함수
    public void OnEnhanceButtonClick()
    {
        // 조건 체크 (재화가 충분한지)
        if (stardust >= costStardust && candy >= costCandy)
        {
            // 버튼 일시 비활성화 (연타 방지 및 연출 집중)
            enhanceButton.interactable = false;

            // 재화 차감
            stardust -= costStardust;
            candy -= costCandy;

            // 3. CP 상승 (예시로 15~25 사이 랜덤 상승)
            int cpGain = Random.Range(15, 26);
            int targetCP = currentCP + cpGain;

            if (cameraShake != null)
            {
                cameraShake.Shake(shakeDuration, shakeMagnitude);
            }

            if (audioSource != null && enhanceSuccessSound != null)
            {
                audioSource.PlayOneShot(enhanceSuccessSound);
            }

            // 4. 이펙트 및 연출 시작
            PlayEnhanceEffects(targetCP);
        }
    }

    private void PlayEnhanceEffects(int targetCP)
    {
        // [이펙트 생성] 탕구리 주변에 파티클 스폰
        if (enhanceFxPrefab != null && fxSpawnPoint != null)
        {
            GameObject fx = Instantiate(enhanceFxPrefab, fxSpawnPoint.position, Quaternion.identity);
            Destroy(fx, 3.0f); // 3초 뒤 이펙트 자동 삭제
        }

        // [숫자 애니메이션] 코루틴을 통해 CP와 재화를 부드럽게 갱신
        StartCoroutine(AnimateEnhanceSequence(targetCP));
    }

    private IEnumerator AnimateEnhanceSequence(int targetCP)
    {
        // 재화 텍스트는 즉시 또는 살짝의 딜레이 후 갱신
        stardustText.text = stardust.ToString("N0");
        candyText.text = candy.ToString();

        // CP가 호선(텍스트)을 따라 부드럽게 올라가는 연출 (0.5초 동안 진행)
        float duration = 0.5f;
        float elapsed = 0f;
        int startCP = currentCP;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            // 선형 보간(Lerp)으로 숫자를 부드럽게 올림
            int intermediateCP = (int)Mathf.Lerp(startCP, targetCP, t);
            cpText.text = $"CP {intermediateCP}";
            
            yield return null;
        }

        // 최종 값 고정
        currentCP = targetCP;
        cpText.text = $"CP {currentCP}";

        // 강화 후 UI 상태 최종 업데이트 (버튼 재활성화 여부 등)
        UpdateUI(true);
    }

    private void UpdateUI(bool isAfterEnhance)
    {
        cpText.text = $"CP {currentCP}";
        stardustText.text = stardust.ToString("N0");
        candyText.text = candy.ToString();

        // 남은 재화로 재강화가 가능한지 체크하여 버튼 활성화 여부 결정
        if (stardust >= costStardust && candy >= costCandy)
        {
            enhanceButton.interactable = true;
        }
        else
        {
            enhanceButton.interactable = false;
        }
    }
}