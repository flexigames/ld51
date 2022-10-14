using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem: Interactable
{
    public string itemType;

    public override bool CanBeUsed(GameObject playerHolding)
    {
        return playerHolding == null;
    }
}
