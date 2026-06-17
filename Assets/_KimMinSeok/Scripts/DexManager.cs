using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DexManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform dexGridParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TMP_InputField searchInputField;

    [Header("Filter Buttons")]
    [SerializeField] private List<Image> filterButtonImages = new List<Image>();

    [Header("Top Counters")]
    [SerializeField] private TextMeshProUGUI shinyText;
    [SerializeField] private TextMeshProUGUI luckyText;
    [SerializeField] private TextMeshProUGUI xxlText;
    [SerializeField] private TextMeshProUGUI xxsText;
    [SerializeField] private TextMeshProUGUI perfectText;

    [Header("Data")]
    [SerializeField] private List<PokemonData> pokemonDatabase = new List<PokemonData>();

    private PokemonForm currentFilter = PokemonForm.Normal;
    private string currentSearchQuery = "";

    void Start()
    {
        if (searchInputField != null)
        {
            searchInputField.onValueChanged.AddListener(OnSearchValueChanged);
        }
        ChangeFilter((int)PokemonForm.Normal);
    }

    public void OnSearchValueChanged(string value)
    {
        currentSearchQuery = value.Trim();
        GeneratePokedex();
    }

    public void ChangeFilter(int formIndex)
    {
        currentFilter = (PokemonForm)formIndex;

        UpdateFilterButtonVisuals();

        GeneratePokedex();
    }

    private void UpdateFilterButtonVisuals()
    {
        for (int i = 0; i < filterButtonImages.Count; i++)
        {
            if (filterButtonImages[i] == null) continue;

            if (i == (int)currentFilter)
            {
                Color c = filterButtonImages[i].color;
                c.a = 1.0f;
                filterButtonImages[i].color = c;
            }
            else
            {
                Color c = filterButtonImages[i].color;
                c.a = 0.4f;
                filterButtonImages[i].color = c;
            }
        }
    }

    public void GeneratePokedex()
    {
        foreach (Transform child in dexGridParent)
        {
            Destroy(child.gameObject);
        }

        int caughtCount = 0;
        int shinyCount = 0;
        int luckyCount = 0;
        int xxlCount = 0;
        int xxsCount = 0;
        int perfectCount = 0;
        int totalCount = 0;

        foreach (PokemonData data in pokemonDatabase)
        {
            if (data.pokemonForm == currentFilter)
            {
                bool isMatchName = data.pokemonName.Contains(currentSearchQuery);
                bool isMatchID = data.pokemonID.ToString().Contains(currentSearchQuery);

                if (!string.IsNullOrEmpty(currentSearchQuery) && !isMatchName && !isMatchID)
                {
                    continue;
                }

                totalCount++;
                if (data.isCaught) caughtCount++;
                if (data.isShiny) shinyCount++;
                if (data.isLucky) luckyCount++;
                if (data.isXXL) xxlCount++;
                if (data.isXXS) xxsCount++;
                if (data.isPerfect) perfectCount++;

                GameObject newSlot = Instantiate(slotPrefab, dexGridParent);
                DexSlot slotScript = newSlot.GetComponent<DexSlot>();

                if (slotScript != null)
                {
                    slotScript.Setup(data);
                }
            }
        }

        if (countText != null) countText.text = $"{caughtCount} / {totalCount}";
        if (shinyText != null) shinyText.text = $"{shinyCount}\n색이다른";
        if (luckyText != null) luckyText.text = $"{luckyCount}\n반짝반짝";
        if (xxlText != null) xxlText.text = $"{xxlCount}\nXXL";
        if (xxsText != null) xxsText.text = $"{xxsCount}\nxxs";
        if (perfectText != null) perfectText.text = $"{perfectCount}\n100%";
    }
}
