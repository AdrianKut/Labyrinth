using UnityEngine;

public class LeanMoveTween : MonoBehaviour
{
    public float moveSpeed;
    public float xBound;

    void Start()
    {
        LeanTween.moveX(this.gameObject, xBound, moveSpeed).setEaseInOutQuad().setLoopPingPong();
    }
}
