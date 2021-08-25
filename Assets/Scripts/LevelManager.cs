using System.Collections;
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

    private void OnEnable()
    {
        LoadButtonsLevel();
    }

    public void LoadButtonsLevel()
    {
        if (MainManager.instance.currentLevelCompleted == 0)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].GetComponent<Button>().interactable = false;
            }
            levels[0].GetComponent<Button>().interactable = true;
        }
        else
        {
            for (int i = 0; i < MainManager.instance.currentLevelCompleted + 1; i++)
            {
                levels[i].GetComponent<Button>().interactable = true;
            }
        }
    }


    public void LoadSelectedLevel()
    {
        var sceneName = EventSystem.current.currentSelectedGameObject.name.ToUpper();
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator LoadYourAsyncScene(string nameOfScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nameOfScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


}

