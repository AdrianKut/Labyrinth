using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] Texture audioOn;
    [SerializeField] Texture audioOff;

    private RawImage imageAudio;
    private void Start()
    {
        imageAudio = this.gameObject.GetComponent<RawImage>();
    }

    public void AudioChanger()
    {
        if (MainManager.isAudio)
        {
            MainManager.isAudio = false;
            imageAudio.texture = audioOff;
        }
        else
        {
            MainManager.isAudio = true;
            imageAudio.texture = audioOn;
        }
    }
}
