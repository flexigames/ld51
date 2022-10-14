using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem: MonoBehaviour
{
    public string itemType;

    Outline outline;

    void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 4f;
    }

    public void Focus()
    {
        outline.enabled = true;
    }

    public void UnFocus()
    {
        outline.enabled = false;
    }
}
