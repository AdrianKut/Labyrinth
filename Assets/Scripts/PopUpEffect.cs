using UnityEngine;

public class PopUpEffect : MonoBehaviour
{
    public float effectSpeed = 3f;
    private RectTransform transfom;
    private Vector3 initScale = Vector3.one;

    private void Awake()
    {
        initScale = transform.localScale;
    }

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
        transfom.localScale = new Vector3( 0, 0, 0 );
        this.gameObject.LeanScale( initScale, 1f ).setEaseOutQuart();

    }
}