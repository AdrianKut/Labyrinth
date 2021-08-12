using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ItemToPick
{
    Gold,
    Key
}

public enum Obstacle
{
    Hole,
    Spike,
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

    public GameObject player;
    public Transform startPosition;

    public int goldToCollect;
    private GameObject[] GoldGameObjects;
    public TextMeshProUGUI textPoints;

    public bool isGameOver;

    public bool isPause = false;
    public GameObject pauseUI;

    public GameObject collectGoldInformationGameObject;
    public GameObject levelCompleteGameObject;

    public void PickItem(ItemToPick item)
    {
        switch (item)
        {
            case ItemToPick.Gold:
                goldToCollect--;
                textPoints.text = "" + goldToCollect;
                break;
            case ItemToPick.Key:
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
        }
    }


    void Start()
    {

        Application.targetFrameRate = 60;
        player.transform.position = startPosition.position;

        player = Instantiate(player, startPosition.position, Quaternion.identity);

        GoldGameObjects = GameObject.FindGameObjectsWithTag("Gold") as GameObject[];

        goldToCollect = GoldGameObjects.Length;
        textPoints.text = "" + goldToCollect;

        StartCoroutine(ShowTransitionEffect());
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
            string[] spliter = SceneManager.GetActiveScene().name.Split('_');
            int currentLevel = int.Parse(spliter[1]);

            if (currentLevel > MainManager.instance.currentLevelCompleted && currentLevel != 10)
            {
                MainManager.instance.currentLevelCompleted = (currentLevel);
                MainManager.instance.Save();
            }

            ButtonPause.SetActive(false);
            StartCoroutine(ShowInformation("complete"));
        }
        else
        {
            StartCoroutine(ShowInformation("collectGold"));
        }
    }

    IEnumerator ShowInformation(string message)
    {
        switch (message)
        {
            case "complete":
                player.SetActive(false);
                levelCompleteGameObject.SetActive(true);
                yield return new WaitForSeconds(2.75f);
                levelCompleteGameObject.SetActive(true);

                //DODA� REKLAME =]
                BackToMenu();
                break;

            case "collectGold":
                collectGoldInformationGameObject.SetActive(true);
                yield return new WaitForSeconds(2f);
                collectGoldInformationGameObject.SetActive(false);
                break;

        }
    }

    public void Pause()
    {
        if (isPause)
        {
            player.SetActive(true);
            pauseUI.SetActive(false);
            isPause = false;
            Time.timeScale = 1;
        }
        else
        {
            player.SetActive(false);
            pauseUI.SetActive(true);
            isPause = true;
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
        Debug.Log("Game Over | RESTART LEVEL");

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.transform.position = startPosition.position;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

        for (int i = 0; i < GoldGameObjects.Length; i++)
            GoldGameObjects[i].SetActive(true);

        goldToCollect = GoldGameObjects.Length;
        textPoints.text = "" + goldToCollect;
    }


}
