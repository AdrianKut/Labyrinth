using System.Collections;
using UnityEngine;

public class Camera : GameManagerInitialazor
{
    [SerializeField] GameObject CenterPoint;

    [SerializeField] Vector3 offset;
    [SerializeField] bool canFollowPlayer = false;

    private Transform player;

    void Start()
    {
        InitializeGameManager();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player.gameObject.SetActive(false);

        StartCoroutine(CameraToMap());
    }

    IEnumerator CameraToMap()
    {
        // USUN¥Æ KOMENTARZE PRZED WYDANIEM


        //yield return new WaitForSeconds(2f);
        //do
        //{
        //    transform.position = new Vector3(0, transform.position.y - 0.2f, -10f);
        //    yield return new WaitForSeconds(0.01f);

        //} while (transform.position.y >= 16f);

        //yield return new WaitForSeconds(1.25f);

        //gameManager.ButtonPause.SetActive(true);
        //StartCoroutine(gameManager.ShowTransitionEffect());
        canFollowPlayer = true;

        yield return new WaitForSeconds(1f);

        player.gameObject.SetActive(true);
    }



    void LateUpdate()
    {
        if (canFollowPlayer)
        {
            transform.position = player.transform.position + offset;
            transform.localRotation = Quaternion.Euler(70, 0, 0);
        }
        else
            transform.LookAt(CenterPoint.transform);

    }
}
