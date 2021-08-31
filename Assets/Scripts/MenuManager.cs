using System.Collections;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startGameView;
    [SerializeField] GameObject mainMenuView;
    [SerializeField] GameObject helpView;
    [SerializeField] GameObject statsView;

    [SerializeField] TextMeshProUGUI titleText;

    public void LoadStartGameView()
    {
        titleText.text = "CHOOSE LEVEL";

        startGameView.SetActive(true);
        mainMenuView.SetActive(false);
    }

    public void LoadMainMenuView()
    {
        titleText.text = "LABYRINTH HD";

        startGameView.SetActive(false);
        helpView.SetActive(false);
        statsView.SetActive(false);
        mainMenuView.SetActive(true);
    }

    public void LoadHelpView()
    {
        titleText.text = "HOW TO PLAY";

        helpView.SetActive(true);
        mainMenuView.SetActive(false);
    }

    public void LoadStatsView()
    {
        titleText.text = "HIGHSCORE";

        statsView.SetActive(true);
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
