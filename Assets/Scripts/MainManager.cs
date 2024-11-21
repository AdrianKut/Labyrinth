using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Advertisements;

public class MainManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
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


        if( instance == null )
        {
            instance = this;
            DontDestroyOnLoad( gameObject );
        }

        Advertisement.Initialize( gameId, false, this );
        Advertisement.Load( intersititalAd, this );
    }

    public bool isConnectedToGooglePlayServices;
    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate( ProcessAuthentication );
        if( Social.localUser.authenticated == false )
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate( ProcessAuthentication );
        }
    }

    internal void ProcessAuthentication( SignInStatus status )
    {
        switch( status )
        {
            case SignInStatus.Success:
                isConnectedToGooglePlayServices = true;
                break;
            case SignInStatus.InternalError:
                isConnectedToGooglePlayServices = false;
                break;
            case SignInStatus.Canceled:
                isConnectedToGooglePlayServices = false;
                break;
            default:
                break;
        }
    }

    private void ShowAndroidToastMessage( string message )
    {
        SSTools.ShowMessage( message, SSTools.Position.bottom, SSTools.Time.oneSecond );
    }

    public void ShowAchievementsGoogleServices()
    {
        if( isConnectedToGooglePlayServices )
            Social.ShowAchievementsUI();
        else
            ShowAndroidToastMessage( "Couldn't load google services!" );
    }

    public void ShowLeaderboardsGoogleServices()
    {
        if( isConnectedToGooglePlayServices )
            Social.ShowLeaderboardUI();
        else
            ShowAndroidToastMessage( "Couldn't load google services!" );
    }


    public void ShowIntersitialAd()
    {
        Advertisement.Show( intersititalAd, this );
    }

    public void ShowFacebookURL()
    {
        Application.OpenURL( "https://www.facebook.com/96GAMES" );
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

        string json = JsonUtility.ToJson( data );

        BinaryFormatter bFormatter = new BinaryFormatter();
        using( Stream output = File.Create( Application.persistentDataPath + "/savefile.dat" ) )
        {
            bFormatter.Serialize( output, json );
        }
    }


    public void Load()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.dat";

        if( File.Exists( path ) )
        {
            using( Stream input = File.OpenRead( path ) )
            {
                string json = (string)bFormatter.Deserialize( input );
                SaveData data = JsonUtility.FromJson<SaveData>( json );
                currentLevelCompleted = data.currentLevelCompleted;
                levelsTime = data.levelsTime;
            }
        }
    }

    #region ADS
    public void OnUnityAdsAdLoaded( string placementId )
    {
        Debug.Log( $"Ads {placementId} loaded complete" );
    }

    public void OnUnityAdsFailedToLoad( string placementId, UnityAdsLoadError error, string message )
    {
        Debug.LogError( $"Ads {placementId} load failed: " + message + " " + error );
    }

    public void OnUnityAdsShowFailure( string placementId, UnityAdsShowError error, string message )
    {
        Debug.LogError( $"Ads {placementId} show failed: " + message + " " + error );
    }

    public void OnUnityAdsShowStart( string placementId )
    {
        Debug.Log( $"Ads {placementId} show start" );
    }

    public void OnUnityAdsShowClick( string placementId )
    {
        Debug.Log( $"Ads {placementId} show clicked sucessfully" );
    }

    public void OnUnityAdsShowComplete( string placementId, UnityAdsShowCompletionState showCompletionState )
    {
        Debug.Log( $"Ads {placementId} complete sucessfully" );
    }

    public void OnInitializationComplete()
    {
        Debug.Log( $"Ads initializaion complete" );
    }

    public void OnInitializationFailed( UnityAdsInitializationError error, string message )
    {
        Debug.LogError( $"Ads failed: " + message + " " + error );
    }
    #endregion
}
