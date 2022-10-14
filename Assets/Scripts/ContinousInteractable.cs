using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousInteractable: Interactable {
    public float secondsNeeded = 2;
    public float secondsSpent = 0;

    public virtual void ContinousInteract()
    {
        secondsSpent += Time.deltaTime;
        if (secondsSpent > secondsNeeded)
        {
            OnDone();
        }
    }

    public virtual void OnDone() {}
}