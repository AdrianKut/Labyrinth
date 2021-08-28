using UnityEngine;

public class PickableItem : GameManagerInitialazor
{
    public ItemToPick ItemToPick;

    void Start()
    {
        InitializeGameManager();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.PickItem(ItemToPick, this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}