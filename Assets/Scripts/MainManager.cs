using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    private MainManager() { }

    public int currentLevelCompleted;
    public static bool isAudio = true;

    void Start()
    {
        Load();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int currentLevelCompleted;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.currentLevelCompleted = currentLevelCompleted;

        string json = JsonUtility.ToJson(data);

        BinaryFormatter bFormatter = new BinaryFormatter();
        using (Stream output = File.Create(Application.persistentDataPath + "/savefile.dat"))
        {
            bFormatter.Serialize(output, json);
        }
    }


    public void Load()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.dat";

        if (File.Exists(path))
        {
            using (Stream input = File.OpenRead(path))
            {
                string json = (string)bFormatter.Deserialize(input);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                currentLevelCompleted = data.currentLevelCompleted;
            }
        }
    }
}
