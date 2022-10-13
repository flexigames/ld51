using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropoffLocation : MonoBehaviour
{
    public string acceptsItemType;

    public string updateTaskId;

    public void DropOff(GameObject item)
    {
        Destroy(item);

        if (updateTaskId != null && updateTaskId != "") {
            TaskManager taskManager = FindObjectOfType<TaskManager>();
            taskManager.IncreaseSteps(updateTaskId);
        }
    }
}
