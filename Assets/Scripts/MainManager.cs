using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager instance;
    private MainManager() { }

    public Dictionary<int, bool> levelNumberIsComplete = new Dictionary<int, bool>()
    {
        {1,true },
        {2,false },
        {3,false },
        {4,false },
        {5,false },
        {6,false },
        {7,false },
        {8,false },
        {9,false },
        {10,false },
   };

    public static bool isAudio = true;

    void Awake()
    {
        
        Save();

        foreach (var item in levelNumberIsComplete)
        {
            Debug.Log("AWAKE: "+item.Key + ": " + item.Value);
        }
    }


    void Start()
    {
        Load();
        foreach (var item in levelNumberIsComplete)
        {
            Debug.Log("START: "+ item.Key + ": " + item.Value);
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public Dictionary<int, bool> levelNumberIsComplete = new Dictionary<int, bool>();
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.levelNumberIsComplete = levelNumberIsComplete;

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
                levelNumberIsComplete = data.levelNumberIsComplete;
            }
        }
    }
}
