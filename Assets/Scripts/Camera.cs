using System.Collections;
using UnityEngine;

public class Camera : GameManagerInitialazor
{
    [SerializeField] GameObject CenterPoint;

    [SerializeField] Vector3 offset;
    [SerializeField] bool canFollowPlayer = false;
    [SerializeField] float cameraLimitYPosition = 17f;

    [SerializeField] GameObject FirstCameraGameObject;
    [SerializeField] GameObject SecondCameraGameObject;

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

        yield return new WaitForSeconds(2f);
        do
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, -10f);
            yield return new WaitForSeconds(0.01f);

        } while (transform.position.y >= cameraLimitYPosition);

        yield return new WaitForSeconds(2f);
        StartCoroutine(gameManager.ShowTransitionEffect());

        canFollowPlayer = true;

        player.gameObject.SetActive(true);
        gameManager.isStarted = true;
        gameManager.ShowUI();
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

    public void CameraChange()
    {
        if (FirstCameraGameObject.activeSelf)
        {
            
            FirstCameraGameObject.SetActive(false);
            SecondCameraGameObject.SetActive(true);
        }
        else if (SecondCameraGameObject.activeSelf)
        {
            Time.timeScale = 1;
            FirstCameraGameObject.SetActive(true);
            SecondCameraGameObject.SetActive(false);
        }
    }
}
