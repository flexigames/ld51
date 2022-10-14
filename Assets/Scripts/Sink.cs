using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : DropoffLocation {

    public int numberOfDirtyPlates = 0;

    public GameObject dirtyPlatesObject;

    public float timeSpentOnTask = 0;

    public float secondsNeededPerTask = 2;

    private ProgressBar progressBar;

    public void Start()
    {
        progressBar = ProgressBar.Add(transform);
    }

    public override bool IsContinous()
    {
        return numberOfDirtyPlates > 0;
    }

    public override bool CanBeUsed(GameObject playerHolding)
    {
        return CanDropOff(playerHolding) || numberOfDirtyPlates > 0;
    }

    public override void Interact(GameObject playerHolding)
    {
        if (CanDropOff(playerHolding))
        {
            Debug.Log("Add dirty plate");
            AddDirtyPlate();
            Destroy(playerHolding);
        }

        if (numberOfDirtyPlates <= 0) return;

        UpdateTimeSpent();

        if (timeSpentOnTask >= secondsNeededPerTask)
        {
            timeSpentOnTask = 0;
            CleanPlate();
        }
    }

    void UpdateTimeSpent()
    {
        timeSpentOnTask += Time.deltaTime;

        progressBar.SetProgress(timeSpentOnTask / secondsNeededPerTask);
    }

    void AddDirtyPlate()
    {
        numberOfDirtyPlates++;
        dirtyPlatesObject.SetActive(true);
    }

    void CleanPlate()
    {
        Game.IncreaseSteps("kitchen");
        Game.PlaySound("success");

        numberOfDirtyPlates--;

        if (numberOfDirtyPlates <= 0 )
        {
            dirtyPlatesObject.SetActive(false);
        }
    }
}