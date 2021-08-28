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
    //TO DELETE BEFORE RELEASE
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

    public void PickItem(ItemToPick nameOfItem, GameObject itemGameObject)
    {
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

    private void Awake()
    {
        player.transform.position = startPosition.position;
        player = Instantiate(player, startPosition.position, Quaternion.identity);
    }

    void Start()
    { 
        StartCoroutine(ShowTransitionEffect());
        HideUI();

        Timer.ResetTime();
        Application.targetFrameRate = 60;

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
        textFPS.SetText((Mathf.Ceil(1.0f / Time.deltaTime).ToString()));
    }

    public void FinishLevel()
    {
        if (goldToCollect == 0)
        {
            isFinished = true;
            string[] spliter = SceneManager.GetActiveScene().name.Split('_');
            int currentLevel = int.Parse(spliter[1]);

            if (currentLevel > MainManager.instance.currentLevelCompleted && currentLevel != 10)
            {
                MainManager.instance.currentLevelCompleted = (currentLevel);
                MainManager.instance.Save();
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

            Debug.Log($"OLD: MIN[{old_minutes}] SEC[{old_seconds}] MS[{old_milliseconds}]");
            Debug.Log($"NEW: MIN[{currentMinutes}] SEC[{currentSeconds}] MS[{currentMilliseconds}]");
            
            float old_allIntoSeconds = (old_minutes * 60) + old_seconds + (old_milliseconds / 100);
            float new_TimeToSeconds = (currentMinutes * 60) + currentSeconds + (currentMilliseconds / 100);
            
            Debug.Log("OLD TIME: " + old_allIntoSeconds);
            Debug.Log("NEW TIME: " + new_TimeToSeconds);

            if (old_minutes == 0 && old_seconds == 0 && old_milliseconds == 0)
            {
                MainManager.instance.levelsTime[currentLevel - 1] = "" + Timer.GetTime();
                MainManager.instance.Save();
            }
            else if (new_TimeToSeconds <= old_allIntoSeconds)
            {
                Debug.Log($"NOWY REKORD: {new_TimeToSeconds} <= {old_allIntoSeconds}");
                MainManager.instance.levelsTime[currentLevel - 1] = "" + Timer.GetTime();
                MainManager.instance.Save();
            }

            //else if (currentMinutes == old_minutes)
            //{
            //    if (currentSeconds == old_seconds)
            //    {
            //        if (currentMilliseconds <= old_milliseconds)
            //        {
            //            MainManager.instance.levelsTime[currentLevel - 1] = "" + Timer.GetTime();
            //            MainManager.instance.Save();
            //        }
            //    }
            //    else if (currentSeconds <= old_seconds)
            //    {
            //        MainManager.instance.levelsTime[currentLevel - 1] = "" + Timer.GetTime();
            //        MainManager.instance.Save();
            //    }
            //}
            //else if (currentMinutes <= old_minutes)
            //{
            //    MainManager.instance.levelsTime[currentLevel - 1] = "" + Timer.GetTime();
            //    MainManager.instance.Save();
            //}

            player.SetActive(false);
            ButtonPause.SetActive(false);

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
            player.SetActive(true);
            pauseUI.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            player.SetActive(false);
            pauseUI.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }

    }


    public void GameOver()
    {
        Handheld.Vibrate();
        Timer.ResetTime();

        Debug.Log("Game Over | RESTART LEVEL");

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.transform.position = startPosition.position;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

        for (int i = 0; i < GoldGameObjects.Length; i++)
            GoldGameObjects[i].SetActive(true);

        for (int i = 0; i < KeyGameObjects.Length; i++)
            KeyGameObjects[i].SetActive(true);

        InitializeTextItemsToPick();
    }

    #region LevelCompleteUI
    public void BackToMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadYourAsyncScene(0));
    }

    public void Restart()
    {
        StartCoroutine(LoadYourAsyncScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextLevel()
    {
        int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneNum != 10)
            StartCoroutine(LoadYourAsyncScene(currentSceneNum + 1));
        else
            StartCoroutine(LoadYourAsyncScene(1));
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
