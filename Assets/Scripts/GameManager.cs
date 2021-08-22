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
    
    public GameObject TransitionPanel;
    public GameObject ButtonPause;
    public GameObject pauseUI;

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

    public void PickItem(ItemToPick item)
    {
        switch (item)
        {
            case ItemToPick.Gold:
                goldToCollect--;
                textGold.text = "" + goldToCollect;
                break;
            case ItemToPick.Key:
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
        Timer.ResetTime();
        Application.targetFrameRate = 60;

        InitializeTextItemsToPick();

        StartCoroutine(ShowTransitionEffect());
    }

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

            player.SetActive(false);
            ButtonPause.SetActive(false);
            StartCoroutine(ShowMessageOnScreen(1, true));

            Debug.Log(Timer.GetTime());
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

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
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


}
