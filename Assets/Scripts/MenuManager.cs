using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startGameView;
    [SerializeField] GameObject mainMenuView;
    [SerializeField] GameObject helpView;

    public void LoadStartGameView()
    {
        startGameView.SetActive(true);
        mainMenuView.SetActive(false);
    }

    public void LoadMainMenuView()
    {
        startGameView.SetActive(false);
        helpView.SetActive(false);
        mainMenuView.SetActive(true);
    }

    public void LoadHelpView()
    {
        helpView.SetActive(true);
        mainMenuView.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
