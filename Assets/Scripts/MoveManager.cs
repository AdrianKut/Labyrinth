using System.Collections;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public float zBound;
    public float xBound;
    public float moveToBoundLoop;
    public float moveSpeed;

    public bool moveForward;
    public bool moveBack;
    public bool moveLeft;
    public bool moveRight;

    RotateManager rotateManager;

    public bool startedOnce = false;
    public bool canChangeRotate = false;
    private void Start()
    {
        rotateManager = GetComponent<RotateManager>();
        //StartCoroutine(Move());
    }

    private void Update()
    {
        if (startedOnce == false)
        {
            startedOnce = true;
            StartCoroutine(Move());
        }


        if (canChangeRotate)
        {
            if (moveForward)
                rotateManager.axis = new Vector3(0, 0, 90f);
            else if (moveBack)
                rotateManager.axis = new Vector3(0, 0, -90f);

            if (moveRight)
                rotateManager.axis = new Vector3(0, 0, -90f);
            else if (moveLeft)
                rotateManager.axis = new Vector3(0, 0, 90f);
        }




    }

    IEnumerator Move()
    {
        do
        {
            if (moveForward)
            {
                for (int i = 0; i < moveToBoundLoop; i++)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + moveSpeed, -zBound, zBound));
                    yield return new WaitForSeconds(0.01f);
                }
                moveForward = false;
                moveRight = false;
                moveBack = true;
                moveLeft = false;
            }

            if (moveBack)
            {
                for (int i = 0; i < moveToBoundLoop; i++)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - moveSpeed, -zBound, zBound));
                    yield return new WaitForSeconds(0.01f);
                }

                moveForward = true;
                moveRight = false;
                moveBack = false;
                moveLeft = false;
            }

            if (moveRight)
            {
                for (int i = 0; i < moveToBoundLoop; i++)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x + moveSpeed, -xBound, xBound), transform.position.y, transform.position.z);
                    yield return new WaitForSeconds(0.01f);
                }

                moveForward = false;
                moveRight = false;
                moveBack = false;
                moveLeft = true;
            }

            if (moveLeft)
            {
                for (int i = 0; i < moveToBoundLoop; i++)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x - moveSpeed, -xBound, xBound), transform.position.y, transform.position.z);
                    yield return new WaitForSeconds(0.01f);
                }

                moveForward = false;
                moveRight = true;
                moveBack = false;
                moveLeft = false;
            }


        } while (true);
    }




}
