using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ItemToPick
{
    Gold,
    Key
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

    void Start()
    {
        Application.targetFrameRate = 999;
        player.transform.position = startPosition.position;

        Instantiate(player, startPosition.position, Quaternion.identity);

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
            MainManager.instance.currentLevelCompleted += 1;
            MainManager.instance.Save();

            //Jakieœ ui gratulacje pomyœlnego przejscia poziomu itp
            SceneManager.LoadScene(0);
        }
        else
        {
            print("Message you dont have enough points to finish level!");
        }
    }

    public void GameOver()
    {

    }


}
