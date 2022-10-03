using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI taskText;

    private List<CompletableTask> tasks = new List<CompletableTask>() {
        new CompletableTask("books", "Put all books in the shelf", 2),
        new CompletableTask("kitchen", "Do the dishes", 3),
        new CompletableTask("laundry", "Pick up laundry from the floor", 3)
    };

    void Start() {
        PrintTasks();
    }

    public void IncreaseSteps(string taskId)
    {
        CompletableTask task = tasks.Find(t => t.id == taskId);
        task.completedSteps++;

        PrintTasks();
    }

    public void PrintTasks()
    {
        string text = "";
        foreach (CompletableTask task in tasks) {
            text += task.name + " " + task.completedSteps + "/" + task.totalSteps + "\n";
        }
        taskText.text = text;
    }
}

public class CompletableTask
{
    public string id;
    public string name;
    public int totalSteps;
    public int completedSteps = 0;

    public CompletableTask(string id, string name, int totalSteps)
    {
        this.id = id;
        this.name = name;
        this.totalSteps = totalSteps;
    }
}