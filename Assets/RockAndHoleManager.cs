using System.Collections;
using UnityEngine;

public class RockAndHoleManager : MonoBehaviour
{
    public GameObject Rock_1, HoleRock_1, Rock_2, HoleRock_2;

    private Collider rock_1_Collider, hole_1_Collider, rock_2_Collider, hole_2_Collider;
    public Vector3 rock_1_StartingPosition, rock_2_StartingPosition;

    void Start()
    {
        if (Rock_1 != null)
            rock_1_Collider = Rock_1.GetComponent<Collider>();

        if (Rock_2 != null)
            rock_2_Collider = Rock_2.GetComponent<Collider>();

        if (HoleRock_1 != null)
            hole_1_Collider = HoleRock_1.GetComponent<Collider>();


        if (HoleRock_2 != null)
            hole_2_Collider = HoleRock_2.GetComponent<Collider>();

        rock_1_StartingPosition = Rock_1.transform.position;
        rock_2_StartingPosition = Rock_2.transform.position;


    }

    private bool startedOnce_rock1 = false, startedOnce_rock2 = false;

    void Update()
    {
        if (rock_1_Collider.bounds.Intersects(hole_1_Collider.bounds) && startedOnce_rock1 == false)
        {
            startedOnce_rock1 = true;
            StartCoroutine(BackToStartPosition(Rock_1, rock_1_StartingPosition));
        }

        if (rock_2_Collider.bounds.Intersects(hole_2_Collider.bounds) && startedOnce_rock2 == false)
        {
            startedOnce_rock2 = true;
            StartCoroutine(BackToStartPosition(Rock_2, rock_2_StartingPosition));
        }
    }

    private IEnumerator BackToStartPosition(GameObject Rock, Vector3 startingPosition)
    {

        Rock.gameObject.GetComponent<RotateManager>().enabled = false;
        Rock.gameObject.GetComponent<MoveManager>().enabled = false;

        do
        {
            Rock.transform.position = new Vector3(Rock.transform.position.x, Rock.transform.position.y - 0.1f, Rock.transform.position.z);
            yield return new WaitForSeconds(0.01f);

        } while (Rock.transform.position.y >= -2);

        Rock.SetActive(false);

        yield return new WaitForSeconds(1f);
        Rock.gameObject.GetComponent<RotateManager>().enabled = true;
        Rock.gameObject.GetComponent<MoveManager>().enabled = true;

        Rock.SetActive(true);
        Rock.GetComponent<MoveManager>().startedOnce = false;
        Rock.transform.position = startingPosition;

        startedOnce_rock1 = false;
        startedOnce_rock2 = false;
    }

}
