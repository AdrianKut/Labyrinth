using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ItemToPick
{
    Gold,
    Key
}

public enum Obstacle
{
    Hole,
    Spike,
    Rock,
}

public abstract class GameManagerInitialazor : MonoBehaviour
{
    protected GameManager gameManager;
    protected void InitializeGameManager() => gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
}

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textFPS;

    public GameObject MainUI;
    public GameObject TransitionPanel;
    public GameObject ButtonPause;
    public GameObject pauseUI;

    [Header("Level Complete")]
    public GameObject LevelCompleteUI;
    public TextMeshProUGUI timerText;

    public bool isGameOver;
    public bool isPaused = false;
    public bool isStarted = false;
    public bool isFinished = false;

    [Header("Player")]
    public GameObject player;
    public Transform startPosition;

    [Header("Items")]
    public int goldToCollect;
    private GameObject[] GoldGameObjects;
    public TextMeshProUGUI textGold;

    public int keyToCollect;
    private GameObject[] KeyGameObjects;

    [Header("Messages")]
    public GameObject TextMessageGameObject;
    public Texture[] TextureMessage; // 0 - Collect Gold | 1 - Level complete | 2 - You need A Key | 

    [Header("Particle Effects")]
    public GameObject GameObjectParticleEffectPickupGold;

    private AudioSource MainAudioSource;

    public void PlaySound(string nameOfAudioClip)
    {
        string path = "Audio/" + nameOfAudioClip;
        AudioClip clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;
        MainAudioSource.PlayOneShot(clip);
    }

    public void PickItem(ItemToPick nameOfItem, GameObject itemGameObject)
    {
        if (MainManager.isAudio)
            PlaySound("CollectGold");

        GameObject tempParticleEffect;
        switch (nameOfItem)
        {
            case ItemToPick.Gold:
                tempParticleEffect = GameObject.Instantiate(GameObjectParticleEffectPickupGold, itemGameObject.transform.position, Quaternion.identity);
                tempParticleEffect.GetComponent<ParticleSystem>().Play();
                Destroy(tempParticleEffect, 2f);
                goldToCollect--;
                textGold.text = "" + goldToCollect;
                break;
            case ItemToPick.Key:
                tempParticleEffect = GameObject.Instantiate(GameObjectParticleEffectPickupGold, itemGameObject.transform.position, Quaternion.identity);
                tempParticleEffect.GetComponent<ParticleSystem>().Play();
                Destroy(tempParticleEffect, 2f);
                keyToCollect++;
                break;
        }
    }

    public void HitObstacles(Obstacle obstacle)
    {
        switch (obstacle)
        {
            case Obstacle.Hole:
                GameOver();
                break;
            case Obstacle.Spike:
                GameOver();
                break;
            case Obstacle.Rock:
                GameOver();
                break;
        }
    }

    private GameObject lockedDoor;

    private void Awake()
    {
        MainAudioSource = GetComponent<AudioSource>();

        player.transform.position = startPosition.position;
        player = Instantiate(player, startPosition.position, Quaternion.identity);
    }

    void Start()
    {
        lockedDoor = GameObject.Find("LockedWall");

        if (MainManager.isAudio == false)
            MainAudioSource.volume = 0;

        StartCoroutine(ShowTransitionEffect());
        HideUI();

        Timer.ResetTime();
        InitializeTextItemsToPick();
    }

    public void HideUI() => MainUI.SetActive(false);
    public void ShowUI() => MainUI.SetActive(true);

    private void InitializeTextItemsToPick()
    {
        GoldGameObjects = GameObject.FindGameObjectsWithTag("Gold") as GameObject[];
        KeyGameObjects = GameObject.FindGameObjectsWithTag("Key") as GameObject[];

        goldToCollect = GoldGameObjects.Length;
        textGold.text = "" + goldToCollect;

        keyToCollect = 0;
    }

    public IEnumerator ShowTransitionEffect()
    {
        TransitionPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        TransitionPanel.SetActive(false);
    }

    void Update()
    {
        if (MainManager.isFPS)
            textFPS.SetText((Mathf.Ceil(1.0f / Time.deltaTime).ToString()));
        else
            textFPS.gameObject.SetActive(false);
    }

    static void ReportNewHighscoreToGooglePlayServices(long time, int level)
    {

        switch (level)
        {
            case 1:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_1, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;

            case 2:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_2, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;

            case 3:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_3, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;

            case 4:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_4, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;

            case 5:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_5, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;

            case 6:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_6, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;

            case 7:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_7, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;


            case 8:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_8, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;


            case 9:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_9, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;


            case 10:
                Social.ReportScore((long)time, GPGSIds.leaderboard_level_10, (success) =>
                {
                    if (!success)
                    {
                        Debug.LogError("Unable to post highscore!");
                    }
                    else
                    {
                        Debug.Log("SUCCES " + level);
                    }
                });
                break;

            default:
                break;
        }


    }

    private static void GetSpliterCurrentLevel(out string[] spliter, out int currentLevel)
    {
        spliter = SceneManager.GetActiveScene().name.Split('_');
        currentLevel = int.Parse(spliter[1]);
    }

    public void FinishLevel()
    {
        if (goldToCollect == 0)
        {
            isFinished = true;
            string[] spliter;
            int currentLevel;
            GetSpliterCurrentLevel(out spliter, out currentLevel);

            if (currentLevel > MainManager.instance.currentLevelCompleted && currentLevel != 10)
            {
                MainManager.instance.currentLevelCompleted = (currentLevel);
                MainManager.instance.Save();
            }

            switch (currentLevel)
            {

                case 1 when MainManager.instance.isConnectedToGooglePlayServices:
                    Social.ReportProgress(GPGSIds.achievement_its_only_beginning, 100f, null);
                    break;

                case 5 when MainManager.instance.isConnectedToGooglePlayServices:
                    Social.ReportProgress(GPGSIds.achievement_keep_calm_and_play_next_levels, 100f, null);
                    break;

                case 10 when MainManager.instance.isConnectedToGooglePlayServices:
                    Social.ReportProgress(GPGSIds.achievement_youre_amazing, 100f, null);
                    break;
            }


            float old_minutes, old_seconds, old_milliseconds, currentMinutes, currentSeconds, currentMilliseconds;

            spliter = MainManager.instance.levelsTime[currentLevel - 1].Split(':');
            old_minutes = float.Parse(spliter[0]);
            old_seconds = float.Parse(spliter[1]);
            old_milliseconds = float.Parse(spliter[2]);

            spliter = Timer.GetTime().Split(':');
            currentMinutes = float.Parse(spliter[0]);
            currentSeconds = float.Parse(spliter[1]);
            currentMilliseconds = float.Parse(spliter[2]);

            float old_allIntoSeconds = (old_minutes * 60) + old_seconds + (old_milliseconds / 100);
            float new_TimeToSeconds = (currentMinutes * 60) + currentSeconds + (currentMilliseconds / 100);

            if (MainManager.instance.isConnectedToGooglePlayServices)
                ReportNewHighscoreToGooglePlayServices((long)new_TimeToSeconds, currentLevel);
            else
                Debug.LogError("Not logged in!");

            if (old_minutes == 0 && old_seconds == 0 && old_milliseconds == 0)
            {
                MainManager.instance.levelsTime[currentLevel - 1] = "" + Timer.GetTime();
                MainManager.instance.Save();


            }
            else if (new_TimeToSeconds <= old_allIntoSeconds)
            {
                MainManager.instance.levelsTime[currentLevel - 1] = "" + Timer.GetTime();
                MainManager.instance.Save();
            }


            player.SetActive(false);
            ButtonPause.SetActive(false);

            PlaySound("levelComplete");
            LevelCompleteUI.SetActive(true);

            var timer = this.gameObject.GetComponent<Timer>();
            timer.HideTimer();
            timerText.SetText("          " + Timer.GetTime());
        }
        else
        {
            StartCoroutine(ShowMessageOnScreen(0));
        }
    }



    public IEnumerator ShowMessageOnScreen(int messageNum, bool backToMenu = false)
    {
        TextMessageGameObject.SetActive(true);
        TextMessageGameObject.GetComponent<RawImage>().texture = TextureMessage[messageNum];
        yield return new WaitForSeconds(2.5f);
        TextMessageGameObject.SetActive(false);

        if (backToMenu)
            BackToMenu();
    }

    public void Pause()
    {
        if (isPaused)
        {
            textFPS.gameObject.SetActive(true);
            player.SetActive(true);
            pauseUI.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            textFPS.gameObject.SetActive(false);
            player.SetActive(false);
            pauseUI.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }

    }

    public void Vibrate()
    {
        if (MainManager.isVibration)
            Handheld.Vibrate();
    }


    public void GameOver()
    {
        if (MainManager.instance.isConnectedToGooglePlayServices)
            Social.ReportProgress(GPGSIds.achievement_ole_this_is_hole, 100f, null);

        PlaySound("gameOver");
        Vibrate();
        Timer.ResetTime();

        MainManager.ShowIntersitialAd();

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.transform.position = startPosition.position;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

        for (int i = 0; i < GoldGameObjects.Length; i++)
            GoldGameObjects[i].SetActive(true);

        for (int i = 0; i < KeyGameObjects.Length; i++)
            KeyGameObjects[i].SetActive(true);

        InitializeTextItemsToPick();

        if (lockedDoor != null)
            lockedDoor.transform.position = new Vector3(lockedDoor.transform.position.x, 0.88f, lockedDoor.transform.position.z);
    }

    #region LevelCompleteUI
    public void BackToMenu()
    {
        int currentLevel;
        GetSpliterCurrentLevel(out _, out currentLevel);

        Time.timeScale = 1;
        StartCoroutine(LoadYourAsyncScene(0));

    }

    public void Restart()
    {
        StartCoroutine(LoadYourAsyncScene(SceneManager.GetActiveScene().buildIndex)); ;
    }

    public void NextLevel()
    {
        int currentLevel;
        GetSpliterCurrentLevel(out _, out currentLevel);

        if (currentLevel != 1 && currentLevel != 2)
        {
            MainManager.ShowIntersitialAd();
            int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneNum != 10)
                StartCoroutine(LoadYourAsyncScene(currentSceneNum + 1));
            else
                StartCoroutine(LoadYourAsyncScene(1));
        }
        else
        {
            int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneNum != 10)
                StartCoroutine(LoadYourAsyncScene(currentSceneNum + 1));
            else
                StartCoroutine(LoadYourAsyncScene(1));
        }
    }

    IEnumerator LoadYourAsyncScene(int numSceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(numSceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    #endregion

}
