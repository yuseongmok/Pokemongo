
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

public class PokemonDataImporter
{
    [MenuItem("Tools/Import Pokemon Data")]
    public static void ImportPokemon()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("PokemonDatabase");

        if (csvFile == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다! Resources 폴더와 파일명을 확인하세요.");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        string folderPath = "Assets/_KimMinSeok/PokemonDataFiles";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] data = lines[i].Split(',');

            int id = int.Parse(data[0].Trim());
            string pName = data[1].Trim();

            PokemonForm form = PokemonForm.Normal;
            System.Enum.TryParse(data[2].Trim(), out form);

            bool isCaught = bool.Parse(data[3].Trim().ToUpper());
            bool isShiny = bool.Parse(data[4].Trim().ToUpper());

            PokemonData newPokemon = ScriptableObject.CreateInstance<PokemonData>();
            newPokemon.pokemonID = id;
            newPokemon.pokemonName = pName;
            newPokemon.pokemonForm = form;
            newPokemon.isCaught = isCaught;
            newPokemon.isShiny = isShiny;

            string spritePath = $"PokemonSprites/{id:D3}";
            newPokemon.pokemonSprite = Resources.Load<Sprite>(spritePath);

            string assetPath = $"{folderPath}/{id:D3}_{pName}.asset";
            AssetDatabase.CreateAsset(newPokemon, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("모든 포켓몬 데이터 에셋 생성이 완료되었습니다");
    }
}
#endif