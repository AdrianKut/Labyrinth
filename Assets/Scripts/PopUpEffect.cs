using UnityEngine;

public class PopUpEffect : MonoBehaviour
{
    public float effectSpeed = 3f;
    private RectTransform transfom;
    private void Start()
    {
        PopUp();
    }

    private void OnEnable()
    {

        PopUp();
    }

    private void PopUp()
    {

        transfom = this.gameObject.GetComponent<RectTransform>();
        transfom.localScale = new Vector3(0, 0, 0);
        this.gameObject.LeanScale(new Vector3(1, 1, 1), 1f).setEaseOutQuart();

        //    transfom = this.gameObject.GetComponent<RectTransform>();
        //    transfom.localScale = new Vector3(0, 0, 0);

        //    while (transfom.localScale.x <= 0.98f)
        //    {
        //        transfom.localScale = new Vector3(transfom.localScale.x + effectSpeed * Time.deltaTime, transfom.localScale.y + effectSpeed * Time.deltaTime, transfom.localScale.z + effectSpeed * Time.deltaTime);
        //        yield return new WaitForSeconds(0.01f);
        //    }
        //}

    }
}