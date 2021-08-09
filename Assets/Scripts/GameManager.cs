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

}

public abstract class GameManagerInitialazor : MonoBehaviour
{
    protected GameManager gameManager;
    protected void InitializeGameManager() => gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
}

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textFPS;

    public GameObject player;
    public Transform startPosition;

    public int goldToCollect;
    public TextMeshProUGUI textPoints;

    public bool isGameOver;

    public bool isPause = false;
    public GameObject pauseUI;

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
                player.transform.position = startPosition.position;
                Debug.Log("Game Over");
                break;
         
                
        }
    }



    public GameObject collectGoldInformationGameObject;
    public GameObject levelCompleteGameObject;

    void Start()
    {
        Application.targetFrameRate = 999;
        player.transform.position = startPosition.position;

        player = Instantiate(player, startPosition.position, Quaternion.identity);

        goldToCollect = GameObject.FindGameObjectsWithTag("Gold").Length;
        textPoints.text = "" + goldToCollect;
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

                //DODAÆ REKLAME :)
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
        //REKLAMA
    }

    public void GameOver()
    {

    }


}
