using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleAndRockManager : MonoBehaviour
{
    public GameObject Rock, HoleRock;

    private Collider rock_Collider, hole_Collider;
    private Vector3 rock_StartingPosition;

    void Start()
    {
        if (Rock != null)
            rock_Collider = Rock.GetComponent<Collider>();

        if (HoleRock != null)
            hole_Collider = HoleRock.GetComponent<Collider>();

        rock_StartingPosition = Rock.transform.position;
    }

    private bool startedOnce = false;
    void Update()
    {
        if (rock_Collider.bounds.Intersects(hole_Collider.bounds) && startedOnce == false)
        {
            startedOnce = true;
            StartCoroutine(BackToStartPosition(Rock, rock_StartingPosition));
        }
    }

    private IEnumerator BackToStartPosition(GameObject Rock, Vector3 startingPosition)
    {

        Rock.gameObject.GetComponent<RotateManager>().enabled = false;
        Rock.gameObject.GetComponent<MoveManager>().enabled = false;

        do
        {
            Rock.transform.position = new Vector3(Rock.transform.position.x, Rock.transform.position.y - 0.05f, Rock.transform.position.z);
            yield return new WaitForSeconds(0.01f);

        } while (Rock.transform.position.y >= -2);

        Rock.SetActive(false);

        yield return new WaitForSeconds(1f);
        Rock.gameObject.GetComponent<RotateManager>().enabled = true;
        Rock.gameObject.GetComponent<MoveManager>().enabled = true;

        Rock.SetActive(true);
        Rock.GetComponent<MoveManager>().startedOnce = false;
        Rock.transform.position = startingPosition;

        startedOnce = false;

    }
}
