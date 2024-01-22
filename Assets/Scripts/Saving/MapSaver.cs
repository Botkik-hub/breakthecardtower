using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/// <summary>
/// Class handles loading and saving player on the start and end of the scene
/// </summary>
public class MapSaver : MonoBehaviour
{
    private const string PathAdditionMap = "/map.save";

    [SerializeField] private bool loadMap;
    [SerializeField] private bool saveMap;

    private void Awake()
    {
        if (loadMap) LoadMap();
    }

    private void OnDestroy()
    {
        if (saveMap) SaveMap();
    }

    public void SaveMap()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PathAdditionMap;
        FileStream stream = new FileStream(path, FileMode.Create);

        MapContainer data = new MapContainer(gameObject.GetComponent<MapPlayer>());
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public void LoadMap()
    {
        string path = Application.persistentDataPath + PathAdditionMap;
        if (!File.Exists(path)) throw new FileLoadException("No save file");
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        
        MapContainer data = formatter.Deserialize(stream) as MapContainer;
        stream.Close();

        if (data == null) throw new FileLoadException("File cannot be load");
        
        data.RestorePlayer(gameObject);
    }
}
