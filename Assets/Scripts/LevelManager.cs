using System.Collections.Generic;
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
        foreach (var level in levels)
        {
            level.GetComponent<Button>().interactable = false;
        }

       // First level is always available
        //levels[0].GetComponent<Button>().interactable = true;
        byte i = 0;
        foreach (KeyValuePair<int, bool> item in MainManager.instance.levelNumberIsComplete)
        {
            if (item.Value == true)
                levels[i].GetComponent<Button>().interactable = true;

            i++;
        }
    }

    public void LoadSelectedLevel()
    {
        var sceneName = EventSystem.current.currentSelectedGameObject.name.ToUpper();
        SceneManager.LoadScene(sceneName);
    }


}

