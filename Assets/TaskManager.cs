using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<GameTask> tasks;

    private Dictionary<string, int> completedSteps = new Dictionary<string, int>();


    void Start() {
        foreach (GameTask task in tasks) {
            completedSteps.Add(task.id, 0);
        }
    }

    public void IncreaseSteps(string taskId)
    {
        completedSteps[taskId]++;
        PrintTasks();
    }

    public void PrintTasks() {
        foreach (GameTask task in tasks) {
            Debug.Log(task.name + ": " + completedSteps[task.id] + "/" + task.numberOfSteps);
        }
    }

}
