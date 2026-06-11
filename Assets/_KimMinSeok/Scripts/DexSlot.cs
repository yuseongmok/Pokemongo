using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DexSlot : MonoBehaviour
{
    [SerializeField] private Image pokemonImage;
    [SerializeField] private TextMeshProUGUI idText;
    private PokemonData currentData;

    public void Setup(PokemonData data)
    {
        currentData = data;
        idText.text = data.pokemonID.ToString("D3");
        if (data.isCaught)
        {
            pokemonImage.sprite = data.pokemonSprite;
            pokemonImage.color = Color.white; 
        }
        else if (data.isDiscovered)
        {
            pokemonImage.sprite = data.pokemonSprite;
            pokemonImage.color = Color.black;
        }
        else
        {
            pokemonImage.sprite = null;
            pokemonImage.color = new Color(0, 0, 0, 0); 
        }
    }
}
