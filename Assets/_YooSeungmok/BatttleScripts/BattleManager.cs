using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [System.Serializable]
    public class PokemonController
    {
        public string pokemonName;
        public int maxHp = 1279;
        public int currentHp;
        
        [Header("Components")]
        public Animator animator;
        public Slider hpBarSlider;

        [Header("VFX Prefabs")]
        public GameObject normalAttackFxPrefab;
        public GameObject skillAttackFxPrefab;
        public GameObject guardFxPrefab; 

        public Transform effectSpawnPoint; 

        [Header("SFX Audio Clips (사운드 파일)")]
        public AudioClip normalAttackSound;
        public AudioClip skillAttackSound;
        public AudioClip guardSound;

        [HideInInspector]
        public bool isGuarding = false;
        
        private float targetHp;
        public float hpFillSpeed = 5f;
        private GameObject currentGuardFx;

        // 사운드 재생기 (오디오 소스)
        private AudioSource audioSource;

        public void Init(AudioSource source)
        {
            currentHp = maxHp;
            targetHp = maxHp;
            audioSource = source; // 매니저로부터 오디오 소스를 전달받음

            if (hpBarSlider != null)
            {
                hpBarSlider.maxValue = maxHp;
                hpBarSlider.value = currentHp;
            }
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip != null && audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        public GameObject SpawnEffect(GameObject effectPrefab, float destroyTime = 2f)
        {
            if (effectPrefab == null) return null;
            GameObject effect = Instantiate(effectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
            Destroy(effect, destroyTime);
            return effect;
        }

        public void TakeDamage(int damage)
        {
            if (isGuarding)
            {
                damage = Mathf.RoundToInt(damage * 0.2f);
                Debug.Log($"{pokemonName}이(가) 방어하여 데미지가 감소했습니다!");
            }

            currentHp -= damage;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
            targetHp = currentHp;

            if (currentHp <= 0)
            {
                animator.SetTrigger("OnDie");
                if (currentGuardFx != null) Destroy(currentGuardFx);
                Debug.Log($"{pokemonName}이(가) 쓰러졌습니다.");
            }
        }

        public void DestroyGuardEffect()
        {
            if (currentGuardFx != null) Destroy(currentGuardFx);
        }

        public void StartGuarding()
        {
            if (isGuarding || guardFxPrefab == null) return;

            isGuarding = true;
            animator.SetBool("IsGuarding", true);
            currentGuardFx = SpawnEffect(guardFxPrefab, 0f);
            
            // 가드 성공 사운드 재생
            PlaySound(guardSound);
        }

        public void UpdateHpBar()
        {
            if (hpBarSlider == null) return;
            hpBarSlider.value = Mathf.MoveTowards(hpBarSlider.value, targetHp, Time.deltaTime * maxHp * hpFillSpeed * 0.2f);
        }
    }

    [Header("Pokemon Setup")]
    public PokemonController player;
    public PokemonController enemy;

    [Header("Skill Settings")]
    public int normalAttackDamage = 150;
    public int skillAttackDamage = 350;

    [Header("Camera Shake Settings")]
    public Transform mainCameraTransform; // 흔들 메인 카메라 등록 칸

    // 메인 오디오 소스 컴포넌트
    private AudioSource globalAudioSource;

    void Start()
    {
        // 내 오브젝트에 붙어있는 AudioSource를 가져옵니다.
        globalAudioSource = GetComponent<AudioSource>();
        if (globalAudioSource == null)
        {
            globalAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // 포켓몬 제어기에 오디오 소스를 나눠줍니다.
        player.Init(globalAudioSource);
        enemy.Init(globalAudioSource);

        StartCoroutine(EnemyAILoop());
    }

    void Update()
    {
        player.UpdateHpBar();
        enemy.UpdateHpBar();
    }

    public void TriggerCameraShake(float duration, float magnitude)
    {
        if (mainCameraTransform != null)
        {
            StartCoroutine(CameraShakeRoutine(duration, magnitude));
        }
    }

    // 카메라 진동을 실시간으로 계산하는 코루틴
    private IEnumerator CameraShakeRoutine(float duration, float magnitude)
    {
       // [수정 핵심] 진동이 '시작하는 그 순간'의 정확한 카메라 위치를 기억합니다.
       Vector3 originalPosition = mainCameraTransform.localPosition;
        float elapsed = 0.0f;

    while (elapsed < duration)
    {
        // 원래 위치(originalPosition)를 기준으로 사방으로 흔들어줍니다.
        float x = originalPosition.x + (Random.Range(-1f, 1f) * magnitude);
        float y = originalPosition.y + (Random.Range(-1f, 1f) * magnitude);

        mainCameraTransform.localPosition = new Vector3(x, y, originalPosition.z);

        elapsed += Time.deltaTime;
        yield return null; 
    }

    // 진동이 끝나면 정확하게 원래 있던 원래 위치로 완벽하게 돌려놓습니다.
       mainCameraTransform.localPosition = originalPosition;
    }
    public void OnPlayerAttackButton()
    {
        if (player.currentHp <= 0 || enemy.currentHp <= 0) return;
        if (player.isGuarding) EndPlayerGuard();

        player.animator.SetTrigger("OnAttack");
        
        // 사운드 재생
        player.PlaySound(player.normalAttackSound);

        enemy.SpawnEffect(player.normalAttackFxPrefab, 1.5f);
        enemy.TakeDamage(normalAttackDamage);

        // 플레이어가 공격할 때는 진동을 약하게 줍니다. (타격감용)
        TriggerCameraShake(0.15f, 0.05f);
    }

    public void OnPlayerSkillButton()
    {
        if (player.currentHp <= 0 || enemy.currentHp <= 0) return;
        if (player.isGuarding) EndPlayerGuard();

        player.animator.SetTrigger("OnSkill");
        
        // 사운드 재생
        player.PlaySound(player.skillAttackSound);

        enemy.SpawnEffect(player.skillAttackFxPrefab, 2.5f);
        enemy.TakeDamage(skillAttackDamage);

        // 플레이어가 강력한 스킬을 쓸 때는 진동을 크고 길게 줍니다!
        TriggerCameraShake(0.4f, 0.2f);
    }

    public void OnPlayerGuardButton()
    {
        if (player.currentHp <= 0 || enemy.currentHp <= 0) return;
        player.StartGuarding();
    }

    private void EndPlayerGuard()
    {
        player.isGuarding = false;
        player.animator.SetBool("IsGuarding", false);
        player.DestroyGuardEffect();
    }

    IEnumerator EnemyAILoop()
    {
        while (player.currentHp > 0 && enemy.currentHp > 0)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));

            if (player.currentHp <= 0 || enemy.currentHp <= 0) break;

            float randomValue = Random.value;
            if (randomValue > 0.3f)
            {
                enemy.animator.SetTrigger("OnAttack");
                enemy.PlaySound(enemy.normalAttackSound);

                player.SpawnEffect(enemy.normalAttackFxPrefab, 1.5f);
                player.TakeDamage(normalAttackDamage);

                // 플레이어가 일반 공격에 맞았을 때 진동!
                TriggerCameraShake(0.2f, 0.1f);
            }
            else
            {
                enemy.animator.SetTrigger("OnSkill");
                enemy.PlaySound(enemy.skillAttackSound);

                player.SpawnEffect(enemy.skillAttackFxPrefab, 2.5f);
                player.TakeDamage(skillAttackDamage);

                TriggerCameraShake(0.5f, 0.3f);
            }
        }
    }
}