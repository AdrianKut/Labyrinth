using System.Collections;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public float zBound;
    public float moveToBoundLoop;
    public float moveSpeed;

    public bool moveForward;
    public bool moveBack;
    
    RotateManager rotateManager;

    private void Start()
    {
        rotateManager = GetComponent<RotateManager>();
        StartCoroutine(Move());
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
                moveBack = true;
            }

            if (moveBack)
            {
                for (int i = 0; i < moveToBoundLoop; i++)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - moveSpeed, -zBound, zBound));
                    yield return new WaitForSeconds(0.01f);
                }

                moveForward = true;
                moveBack = false;
            }
        

            //moveForward = true;
            //moveBack = false;

            //for (int i = 0; i < moveToBoundLoop; i++)
            //{
            //    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + moveSpeed, -zBound, zBound));
            //    yield return new WaitForSeconds(0.01f);
            //}

            //moveForward = false;
            //moveBack = true;

            //for (int i = 0; i < moveToBoundLoop; i++)
            //{
            //    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - moveSpeed, -zBound, zBound));
            //    yield return new WaitForSeconds(0.01f);
            //}

        } while (true);
    }


    private void Update()
    {
        if (moveForward)
            rotateManager.axis = new Vector3(0, -90f, 0);
        else if (moveBack)
            rotateManager.axis = new Vector3(0, 90f, 0);
    }

}
