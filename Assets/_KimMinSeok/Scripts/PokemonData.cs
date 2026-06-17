using UnityEngine;


public enum PokemonForm
{
    Normal,
    G_MAX,
    MEGA
}

[CreateAssetMenu(fileName = "PokemonData", menuName = "Scriptable Objects/PokemonData")]
public class PokemonData : ScriptableObject
{
    public int pokemonID; 
    public string pokemonName;   
    public Sprite pokemonSprite;  
    public bool isDiscovered;
    public bool isCaught;

    public bool isShiny;
    public bool isLucky; 
    public bool isXXL;
    public bool isXXS;        
    public bool isPerfect;

    public PokemonForm pokemonForm = PokemonForm.Normal;
}
