using System.Collections;
using UnityEngine;

public class PopUpEffect : MonoBehaviour
{
    public float effectSpeed = 3f;
    private RectTransform transfom;
    private void Start()
    {

        StartCoroutine(PopUp());
    }

    private void OnEnable()
    {

        StartCoroutine(PopUp());
    }

    IEnumerator PopUp()
    {
        transfom = this.gameObject.GetComponent<RectTransform>();
        transfom.localScale = new Vector3(0, 0, 0);

        while (transfom.localScale.x <= 0.98f)
        {
            transfom.localScale = new Vector3(transfom.localScale.x + effectSpeed * Time.deltaTime, transfom.localScale.y + effectSpeed * Time.deltaTime, transfom.localScale.z + effectSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
