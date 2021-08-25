using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textLevelsTime;

    private void Start()
    {
        LoadLevelTime();
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
    }

}
