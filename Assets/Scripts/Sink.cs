using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : DropoffLocation {

    public int numberOfDirtyPlates = 0;

    public GameObject dirtyPlatesObject;

    public float timeSpentOnTask = 0;

    public float secondsNeededPerTask = 5;

    public override void DropOff(GameObject item)
    {
        AddDirtyPlate();
        Destroy(item);
    }

    public void Interact()
    {
        if (numberOfDirtyPlates <= 0) return;

        timeSpentOnTask += Time.deltaTime;
        if (timeSpentOnTask >= secondsNeededPerTask)
        {
            timeSpentOnTask = 0;
            CleanPlate();
        }
    }

    void AddDirtyPlate()
    {
        numberOfDirtyPlates++;
        dirtyPlatesObject.SetActive(true);
    }

    void CleanPlate()
    {
        Game.IncreaseSteps("kitchen");

        numberOfDirtyPlates--;

        if (numberOfDirtyPlates <= 0 )
        {
            dirtyPlatesObject.SetActive(false);
        }
    }
}