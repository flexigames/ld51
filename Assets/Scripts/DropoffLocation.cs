using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropoffLocation : Interactable
{
    public string acceptsItemType;

    public string updateTaskId;

    public override bool CanBeUsed(GameObject playerHolding)
    {
        return playerHolding != null && acceptsItemType == playerHolding.GetComponent<PickupItem>().itemType;
    }

    public override void Interact(GameObject playerHolding)
    {
        Game.PlaySound("success");
        Destroy(playerHolding);

        if (updateTaskId != null && updateTaskId != "") {
            Game.IncreaseSteps(updateTaskId);
        }
    }
}
