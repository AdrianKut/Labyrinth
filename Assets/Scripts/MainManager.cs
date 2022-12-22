using GooglePlayGames;
using GooglePlayGames.BasicApi;
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

    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void Start()
    {    
        SignInToGooglePlayServices();
        Application.targetFrameRate = 90;
        Load();


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Advertisement.Initialize(gameId);
        Advertisement.Load(intersititalAd);
    }

    public bool isConnectedToGooglePlayServices;
    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            if (result == SignInStatus.Success)
                isConnectedToGooglePlayServices = true;
            else
                isConnectedToGooglePlayServices = false;
        });
    }

    private void ShowAndroidToastMessage(string message)
    {
        SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.oneSecond);
    }

    public void ShowAchievementsGoogleServices()
    {
        if (isConnectedToGooglePlayServices)
            Social.ShowAchievementsUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }

    public void ShowLeaderboardsGoogleServices()
    {
        if (isConnectedToGooglePlayServices)
            Social.ShowLeaderboardUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }


    public static void ShowIntersitialAd()
    {
            Advertisement.Show(intersititalAd);
    }

    public void ShowFacebookURL()
    {
        Application.OpenURL("https://www.facebook.com/96GAMES");
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
