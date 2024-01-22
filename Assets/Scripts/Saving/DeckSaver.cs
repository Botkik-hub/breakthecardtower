using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DeckSaver : MonoBehaviour
{
    private const string PathAdditionDeck = "/deck.save";

    [SerializeField] private bool loadDeck;
    [SerializeField] private bool saveDeck;

    private void Start()
    {
        if (loadDeck) LoadDeck();
    }

    private void OnDestroy()
    {
        if (saveDeck) SaveDeck();
    }

    public void SaveDeck()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PathAdditionDeck;
        FileStream stream = new FileStream(path, FileMode.Create);

        DeckContainer data = new DeckContainer(gameObject.GetComponent<PlayerCombatData>());
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public void SaveDeckAsString(string[] cardNames)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PathAdditionDeck;
        FileStream stream = new FileStream(path, FileMode.Create);

        DeckContainer data = new DeckContainer(cardNames);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public void LoadDeck()
    {
        string path = Application.persistentDataPath + PathAdditionDeck;
        if (!File.Exists(path)) throw new FileLoadException("No save file");
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        
        DeckContainer data = formatter.Deserialize(stream) as DeckContainer;
        stream.Close();

        if (data == null) throw new FileLoadException("File cannot be load");
        
        data.RestoreDeck(gameObject.GetComponent<PlayerCombatData>());
    }

    public string[] LoadDeckAsString()
    {
        string path = Application.persistentDataPath + PathAdditionDeck;
        if (!File.Exists(path)) throw new FileLoadException("No save file");
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        
        DeckContainer data = formatter.Deserialize(stream) as DeckContainer;
        stream.Close();

        if (data == null) throw new FileLoadException("File cannot be load");
        
        return data.GetCardsNames();
    }
}