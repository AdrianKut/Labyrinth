using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinishZone : GameManagerInitialazor
{

    public void Start()
    {
        InitializeGameManager();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.FinishLevel();
        }
    }
}
