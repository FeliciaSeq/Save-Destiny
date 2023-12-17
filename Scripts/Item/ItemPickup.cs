
using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;



    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp ()
    {
        Debug.Log("Picking up: " + item.name);
        //Add item to inventory
        bool wasPickedUp = Inventory.instance.Add(item);



        //Inventory.instance.Add(item);

        //Removing game object from the scene

        if (wasPickedUp)
            Destroy(gameObject);
    }


}
