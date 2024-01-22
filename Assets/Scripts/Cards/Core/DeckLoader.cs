using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Loads all decks (ScriptableObjects) in the game
/// Made a singleton to prevent double loading decks
/// </summary>
public class DeckLoader : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////////////
    /// Singleton part
    private static DeckLoader _instance;

    public static DeckLoader Instance()
    {
        return _instance;
    }

    private void MakeSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////

    private readonly List<DeckTemplate> _decks = new List<DeckTemplate>();

    private void Awake()
    {
        MakeSingleton();
        LoadDeckPrefabs();
    }
    private void LoadDeckPrefabs()
    {
        DeckTemplate[] gos = Resources.LoadAll<DeckTemplate>("Prefabs/Decks/");
        foreach (DeckTemplate go in gos)
        {
            _decks.Add(go);
        }
    }

    public DeckTemplate GetDeck(string deckName)
    {
        return _decks.Find(e => e.name == deckName);
    }
}
