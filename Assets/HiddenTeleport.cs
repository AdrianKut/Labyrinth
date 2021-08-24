using System.Collections;
using UnityEngine;

public class HiddenTeleport : MonoBehaviour
{
    private Transform Position_A; 
    public Transform PositionToTeleport;

    void Start()
    {
        Position_A = this.gameObject.GetComponent<Transform>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(TeleportToPosition(other.gameObject, PositionToTeleport.transform.position, Position_A.transform.position));
        }
    }

    IEnumerator TeleportToPosition(GameObject player, Vector3 positionChildren, Vector3 positionParent)
    {
       
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        do
        {
            player.transform.position = new Vector3(positionParent.x, player.transform.position.y - 0.05f, positionParent.z);
            Handheld.Vibrate();
            yield return new WaitForSeconds(0.03f);

        } while (player.transform.position.y >= -1);

        do
        {
            player.transform.position = new Vector3(positionChildren.x, player.transform.position.y + 0.05f, positionChildren.z);
            Handheld.Vibrate();
            yield return new WaitForSeconds(0.03f);

        } while (player.transform.position.y <= 0.71);

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
}
