using System.Text;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUITimer;

    private static readonly StringBuilder sB = new StringBuilder();
    private static float time;

    public static void ResetTime() => time = 0;
    public static string GetTime() => sB.ToString();

    private void HideTimer() => TextMeshProUGUITimer.text = "";

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void DisplayTime()
    {
        if (!gameManager.isFinished && gameManager.isStarted)
        {
            time += Time.deltaTime;
            sB.Clear();

            float minutes = Mathf.Floor(time / 60);
            float seconds = Mathf.Floor(time % 60);
            float milliseconds = Mathf.Floor(time * 100);
            milliseconds = (milliseconds % 100);

            sB.Append(string.Format("{0:00}:{1:00}:{2:F0}", minutes, seconds,milliseconds));

            TextMeshProUGUITimer.text = "" + sB;
        }
    }


    void Update()
    {
        DisplayTime();
    }


}
