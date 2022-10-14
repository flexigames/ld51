using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    Outline outline;

    public virtual void Interact(GameObject playerHolding) {}

    public virtual bool CanBeUsed(GameObject playerHolding)
    {
        return true;
    }

    public virtual bool IsContinous()
    {
        return false;
    }

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
