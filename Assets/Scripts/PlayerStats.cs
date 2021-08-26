using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textLevelsTime;
    [SerializeField] GameObject gameObjectAskReset;
    [SerializeField] Animator animator;

    private void Start()
    {
        LoadLevelTime();
        animator = gameObjectAskReset.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gameObjectAskReset.SetActive(false);
    }

    public void LoadLevelTime()
    {
        for (int i = 0; i < MainManager.instance.levelsTime.Length; i++)
        {
            textLevelsTime.text += MainManager.instance.levelsTime[i] + "\n";
        }
    }

    public void ResetStats()
    {
        MainManager.instance.currentLevelCompleted = 0;
        for (int i = 0; i < MainManager.instance.levelsTime.Length; i++)
        {
            MainManager.instance.levelsTime[i] = "00:00:00";
        }
        MainManager.instance.Save();
        
        textLevelsTime.text = "";
        LoadLevelTime();

        StartCoroutine(DisableAfterSeconds());
    }


    public void EnableAskReset() => gameObjectAskReset.SetActive(true);
    public void DisableAskReset() => StartCoroutine(DisableAfterSeconds());

    IEnumerator DisableAfterSeconds()
    {
        animator.SetTrigger("Out");
        yield return new WaitForSeconds(0.25f);
        gameObjectAskReset.SetActive(false);
    }




}
