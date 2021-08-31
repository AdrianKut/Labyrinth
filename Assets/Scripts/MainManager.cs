using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Advertisements;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    private MainManager() { }

    public int currentLevelCompleted;
    public string[] levelsTime = new string[10];
    public static bool isAudio = true;
    public static bool isFPS = false;
    public static bool isVibration = true;

    public static string gameId = "4288261";
    public static string bannerAd = "Banner_Android";
    public static string intersititalAd = "Interstitial_Android";

    void Start()
    {
        Application.targetFrameRate = 90;
        Load();


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Advertisement.Initialize(gameId, true);
        Advertisement.Load(intersititalAd);
    }

    public static void ShowIntersitialAd()
    {
        if (Advertisement.IsReady())
            Advertisement.Show(intersititalAd);
    }

    [System.Serializable]
    class SaveData
    {
        public int currentLevelCompleted;
        public string[] levelsTime = new string[10];
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.currentLevelCompleted = currentLevelCompleted;
        data.levelsTime = levelsTime;

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
                levelsTime = data.levelsTime;
            }
        }
    }
}
