using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    Audio,
    FPS,
    Vibration
}

public class ButtonController : MonoBehaviour
{
    [SerializeField] Sprite buttonON;
    [SerializeField] Sprite buttonOFF;
    [SerializeField] ButtonType buttonType;

    private Image image;

    private void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        InitializeButtonImage();
    }

    private void InitializeButtonImage()
    {
        switch (buttonType)
        {
            case ButtonType.Audio:
                if (MainManager.isAudio)
                    image.sprite = buttonON;
                else
                    image.sprite = buttonOFF;
                break;
            case ButtonType.FPS:
                if (MainManager.isFPS)
                    image.sprite = buttonON;
                else
                    image.sprite = buttonOFF;
                break;
            case ButtonType.Vibration:
                if (MainManager.isVibration)
                    image.sprite = buttonON;
                else
                    image.sprite = buttonOFF;
                break;
        }
    }

    public void Changer()
    {
        switch (buttonType)
        {
            case ButtonType.Audio:
                if (MainManager.isAudio)
                {
                    MainManager.isAudio = false;
                    goto disable;
                }
                else
                {
                    MainManager.isAudio = true;
                    goto activate;
                }
            case ButtonType.FPS:
                if (MainManager.isFPS)
                {
                    MainManager.isFPS = false;
                    goto disable;
                }
                else
                {
                    MainManager.isFPS = true;
                    goto activate;
                }
            case ButtonType.Vibration:
                if (MainManager.isVibration)
                {
                    MainManager.isVibration = false;
                    goto disable;
                }
                else
                {
                    MainManager.isVibration = true;
                    goto activate;
                }

            disable:
                image.sprite = buttonOFF;
                break;

            activate:
                image.sprite = buttonON;
                break;

        }


    }
}
