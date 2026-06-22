using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startGameView;
    [SerializeField] GameObject mainMenuView;
    [SerializeField] GameObject helpView;
    [SerializeField] GameObject customizeView;
    [SerializeField] GameObject statsView;

    [SerializeField] TextMeshProUGUI titleText;

    public void LoadStartGameView()
    {
        UpdateText( "CHOOSE LEVEL" );

        startGameView.SetActive( true );
        mainMenuView.SetActive( false );
    }

    public void LoadMainMenuView()
    {
        UpdateText( "LABYRINTH HD" );

        startGameView.SetActive( false );
        helpView.SetActive( false );
        statsView.SetActive( false );
        customizeView.SetActive( false );

        mainMenuView.SetActive( true );
    }

    public void LoadHelpView()
    {
        UpdateText( "HOW TO PLAY" );

        helpView.SetActive( true );
        mainMenuView.SetActive( false );
    }

    public void LoadCustomizeView()
    {
        UpdateText( "CUSTOMZIE" );

        customizeView.SetActive( true );
        mainMenuView.SetActive( false );
    }

    public void LoadStatsView()
    {
        UpdateText( "HIGHSCORE" );

        statsView.SetActive( true );
        mainMenuView.SetActive( false );
    }

    private void UpdateText( string text )
    {
        titleText.text = text;
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
