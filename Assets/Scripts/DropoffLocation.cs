using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropoffLocation : MonoBehaviour
{
    public string acceptsItemType;

    public string updateTaskId;

    public virtual void DropOff(GameObject item)
    {
        Destroy(item);

        if (updateTaskId != null && updateTaskId != "") {
            Game.IncreaseSteps(updateTaskId);
        }
    }
}
