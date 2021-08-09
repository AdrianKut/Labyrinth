using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesObject : GameManagerInitialazor
{
    public Obstacle typeOfObstacle;

    void Start()
    {
        InitializeGameManager();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.HitObstacles(typeOfObstacle);
            Debug.Log("GAME OVER!");
        }
    }
}
