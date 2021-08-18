using System.Collections;
using UnityEngine;

public class LockedDoor : GameManagerInitialazor
{
    public float hideSpeed;

    void Start()
    {
        InitializeGameManager();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameManager.keyToCollect == 1)
            {
                Debug.Log("OPENING");
                gameManager.keyToCollect = 0;
                gameManager.textKey.text = "" + gameManager.keyToCollect;
                StartCoroutine(HideWall());
            }
            else
                Debug.Log("FIND KEY!!!");
        }
    }

    IEnumerator HideWall()
    {
        do
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - hideSpeed, transform.position.z);
            yield return new WaitForSeconds(0.01f);

        } while (transform.position.y >= -1);

        
    }


}
