using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DexManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform dexGridParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("Data")]
    [SerializeField] private List<PokemonData> pokemonDatabase = new List<PokemonData>();

    void Start()
    {
        GeneratePokedex();
    }

    public void GeneratePokedex()
    {
        foreach (Transform child in dexGridParent)
        {
            Destroy(child.gameObject);
        }
        int caughtCount = 0;
        int totalCount = pokemonDatabase.Count;

        foreach (PokemonData data in pokemonDatabase)
        {
            GameObject newSlot = Instantiate(slotPrefab, dexGridParent);
            DexSlot slotScript = newSlot.GetComponent<DexSlot>();

            if (slotScript != null)
            {
                slotScript.Setup(data);
            }

            if (data.isCaught)
            {
                caughtCount++;
            }
        }

        if (countText != null)
        {
            countText.text = $"{caughtCount} / {totalCount}";
        }
    }
}
