using System.Collections;
using UnityEditor;
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
            if (gameManager.keyToCollect >= 1)
            {
                gameManager.keyToCollect = 0;

                gameManager.PlaySound("wallFall");
                StartCoroutine(HideWall());
            }
            else
                gameManager.StartCoroutine(gameManager.ShowMessageOnScreen(2));
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
