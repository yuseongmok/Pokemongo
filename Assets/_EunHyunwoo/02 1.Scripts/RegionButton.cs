using UnityEngine;
using UnityEngine.SceneManagement;

public class RegionButton : MonoBehaviour
{
    public void OnClickKanto()
    {
        SceneManager.LoadScene("Next Robby");
    }

    public void OnClickBack()
 {
    SceneManager.LoadScene("Robby");
 }

   public void OnClickPokemon()
 {
    SceneManager.LoadScene("PokemonDetail");
 }

  public void OnClickPokemon0004()
  {
     SceneManager.LoadScene("PokemonDetail_0004");
  }
}
