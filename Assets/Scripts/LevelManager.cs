using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    private void Start()
    {
        LoadButtonsLevel();
    }

    private void LoadButtonsLevel()
    {
        for (int i = 0; i < MainManager.instance.currentLevelCompleted + 1; i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
        }
    }

    public void LoadSelectedLevel()
    {
        var sceneName = EventSystem.current.currentSelectedGameObject.name.ToUpper();
        SceneManager.LoadScene(sceneName);
    }


}

