using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startGameView;
    [SerializeField] GameObject mainMenuView;

    [SerializeField] Animator transitionMainMenuAnim;
    [SerializeField] Animator transitionStartGameAnim;

    public void LoadStartGameView()
    {
        StartCoroutine(LoadTransition());
        startGameView.SetActive(true);
        mainMenuView.SetActive(false);
    }

    public void LoadMainMenuView()
    {
        StartCoroutine(LoadTransition());
        startGameView.SetActive(false);
        mainMenuView.SetActive(true);

    }

    IEnumerator LoadTransition()
    {
        transitionMainMenuAnim.SetTrigger("end");
        transitionStartGameAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
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
