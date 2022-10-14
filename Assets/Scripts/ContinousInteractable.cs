using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousInteractable: Interactable {
    public float secondsNeeded = 2;
    public float secondsSpent = 0;

    public override bool IsContinous()
    {
        return true;
    }

    public override void Interact(GameObject playerHolding)
    {
        secondsSpent += Time.deltaTime;

        progressBar.SetProgress(secondsSpent / secondsNeeded);

        if (secondsSpent > secondsNeeded)
        {
            OnDone();
        }
    }

    public virtual void OnDone() {}

    private ProgressBar progressBar;

    public void Start()
    {
        progressBar = ProgressBar.Add(transform);
    }

    public void Update()
    {
        progressBar.gameObject.transform.rotation = Quaternion.identity;
    }
}